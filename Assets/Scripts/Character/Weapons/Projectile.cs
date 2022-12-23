using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	[SerializeField]
	private ProjectileMoveController moveController;

	private List<IStateEffect> _effects;
	private Character _target;
	private int _damage = 1;

	public UnityAction OnHitTarget;

	private void Awake()
	{
		moveController = this.GetComponent<ProjectileMoveController>();
	}

	public void AttackTarget(Character target)
	{
		_target = target;
		this.transform.localPosition = Vector3.zero;
		this.transform.localRotation = Quaternion.identity;
		StartCoroutine(moveController.StartMove(target));
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
		if (!other.gameObject.Equals(_target.gameObject))
			return;

		_target.TakeDmg(_damage);
		_target.ApplyEffects(_effects);
		OnHitTarget.Invoke();
	}
}