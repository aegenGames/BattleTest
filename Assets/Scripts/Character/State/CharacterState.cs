using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterState : MonoBehaviour
{
	[SerializeField]
	private EffectsManager _effectsManager;
	[SerializeField]
	private HealthBar _health;
	[SerializeField]
	private ShieldBar _shield;

	public UnityEvent OnDied { get; } = new UnityEvent();

	private void Start()
	{
		_health.OnHPOver.AddListener(Died);
		_effectsManager = this.GetComponent<EffectsManager>();
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