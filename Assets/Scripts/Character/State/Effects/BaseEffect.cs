using UnityEngine;
using UnityEngine.Events;

public class BaseEffect : MonoBehaviour, IStateEffect
{
	[SerializeField]
	protected string _effectName;

	protected Character target;
	public string EffectName { get => _effectName; }
	public UnityEvent<IStateEffect> OnEffectEnded { get; } = new UnityEvent<IStateEffect>();

	public void SetTarget(Character state)
	{
		target = state;
	}

	public virtual void ActivateEffect(IStateEffect effect) { }

	public void DeactivateEffect()
	{
		OnEffectEnded.Invoke(this);
	}

	public virtual bool Use()
	{
		return true;
	}

	public GameObject GetGameObject()
	{
		return this.gameObject;
	}

	public override int GetHashCode()
	{
		return _effectName.GetHashCode();
	}

	public override bool Equals(object other)
	{
		return Equals(other as IStateEffect);
	}

	public bool Equals(IStateEffect other)
	{
		if (other == null)
			return false;

		return this.EffectName.Equals(other.EffectName);
	}
}
