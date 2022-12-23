using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
	[Header("DIsplay shield")]
	[SerializeField]
	private Image _outputImage;
	[SerializeField]
	private TextMeshProUGUI _outputText;
	[SerializeField]
	[Range(0, 1)]
	private float _ammountRange = 0.25f;

	[Header("Shield settings")]
	[Min(1)]
	[SerializeField]
	private int _maxShield = 2;

	private int _shield = 0;
	private int _duration = 0;

	private int Shield
	{
		get => _shield;
		set
		{
			_shield = value;
			ChangeShieldBar();
		}
	}

	public void SetShield(int value, int shieldDuration)
	{
		Shield = value;
		_duration = shieldDuration;
	}

	public int TakeDmg(int dmg)
	{
		if (Shield == 0)
			return dmg;

		if (Shield > dmg)
		{
			Shield -= dmg;
			dmg = 0;
		}
		else
		{
			dmg -= Shield;
			Shield = 0;
		}

		return dmg;
	}

	private void ChangeShieldBar()
	{
		_outputImage.fillAmount = _ammountRange * Shield / _maxShield;
		if (Shield == 0)
		{
			_outputText.gameObject.SetActive(false);
			_outputText.text = Shield.ToString();
		}
		else
		{
			_outputText.gameObject.SetActive(false);
		}
	}

	public void ResetShield()
	{
		Shield = 0;
	}

	public void DecreaseDuration() {
		if (--_duration == 0)
			ResetShield();
	}
}