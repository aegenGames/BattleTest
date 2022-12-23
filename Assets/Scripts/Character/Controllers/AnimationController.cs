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
	/// �������� ��������, ���� �� ������, ��� �� ��������� ����� ��� ������ ���������,
	/// � ����������� �� �������� isEnd.
	/// </summary>
	/// <param name="nameState">
	/// ��� ���������
	/// </param>
	/// <param name="isEnd">
	/// true - �������� ����� ���������� ���������.
	/// false - �������� ������ ���������� ���������.
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