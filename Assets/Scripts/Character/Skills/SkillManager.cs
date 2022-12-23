using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager : MonoBehaviour
{
	[SerializeField]
	private SkillTrigger trigger;
	[SerializeField]
	[SerializeInterface(typeof(ISkill))]
	private List<Object> _skillsPrefabs;
	protected ISkill[] _skills => _skillsPrefabs.OfType<ISkill>().ToArray();
	private ISkill activeSkill;

	public Character SelfTarget { get; set; }

	public UnityAction<string, bool> SetAnimation;
	public delegate IEnumerator WaitAnimationStateHandler(string nameTrigger, bool value);
	public event WaitAnimationStateHandler WaitAnimationStateEvent;
	public delegate IEnumerator ReturnOnStartPositionHandler();
	public event ReturnOnStartPositionHandler ReturnOnStartPositionEvent;
	public delegate IEnumerator MoveToEnemyHandler(Transform target, float distanceForAttack);
	public event MoveToEnemyHandler MoveToEnemyEvent;

	private void Start()
	{
		for (int i = 0; i < _skills.Length; ++i)
		{
			_skills[i].OnSkillFinished.AddListener(EndSkill);
			_skills[i].MoveToEnemyEvent += MoveToEnemyForAttack;
			_skills[i].ReturnOnStartPositionEvent += MoveToPosition;
			_skills[i].AttackAnimationEvent += SetAttackAnimation;
			_skills[i].WaitAnimationStateEvent += WaitAnimationState;
		}

		trigger.SelfTarget = SelfTarget;
		trigger.OnHitObject += LaunchSkill;
	}

	private void SetAttackAnimation(string nameState, bool value)
	{
		SetAnimation(nameState, value);
	}

	public void ActivateTrigger()
	{
		activeSkill = _skills[Random.Range(0, _skills.Length)];
		trigger.gameObject.SetActive(true);
		trigger.SetSkill(activeSkill);
	}

	public void DeactivatedTrigger()
	{
		trigger.gameObject.SetActive(false);
	}

	public void ResetManager()
	{
		if (activeSkill != null)
			activeSkill.BreakSkill();
		DeactivatedTrigger();
	}

	private void LaunchSkill(Character target)
	{
		if (Player.IsBusy)
			return;

		Player.IsBusy = true;
		activeSkill.StartSkill(target);
		DeactivatedTrigger();
	}

	private void EndSkill()
	{
		Player.IsBusy = false;
	}

	protected IEnumerator MoveToEnemyForAttack(Transform target, float distanceToEnmy)
	{
		yield return StartCoroutine(MoveToEnemyEvent(target, distanceToEnmy));
	}
	protected IEnumerator MoveToPosition()
	{
		yield return StartCoroutine(ReturnOnStartPositionEvent());
	}

	protected IEnumerator WaitAnimationState(string nameState, bool value)
	{
		yield return StartCoroutine(WaitAnimationStateEvent(nameState, value));
	}
}