using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SkillMeleeAttack : BaseSkill, IMovementSkill, IAnimatedSkill
{
	[Header("Attack settings")]
	[SerializeField]
	[Min(0)]
	private float _distanceForAttack = 0.1f;
	[SerializeField]
	[Min(1)]
	private int _damage = 3;

    public UnityAction<string, bool> SkillAnimationEvent { get; set; }

	public event IAnimatedSkill.WaitAnimationStateHandler WaitAnimationStateEvent;
	public event IMovementSkill.ReturnOnStartPositionHandler ReturnOnStartPositionEvent;
    public event IMovementSkill.MoveToEnemyHandler MoveToEnemyEvent;

    public override void StartSkill(ICharacter target)
	{
		base.StartSkill(target);
		StartCoroutine(StartAttack(target));
	}

	private IEnumerator StartAttack(ICharacter target)
	{
		yield return StartCoroutine(MoveToEnemyEvent(target.GetGameObject().transform, _distanceForAttack));
		StartCoroutine(Attack(target));
	}

	public IEnumerator Attack(ICharacter target)
	{
		SkillAnimationEvent("StartMeleeAnimation", true);
		yield return StartCoroutine(WaitAnimationStateEvent("MeleeAttack", true));
		target.TakeDmg(_damage);
		IEffectable targetEffects = target as IEffectable;
		targetEffects?.ApplyEffects(Effects);
		_particleAttackEffect.Play(true);
		yield return StartCoroutine(WaitAnimationStateEvent("Idle", false));
		yield return StartCoroutine(StopAttack());
	}

	private IEnumerator StopAttack()
	{
		yield return StartCoroutine(ReturnOnStartPositionEvent());
		OnSkillFinished.Invoke();
	}
}