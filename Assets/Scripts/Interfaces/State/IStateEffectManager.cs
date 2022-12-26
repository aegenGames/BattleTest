using System.Collections.Generic;

public interface IStateEffectManager
{
	void ActivateEffect(IStateEffect effect, Character target);
	void ActivateEffects(List<IStateEffect> effects, Character target);
	void DeactivateEffect(IStateEffect effect);
	void DeactivateEffects(List<IStateEffect> effects);
	void DeactivateEffects(Dictionary<string, IStateEffect> effects);
	void DeactivateAllEffects();
	void UseEffects();
}
