using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager : MonoBehaviour, ISkillManager
{
	[SerializeField]
	[SerializeInterface(typeof(ISkillTrigger))]
	private Object _trigger;
	private ISkillTrigger Trigger => _trigger as ISkillTrigger;

	[SerializeField]
	[SerializeInterface(typeof(ISkill))]
	private List<Object> _skillsPrefabs;
	private ISkill[] _skills => _skillsPrefabs.OfType<ISkill>().ToArray();

	private ISkill activeSkill;

	public ICharacter SelfTarget { get; set; }
    public UnityAction<string, bool> SkillAnimationEvent { get; set; }

	public event IAnimatedSkill.WaitAnimationStateHandler WaitAnimationStateEvent;
	public event IMovementSkill.ReturnOnStartPositionHandler ReturnOnStartPositionEvent;
	public event IMovementSkill.MoveToEnemyHandler MoveToEnemyEvent;

	private void Start()
	{
		SetEvents();
	}

	private void SetEvents()
    {
		for (int i = 0; i < _skills.Length; ++i)
		{
			_skills[i].OnSkillFinished.AddListener(EndSkill);

			IMovementSkill movementSkill = _skills[i] as IMovementSkill;
			if (movementSkill != null)
			{
				movementSkill.MoveToEnemyEvent += MoveToEnemyForAttack;
				movementSkill.ReturnOnStartPositionEvent += MoveToPosition;
			}

			IAnimatedSkill animatedSkill = _skills[i] as IAnimatedSkill;
			if (animatedSkill != null)
			{
				animatedSkill.SkillAnimationEvent += SetAttackAnimation;
				animatedSkill.WaitAnimationStateEvent += WaitAnimationState;
			}
		}

		Trigger.SelfTarget = SelfTarget;
		Trigger.OnHitObject += LaunchSkill;
	}

	private void SetAttackAnimation(string nameState, bool value)
	{
		SkillAnimationEvent(nameState, value);
	}

	public void ActivateTrigger()
	{
		activeSkill = _skills[Random.Range(0, _skills.Length)];
		Trigger.GetGameObject().SetActive(true);
		Trigger.SetSkill(activeSkill);
	}

	public void DeactivatedTrigger()
	{
		Trigger.GetGameObject().SetActive(false);
	}

	public void ResetManager()
	{
		if (activeSkill != null)
			activeSkill.BreakSkill();
		DeactivatedTrigger();
	}

	private void LaunchSkill(ICharacter target)
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

	private IEnumerator MoveToEnemyForAttack(Transform target, float distanceToEnmy)
	{
		yield return StartCoroutine(MoveToEnemyEvent(target, distanceToEnmy));
	}
	private IEnumerator MoveToPosition()
	{
		yield return StartCoroutine(ReturnOnStartPositionEvent());
	}

	private IEnumerator WaitAnimationState(string nameState, bool value)
	{
		yield return StartCoroutine(WaitAnimationStateEvent(nameState, value));
	}
}