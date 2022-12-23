using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CleaningEffect : BaseEffect
{
	[SerializeField]
	[SerializeInterface(typeof(IStateEffect))]
	private List<Object> _removableEffectPrefabs;
	private List<IStateEffect> _removableDebuffs => _removableEffectPrefabs.OfType<IStateEffect>().ToList();

	public override void ActivateEffect(IStateEffect effect)
	{
		base.ActivateEffect(effect);
		Use();
	}

	public override bool Use()
	{
		target.RemoveEffects(_removableDebuffs);
		DeactivateEffect();
		base.Use();
		return true;
	}
}