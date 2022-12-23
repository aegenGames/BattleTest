using System;
using UnityEngine;
using UnityEngine.Events;

public interface IStateEffect : IEquatable<IStateEffect>
{
	string EffectName { get; }
	UnityEvent<IStateEffect> OnEffectEnded { get; }
	void SetTarget(Character state);
	void ActivateEffect(IStateEffect effectSettings);
	void DeactivateEffect();
	GameObject GetGameObject();
	bool Use();
	public bool Equals(object other);
	public int GetHashCode();
}