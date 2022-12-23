using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	[SerializeField]
	private CharactersManager _charactersManager;

	private bool _isActive = false;

	public static bool IsBusy { get; set; } = false;
	public bool IsActive
	{
		get => _isActive;
		set 
		{
			_isActive = value;
			if (_isActive)
				StartTurn();
		}
	}

	public UnityAction OnTurnOver;
	public UnityAction OnStartTurn;
	public UnityAction<string> OnLoss;

	private void Start()
	{
		_charactersManager.OnCharacterDied += Loss;
	}

	public void StartTurn()
	{
		_charactersManager.ActivateCharacters();
		OnStartTurn?.Invoke();
	}

	public void EndTurn()
	{
		if (IsBusy)
			return;

		_charactersManager.DeactivateCharacters();
		OnTurnOver?.Invoke();
	}

	public void ResetPlayer()
	{
		_charactersManager.ResetManager();
	}

	public void Loss()
	{
		OnLoss(this.tag);
	}
}