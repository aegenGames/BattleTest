using UnityEngine;

public class SkillDistantionAttack : BaseSkill
{
	[Min(1)]
	[SerializeField]
	private int _damage = 1;

	[SerializeField]
	[SerializeInterface(typeof(IProjectile))]
	private Object projectile;
	private IProjectile _projectile => projectile as IProjectile;

	private void Start()
	{
		_projectile.OnHitTarget += StopSkill;
		_projectile.SetEffects(_effects);
		_projectile.SetDmg(_damage);
	}

	public override void StartSkill(ICharacter target)
	{
		base.StartSkill(target);
		_projectile.SetActive(true);
		_projectile.AttackTarget(target);
	}

	private void StopSkill()
	{
		_projectile.SetActive(false);
		OnSkillFinished.Invoke();
	}

	public override void BreakSkill()
	{
		base.BreakSkill();
		_projectile.SetActive(false);
	}
}