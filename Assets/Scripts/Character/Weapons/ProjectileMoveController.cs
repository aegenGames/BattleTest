using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileMoveController : MonoBehaviour
{
	[Min(0.1f)]
	[SerializeField]
	private float _speed = 6;
	[Min(0.1f)]
	[SerializeField]
	private float _angularSpeed = 4;

	private Rigidbody _rig;

	private void Awake()
	{
		_rig = this.GetComponent<Rigidbody>();
	}

	public IEnumerator StartMove(Character target)
	{
		Quaternion targetRotate;
		Quaternion rotation;
		_rig.velocity = Vector3.zero;

		while (true)
		{
			targetRotate = Quaternion.LookRotation(target.Center - this.transform.position, Vector3.forward);
			rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotate, _angularSpeed);
			_rig.MoveRotation(rotation);
			_rig.AddRelativeForce(0, 0, _speed);
			yield return new WaitForFixedUpdate();
		}
	}
}
