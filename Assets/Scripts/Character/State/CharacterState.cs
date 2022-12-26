using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterState : MonoBehaviour, ICharacterState
{
	[SerializeField]
	[SerializeInterface(typeof(IStateEffectManager))]
	private Object _effectsPrefab;
	private IStateEffectManager EffectsManager => _effectsPrefab as IStateEffectManager;

	[SerializeField]
	[SerializeInterface(typeof(IHealth))]
	private Object _healthBar;
	private IHealth Health => _healthBar as IHealth;

	[SerializeField]
	[SerializeInterface(typeof(IShield))]
	private Object _shieldBar;
	private IShield Shield => _shieldBar as IShield;

	public UnityAction OnDied { get; set; }

	private void Start()
	{
		Health.OnHPOver.AddListener(Died);
	}

	private void Died()
	{
		OnDied.Invoke();
	}

	public void ResetState()
	{
		Health.ReserHP();
		Shield.ResetShield();
		EffectsManager.DeactivateAllEffects();
	}

	public void IncreaseHealth(int value)
	{
		Health.HP += value;
	}

	public void HandleDmg(int dmg)
	{
		dmg = Shield.TakeDmg(dmg);
		Health.HP -= dmg;
	}

	public void SetShield(int value, int duration)
	{
		Shield.SetShield(value, duration);
	}

	public void ApplyEffects(List<IStateEffect> effects, Character target)
	{
		EffectsManager.ActivateEffects(effects, target);
	}

	public void RemoveEffects(List<IStateEffect> effects)
	{
		EffectsManager.DeactivateEffects(effects);
	}

	public void ChangeState()
	{
		EffectsManager.UseEffects();
		Shield.DecreaseDuration();
	}
}