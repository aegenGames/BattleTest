using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour, IAnimationController
{
	private Animator _animator;

	private void Start()
	{
		_animator = this.GetComponent<Animator>();
	}

	public void SetFalseAnimation(string name)
	{
		SetAnimationTrigger(name, false);
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

	public void SetAnimationTrigger(string name, bool value)
	{
		_animator.SetBool(name, value);
	}

	public void SetRoot()
	{
		StopAllCoroutines();
		_animator.Play("Idle");
	}
}