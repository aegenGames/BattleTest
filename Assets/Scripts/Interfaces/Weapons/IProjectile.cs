using System.Collections.Generic;
using UnityEngine.Events;

public interface IProjectile
{
	UnityAction OnHitTarget { get; set; }
	void AttackTarget(ICharacter target);
	void SetEffects(List<IStateEffect> newEffects);
	void SetDmg(int dmg);
	void SetActive(bool value);
}
