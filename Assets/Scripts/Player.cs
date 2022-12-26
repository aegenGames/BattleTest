using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IPlayer
{
	[SerializeField]
	[SerializeInterface(typeof(ICharacterManager))]
	private Object charactersManager;
	private ICharacterManager _charactersManager => charactersManager as ICharacterManager;

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

	public UnityAction OnTurnOver { get; set; }
	public UnityAction OnStartTurn { get; set; }
	public UnityAction<string> OnLoss { get; set; }

	private void Start()
	{
		_charactersManager.OnCharacterDied += Loss;
	}

	private void StartTurn()
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

	private void Loss()
	{
		OnLoss(this.tag);
	}
}