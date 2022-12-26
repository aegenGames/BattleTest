using System.Collections.Generic;

public interface IEffectable
{
	void ApplyEffects(List<IStateEffect> effects);
	void RemoveEffects(List<IStateEffect> effects);
}
