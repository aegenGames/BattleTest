using System.Collections;
using UnityEngine;

public class Bot : MonoBehaviour
{
	[SerializeField]
	private Player _botPlayer;
	[SerializeField]
	private float _pauseBetweenAttack = 1;

	private Character[] _targets;

	private void Start()
	{
		_botPlayer.OnStartTurn += Activate;
		_targets = GetTargets();
	}

	private void Activate()
	{
		SkillTrigger[] triggers = GetTriggers();
		StartCoroutine(StartAttacks(triggers, _targets));
	}

	private SkillTrigger[] GetTriggers()
	{
		return GameObject.FindObjectsOfType<SkillTrigger>();
	}

	private Character[] GetTargets()
	{
		GameObject[] targetsGO = GameObject.FindGameObjectsWithTag("PlayerCharacter");
		int goCount = targetsGO.Length;

		Character[] targets = new Character[goCount];
		for(int i = 0; i < goCount; ++i)
		{
			targets[i] = targetsGO[i].GetComponent<Character>();
		}

		return targets;
	}

	private IEnumerator StartAttacks(SkillTrigger[] triggers, Character[] targets)
	{
		int targetsCount = targets.Length;
		WaitForSeconds wait = new WaitForSeconds(_pauseBetweenAttack);

		for (int i = 0; i < triggers.Length; ++i)
		{
			yield return wait;
			triggers[i].HitObject(targets[Random.Range(0, targetsCount)]);
			do
			{
				yield return new WaitForEndOfFrame();
			}
			while (Player.IsBusy);

			if (_botPlayer.IsActive == false)
				yield break;
		}

		EndAttack();
	}

	private void EndAttack()
	{
		_botPlayer.EndTurn();
	}
}
