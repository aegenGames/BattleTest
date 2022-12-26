using UnityEngine.Events;

public interface IHealth
{
	UnityEvent OnHPOver { get; }
	int HP { get; set; }
	void ReserHP();
}
