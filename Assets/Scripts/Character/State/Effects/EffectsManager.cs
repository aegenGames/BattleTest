using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
	[SerializeField]
	private Transform _outputField;

	private Dictionary<string, IStateEffect> _effectsObjects;
	private Dictionary<string, IStateEffect> _activeEffects;

	private void Awake()
	{
		_effectsObjects = new Dictionary<string, IStateEffect>();
		_activeEffects = new Dictionary<string, IStateEffect>();
	}

	public void ActivateEffects(List<IStateEffect> effects, Character target)
	{
		if (effects == null)
			return;

		foreach(IStateEffect effect in effects)
		{
			ActivateEffect(effect, target);
		}
	}

	public void ActivateEffect(IStateEffect effect, Character target)
	{
		IStateEffect activeEffect = _effectsObjects.GetValueOrDefault(effect.EffectName);

		if (activeEffect == null)
		{
			activeEffect = Instantiate(effect.GetGameObject(), Vector3.zero, _outputField.rotation, _outputField).GetComponent<IStateEffect>();
			activeEffect.SetTarget(target);
			activeEffect.OnEffectEnded.AddListener(DeactivateEffect);
			_effectsObjects.Add(activeEffect.EffectName, activeEffect);
		}
		else
		{
			activeEffect.GetGameObject().SetActive(true);
		}

		activeEffect.ActivateEffect(effect);
		_activeEffects.TryAdd(activeEffect.EffectName, activeEffect);
	}

	public void DeactivateEffect(IStateEffect effect)
	{
		Debug.Log(effect);
		IStateEffect activeEffect = _activeEffects.GetValueOrDefault(effect.EffectName);
		if (activeEffect == null)
			return;

		activeEffect.GetGameObject().SetActive(false);
		_activeEffects.Remove(effect.EffectName);
	}

	public void DeactivateEffects(List<IStateEffect> effects)
	{
		foreach(IStateEffect effect in effects)
		{
			DeactivateEffect(effect);
		}
	}

	public void DeactivateEffects(Dictionary<string, IStateEffect> effects)
	{
		DeactivateEffects(effects.Values.ToList());
	}

	public void DeactivateAllEffects()
	{
		DeactivateEffects(_activeEffects);
	}

	public void UseEffects()
	{
		IStateEffect effect;
		for (int i = 0; i < _activeEffects.Count; ++i)
		{
			effect = _activeEffects.ElementAt(i).Value;
			if(effect.Use())
			{
				--i;
			}
		}
	}
}