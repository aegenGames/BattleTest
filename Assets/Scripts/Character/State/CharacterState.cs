using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterState : MonoBehaviour, ICharacterState
{
	[SerializeField]
	[SerializeInterface(typeof(IStateEffectManager))]
	private Object effectsManager;
	private IStateEffectManager _effectsManager => effectsManager as IStateEffectManager;

	[SerializeField]
	[SerializeInterface(typeof(IHealth))]
	private Object healthBar;
	private IHealth _health => healthBar as IHealth;

	[SerializeField]
	[SerializeInterface(typeof(IShield))]
	private Object shieldBar;
	private IShield _shield => shieldBar as IShield;

	public UnityAction OnDied { get; set; }

	private void Start()
	{
		_health.OnHPOver.AddListener(Died);
	}

	private void Died()
	{
		OnDied.Invoke();
	}

	public void ResetState()
	{
		_health.ReserHP();
		_shield.ResetShield();
		_effectsManager.DeactivateAllEffects();
	}

	public void IncreaseHealth(int value)
	{
		_health.HP += value;
	}

	public void HandleDmg(int dmg)
	{
		dmg = _shield.TakeDmg(dmg);
		_health.HP -= dmg;
	}

	public void SetShield(int value, int duration)
	{
		_shield.SetShield(value, duration);
	}

	public void ApplyEffects(List<IStateEffect> effects, Character target)
	{
		_effectsManager.ActivateEffects(effects, target);
	}

	public void RemoveEffects(List<IStateEffect> effects)
	{
		_effectsManager.DeactivateEffects(effects);
	}

	public void ChangeState()
	{
		_effectsManager.UseEffects();
		_shield.DecreaseDuration();
	}
}