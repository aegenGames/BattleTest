using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	private Animator _animator;

	private void Start()
	{
		_animator = this.GetComponent<Animator>();
	}

	public void SetFalseAttackAnimation(string name)
	{
		SetBool(name, false);
	}

	/// <summary>
	/// Корутина работает, пока не выйдет, или не достигнет конца или начала состояния,
	/// в зависимости от значения isEnd.
	/// </summary>
	/// <param name="nameState">
	/// Имя состояния
	/// </param>
	/// <param name="isEnd">
	/// true - Ожидание конца указанного состояния.
	/// false - Ожидание начала указанного состояния.
	/// </param>
	/// <returns></returns>
	public IEnumerator WaitAnimationState(string nameState, bool isEnd)
	{
		do
		{
			yield return new WaitForEndOfFrame();
		} while (_animator.GetCurrentAnimatorStateInfo(0).IsName(nameState) == isEnd);
	}

	public void SetBool(string name, bool value)
	{
		_animator.SetBool(name, value);
	}

	public void SetRoot()
	{
		StopAllCoroutines();
		_animator.Play("Idle");
	}
}