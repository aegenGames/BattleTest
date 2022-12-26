using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMoveController : MonoBehaviour, ICharacterMoveController
{
	private NavMeshAgent _agent;
	public UnityAction<string, bool> ChangeTriggerState { get; set; }
	
	private void Start()
	{
		_agent = this.GetComponent<NavMeshAgent>();
	}

	public IEnumerator MoveTo(Vector3 target, float stoppingDistance = 0)
	{
		ChangeTriggerState("IsMovement", true);

		_agent.stoppingDistance = stoppingDistance;
		_agent.SetDestination(target);
		yield return new WaitForSeconds(0.1f);

		do
		{
			yield return new WaitForEndOfFrame();
		}
		while (_agent.velocity.magnitude != 0);

		SetDormancy();
	}

	public IEnumerator MoveToEnemy(UnityEngine.Transform target, float StopingDistanceToEnemy = 0.1f)
	{
		NavMeshAgent targetAgent = target.GetComponent<NavMeshAgent>();
		float objectsRadius = targetAgent ? targetAgent.radius * target.lossyScale.z + _agent.radius * this.transform.lossyScale.z : 0;
		float stoppingDistance = objectsRadius + StopingDistanceToEnemy;

		yield return StartCoroutine(MoveTo(target.position, stoppingDistance));
	}

	public IEnumerator Rotate(Vector3 axis, float angle)
	{
		ChangeTriggerState("IsMovement", true);

		angle = angle - this.transform.eulerAngles.y;
		float rotationSpeed;
		float angleRotate;

		while (Mathf.Abs(angle) > 0)
		{
			rotationSpeed = _agent.angularSpeed * Time.deltaTime;
			angleRotate = Mathf.Clamp(angle, -rotationSpeed, rotationSpeed);
			_agent.transform.Rotate(axis, angleRotate);
			angle -= angleRotate;
			yield return new WaitForEndOfFrame();
		}

		ChangeTriggerState("IsMovement", false);
	}

	public IEnumerator Rotate(Quaternion rotation, Vector3 axis)
	{
		yield return StartCoroutine(Rotate(axis, rotation.eulerAngles.y));
	}

	public void TeleportOnPosition(Vector3 position, Quaternion rotate)
	{
		_agent.transform.position = position;
		_agent.transform.rotation = rotate;
	}

	public void StopAllMove()
	{
		StopAllCoroutines();
		SetDormancy();
		_agent.velocity = Vector3.zero;
	}

	private void SetDormancy()
	{
		_agent.ResetPath();
		_agent.stoppingDistance = 0;
		ChangeTriggerState("IsMovement", false);
	}
}