using UnityEngine;

public class SkillDistantionAttack : BaseSkill
{
	[Min(1)]
	[SerializeField]
	private int _damage = 1;

	[SerializeField]
	[SerializeInterface(typeof(IProjectile))]
	private Object _projectile;
	private IProjectile Projectile => _projectile as IProjectile;

	private void Start()
	{
		Projectile.OnHitTarget += StopSkill;
		Projectile.SetEffects(Effects);
		Projectile.SetDmg(_damage);
	}

	public override void StartSkill(ICharacter target)
	{
		base.StartSkill(target);
		Projectile.SetActive(true);
		Projectile.AttackTarget(target);
	}

	private void StopSkill()
	{
		Projectile.SetActive(false);
		OnSkillFinished.Invoke();
	}

	public override void BreakSkill()
	{
		base.BreakSkill();
		Projectile.SetActive(false);
	}
}