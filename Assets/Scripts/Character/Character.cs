using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ISkillManager))]
[RequireComponent(typeof(ICharacterState))]
[RequireComponent(typeof(ICharacterMoveController))]
[RequireComponent(typeof(IAnimationController))]
public class Character : MonoBehaviour, ICharacter, IMovable, IAnimated, IEffectable
{
	private ISkillManager _skillManager;
	private ICharacterState _state;
	private ICharacterMoveController _moveController;
	private IAnimationController _animController;

	private Vector3 _startPosition;
	private Quaternion _startRotation;

	public Vector3 Center { get; set; }
	public UnityAction OnDied { get; set; }

	private void Start()
	{
		ComponentCaching();
		SetDelegates();
	}

	public GameObject GetGameObject()
    {
		return this.gameObject;
    }

	private void ComponentCaching()
	{
		_skillManager = this.GetComponent<ISkillManager>();
		_moveController = this.GetComponent<ICharacterMoveController>();
		_state = this.GetComponent<ICharacterState>();
		_animController = this.GetComponent<IAnimationController>();
		Center = this.GetComponent<Collider>().bounds.center;

		_startPosition = this.transform.position;
		_startRotation = this.transform.rotation;
		_skillManager.SelfTarget = this;
	}

	private void SetDelegates()
	{
		_state.OnDied += OnDied;
		_moveController.ChangeTriggerState += SetAnimationTrigger;
		_skillManager.MoveToEnemyEvent += MoveToEnemy;
		_skillManager.ReturnOnStartPositionEvent += MoveToStartPosition;
		_skillManager.WaitAnimationStateEvent += WaitAnimationState;
		_skillManager.SkillAnimationEvent += SetAnimationTrigger;
	}

	public void Activate()
	{
		_skillManager.ActivateTrigger();
	}

	public void Deactivate()
	{
		_skillManager.DeactivatedTrigger();
		_state.ChangeState();
	}

	public void ResetCharacter()
	{
		_state.ResetState();
		_skillManager.ResetManager();
		_moveController.StopAllMove();
		_moveController.TeleportOnPosition(_startPosition, _startRotation);
		_animController.SetRoot();
	}

	public void TakeDmg(int dmg)
	{
		_state.HandleDmg(dmg);
	}

	public void TakeHeal(int value)
	{
		_state.IncreaseHealth(value);
	}

	public void TakeShield(int value, int duration)
	{
		_state.SetShield(value, duration);
	}

	public void ApplyEffects(List<IStateEffect> effects)
	{
		_state.ApplyEffects(effects, this);
	}

	public void RemoveEffects(List<IStateEffect> effects)
	{
		_state.RemoveEffects(effects);
	}

	public IEnumerator MoveToStartPosition()
	{
		yield return MoveTo(_startPosition);
		yield return StartCoroutine(_moveController.Rotate(_startRotation, this.transform.up));
	}

	public IEnumerator MoveToEnemy(Transform target, float distanceForAttack)
	{
		yield return StartCoroutine(_moveController.MoveToEnemy(target.transform, distanceForAttack));
	}

	public IEnumerator MoveTo(Vector3 position, float stoppingDistance = 0)
	{
		yield return StartCoroutine(_moveController.MoveTo(position, stoppingDistance));
	}

	public IEnumerator Rotate(Vector3 axis, float angle)
	{
		yield return StartCoroutine(_moveController.Rotate(axis, angle));
	}

	public IEnumerator WaitAnimationState(string nameTrigger, bool value)
	{
		yield return StartCoroutine(_animController.WaitAnimationState(nameTrigger, value));
	}

	public void SetAnimationTrigger(string nameTrigger, bool value)
	{
		_animController.SetAnimationTrigger(nameTrigger, value);
	}
}