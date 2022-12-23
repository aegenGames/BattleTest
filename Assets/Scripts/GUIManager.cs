using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	[SerializeField]
	private MatchManager _matchManager;
	[SerializeField]
	private Button _endTurnButton;

	private void Start()
	{
		_matchManager.OnPlayerChanged += SetActiveButton;
	}

	private void SetActiveButton(bool value)
	{
		_endTurnButton.gameObject.SetActive(value);
	}

	private void Update()
	{
		if(Input.GetKey("escape"))
		{
			Application.Quit();
		}

		if (Input.GetKeyUp("space") && _endTurnButton.IsActive())
		{
			_endTurnButton.onClick.Invoke();
		}
	}
}