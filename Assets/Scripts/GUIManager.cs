using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	[SerializeField]
	private Button _endTurnButton;

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