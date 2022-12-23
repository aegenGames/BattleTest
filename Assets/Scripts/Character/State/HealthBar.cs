using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[Header("DIsplay HP")]
	[SerializeField]
	private Image _outputImage;
	[SerializeField]
	private TextMeshProUGUI _outputText;

	[Header("HP settings")]
	[Min(1)]
	[SerializeField]
	private int maxHP = 10;
	
	private int hp;
	public UnityEvent OnHPOver { get; } = new UnityEvent();

	private void Start()
	{
		ReserHP();
	}

	public int HP
	{
		get => hp;
		set
		{
			hp = Mathf.Clamp(value, 0, maxHP);
			ChangeHealthBar();
		}
	}

	private void ChangeHealthBar()
	{
		_outputImage.fillAmount = (float)HP / maxHP;
		_outputText.text = HP.ToString();

		if (HP == 0)
			OnHPOver.Invoke();
	}

	public void ReserHP()
	{
		HP = maxHP;
	}
}
