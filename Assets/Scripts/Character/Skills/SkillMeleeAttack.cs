using System.Collections;
using UnityEngine;

public class SkillMeleeAttack : BaseSkill
{
	[Header("Attack settings")]
	[SerializeField]
	[Min(0)]
	private float _distanceForAttack = 0.1f;
	[SerializeField]
	[Min(1)]
	private int _damage = 3;

    public override void StartSkill(Character target)
	{
		base.StartSkill(target);
		StartCoroutine(StartAttack(target));
	}

	private IEnumerator StartAttack(Character target)
	{
		yield return StartCoroutine(MoveToEnemy(target.transform, _distanceForAttack));
		StartCoroutine(Attack(target));
	}

	public IEnumerator Attack(Character target)
	{
		AttackAnimationEvent("StartMeleeAnimation", true);
		yield return StartCoroutine(WaitAnimationState("MeleeAttack", true));
		target.TakeDmg(_damage);
		target.ApplyEffects(_effects);
		_particleAttackEffect.Play(true);
		yield return StartCoroutine(WaitAnimationState("Idle", false));
		yield return StartCoroutine(StopAttack());
	}

	private IEnumerator StopAttack()
	{
		yield return StartCoroutine(ReturnOnStart());
		OnSkillFinished.Invoke();
	}
}