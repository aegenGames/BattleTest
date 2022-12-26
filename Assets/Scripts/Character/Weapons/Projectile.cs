using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(IProjectileMoveController))]
public class Projectile : MonoBehaviour, IProjectile
{
	private IProjectileMoveController moveController;

	private List<IStateEffect> _effects;
	private ICharacter _target;
	private int _damage = 1;

	public UnityAction OnHitTarget { get; set; }

	private void Awake()
	{
		moveController = this.GetComponent<IProjectileMoveController>();
	}

	public void AttackTarget(ICharacter target)
	{
		_target = target;
		this.transform.localPosition = Vector3.zero;
		this.transform.localRotation = Quaternion.identity;
		StartCoroutine(moveController.MoveToTarget(target));
	}

	public void SetEffects(List<IStateEffect> newEffects)
	{
		_effects = newEffects;
	}

	public void SetDmg(int dmg)
	{
		_damage = dmg;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.Equals(_target.GetGameObject()))
			return;

		_target.TakeDmg(_damage);
		IEffectable targetEffects = _target as IEffectable;
		targetEffects?.ApplyEffects(_effects);
		OnHitTarget.Invoke();
	}
	public void SetActive(bool value)
    {
		this.gameObject.SetActive(value);
    }
}