using System;
using UnityEngine;
using UnityEngine.Events;

public class CharactersManager : MonoBehaviour
{
	[SerializeField]
	private Character[] _characterPrefabs;
	[SerializeField]
	private Transform _spawnPointsSet;

	private Character[] _characters;
	private Transform[] _spawnPoints;

	public UnityAction OnCharacterDied;

	private void Awake()
	{
		ExtractSpawnPoints();
		InstantiateCharacter();
	}

	private void ExtractSpawnPoints()
	{
		_spawnPoints = new Transform[_spawnPointsSet.childCount];             // GetComponentsInChildren возвращает трансформы потомков вместе с родителем
		for (int i = 0; i < _spawnPoints.Length; ++i)						// поэтому его не использовал
		{
			_spawnPoints[i] = _spawnPointsSet.GetChild(i);
		}
	}

	private void InstantiateCharacter()
	{
		_characters = new Character[_characterPrefabs.Length];
		try
		{
			for (int i = 0; i < _characters.Length; ++i)
			{
				_characters[i] = Instantiate(_characterPrefabs[i], _spawnPoints[i].position, _spawnPoints[i].rotation, this.transform);
				_characters[i].OnDied.AddListener(ChararacterDied);
			}
		}
		catch(IndexOutOfRangeException e)
		{
			Debug.LogError(e);
			throw new ArgumentOutOfRangeException("index parameter is out of range.", e, "\n increase number of spawn points");
		}
	}

	public void ActivateCharacters()
	{
		for (int i = 0; i < _characters.Length; ++i)
		{
			_characters[i].Activate();
		}
	}

	public void DeactivateCharacters()
	{
		for (int i = 0; i < _characters.Length; ++i)
		{
			_characters[i].Deactivate();
		}
	}

	private void ChararacterDied()
	{
		OnCharacterDied();
	}

	public void ResetManager()
	{
		for (int i = 0; i < _characters.Length; ++i)
		{
			_characters[i].ResetCharacter();
		}
	}
}