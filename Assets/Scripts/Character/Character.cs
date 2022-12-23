using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SkillManager))]
[RequireComponent(typeof(CharacterState))]
[RequireComponent(typeof(CharacterMoveController))]
[RequireComponent(typeof(AnimationController))]
public class Character : MonoBehaviour
{
	[SerializeField]
	private SkillManager _skillManager;
	[SerializeField]
	private CharacterState _state;
	[SerializeField]
	private CharacterMoveController _moveController;
	[SerializeField]
	private AnimationController _animController;

	private Vector3 _startPosition;
	private Quaternion _startRotation;

	public Vector3 Center { get; set; }
	public UnityEvent OnDied { get; } = new UnityEvent();

	private void Start()
	{
		ComponentCaching();
		SetDelegates();
	}

	private void ComponentCaching()
	{
		_skillManager = this.GetComponent<SkillManager>();
		_moveController = this.GetComponent<CharacterMoveController>();
		_state = this.GetComponent<CharacterState>();
		_animController = this.GetComponent<AnimationController>();
		Center = this.GetComponent<Collider>().bounds.center;

		_startPosition = this.transform.position;
		_startRotation = this.transform.rotation;
		_skillManager.SelfTarget = this;
	}

	private void SetDelegates()
	{
		_state.OnDied.AddListener(Died);
		_moveController.ChangeTriggerState += SetAnimationTriggerState;
		_skillManager.MoveToEnemyEvent += MoveToEnemy;
		_skillManager.ReturnOnStartPositionEvent += MoveToStartPosition;
		_skillManager.WaitAnimationStateEvent += WaitAnimationState;
		_skillManager.SetAnimation += SetAnimationTriggerState;
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
		_moveController.ReturnStartPosition(_startPosition, _startRotation);
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

	private void Died()
	{
		OnDied.Invoke();
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

	public IEnumerator MoveTo(Vector3 position)
	{
		yield return StartCoroutine(_moveController.MoveTo(position));
	}

	public IEnumerator Rotation(Vector3 axis, float angle)
	{
		yield return StartCoroutine(_moveController.Rotate(axis, angle));
	}

	public IEnumerator WaitAnimationState(string nameTrigger, bool value)
	{
		yield return StartCoroutine(_animController.WaitAnimationState(nameTrigger, value));
	}

	public void SetAnimationTriggerState(string nameTrigger, bool value)
	{
		_animController.SetBool(nameTrigger, value);
	}
}