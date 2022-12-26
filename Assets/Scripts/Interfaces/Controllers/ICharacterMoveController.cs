using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public interface ICharacterMoveController : IMovable
{
	public UnityAction<string, bool> ChangeTriggerState { get; set; }
	public IEnumerator Rotate(Quaternion rotation, Vector3 axis);
	public void TeleportOnPosition(Vector3 position, Quaternion rotate);
	public void StopAllMove();
}
