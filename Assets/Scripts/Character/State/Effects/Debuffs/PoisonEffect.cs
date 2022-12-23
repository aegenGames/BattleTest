using UnityEngine;

public class PoisonEffect : BaseEffect
{
	[Min(1)]
	[SerializeField]
	private int _damage = 1;
	[Min(1)]
	[SerializeField]
	private int _duration = 1;

	public override void ActivateEffect(IStateEffect effect)
	{
		base.ActivateEffect(effect);
		SetProperties(effect as PoisonEffect);
	}

	private void SetProperties(PoisonEffect effect)
	{
		if (effect == null)
			return;

		_damage = effect._damage;
		_duration = effect._duration;
	}

	public override bool Use()
	{
		target.TakeDmg(_damage);
		--_duration;
		if (_duration == 0)
		{
			DeactivateEffect();
			base.Use();
		}

		return false;
	}
}