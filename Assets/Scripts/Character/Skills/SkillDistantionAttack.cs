using UnityEngine;

public class SkillDistantionAttack : BaseSkill
{
	[Min(1)]
	[SerializeField]
	private int _damage = 1;
	[SerializeField]
	private Projectile _projectile;

	private void Start()
	{
		_projectile.OnHitTarget += StopSkill;
		_projectile.SetEffects(_effects);
		_projectile.SetDmg(_damage);
	}

	public override void StartSkill(Character target)
	{
		base.StartSkill(target);
		_projectile.gameObject.SetActive(true);
		_projectile.AttackTarget(target);
	}

	private void StopSkill()
	{
		_projectile.gameObject.SetActive(false);
		OnSkillFinished.Invoke();
	}

	public override void BreakSkill()
	{
		base.BreakSkill();
		_projectile.gameObject.SetActive(false);
	}
}