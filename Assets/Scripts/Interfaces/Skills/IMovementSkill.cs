using System.Collections;
using UnityEngine;

public interface IMovementSkill
{
	delegate IEnumerator ReturnOnStartPositionHandler();
	event ReturnOnStartPositionHandler ReturnOnStartPositionEvent;
	delegate IEnumerator MoveToEnemyHandler(Transform target, float distanceForAttack);
	event MoveToEnemyHandler MoveToEnemyEvent;
}
