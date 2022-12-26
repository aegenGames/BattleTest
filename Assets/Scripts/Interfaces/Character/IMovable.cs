using System.Collections;
using UnityEngine;

public interface IMovable
{
	IEnumerator MoveTo(Vector3 position, float stoppingDistance = 0);
	IEnumerator MoveToEnemy(Transform target, float distanceForAttack);
	IEnumerator Rotate(Vector3 axis, float angle);
}
