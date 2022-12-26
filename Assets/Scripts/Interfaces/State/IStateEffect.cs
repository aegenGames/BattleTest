using System;
using UnityEngine.Events;

public interface IStateEffect : IEquatable<IStateEffect>, IGameObjectli
{
	string EffectName { get; }
	UnityEvent<IStateEffect> OnEffectEnded { get; }
	void SetTarget(Character state);
	void ActivateEffect(IStateEffect effectSettings);
	void DeactivateEffect();
	bool Use();
	bool Equals(object other);
	int GetHashCode();
}