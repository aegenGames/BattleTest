using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour
{
	[Header("Players")]
	[SerializeField]
	Player _player;
	[SerializeField]
	Player _botPlayer;

	[Header("End match")]
	[SerializeField]
	private Image _outputResultGameImage;
	[SerializeField]
	private Sprite _winSprite;
	[SerializeField]
	private Sprite _lossSprite;


	public UnityAction<bool> OnPlayerChanged;

	void Start()
	{
		SetDelegates();
		StartMatch();
	}

	private void SetDelegates()
    {
		_player.OnTurnOver += PassTurn;
		_botPlayer.OnTurnOver += PassTurn;
		_player.OnLoss += EndGame;
		_botPlayer.OnLoss += EndGame;
	}

	private void Update()
	{
		if(Input.GetKeyUp("r"))
		{
			RestartGame();
		}
	}

	private void EndGame(string tagLoser)
	{
		_outputResultGameImage.gameObject.SetActive(true);
		_outputResultGameImage.sprite = tagLoser == "Player" ? _lossSprite : _winSprite;
		_player.IsActive = false;
		_botPlayer.IsActive = false;
		OnPlayerChanged(_player.IsActive);
		Player.IsBusy = true;
	}

	private void RestartGame()
	{
		_player.ResetPlayer();
		_botPlayer.ResetPlayer();
		StartMatch();
		OnPlayerChanged(_player.IsActive);
	}

	private void StartMatch()
	{
		_outputResultGameImage.gameObject.SetActive(false);
		_player.IsActive = true;
		_botPlayer.IsActive = false;
		Player.IsBusy = false;
	}

	private void PassTurn()
	{
		_player.IsActive = !_player.IsActive;
		_botPlayer.IsActive = !_botPlayer.IsActive;
		OnPlayerChanged(_player.IsActive);
	}
}
