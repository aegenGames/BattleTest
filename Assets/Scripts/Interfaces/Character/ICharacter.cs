using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public interface ICharacter : IGameObjectli
{
	Vector3 Center { get; set; }
	UnityAction OnDied { get; set; }
	void Activate();
	void Deactivate();
	void ResetCharacter();
	void TakeDmg(int dmg);
	void TakeHeal(int value);
	void TakeShield(int value, int duration);
	IEnumerator MoveToStartPosition();
}
