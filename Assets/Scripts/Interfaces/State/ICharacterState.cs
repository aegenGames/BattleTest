using System.Collections.Generic;
using UnityEngine.Events;

public interface ICharacterState
{
	UnityAction OnDied { get; set; }
	void ResetState();
	void IncreaseHealth(int value);
	void HandleDmg(int dmg);
	void SetShield(int value, int duration);
	void ApplyEffects(List<IStateEffect> effects, Character target);
	void RemoveEffects(List<IStateEffect> effects);
	void ChangeState();
}
