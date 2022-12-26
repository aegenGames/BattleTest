public interface IShield
{
	void SetShield(int value, int shieldDuration);
	int TakeDmg(int dmg);
	void ResetShield();
	void DecreaseDuration();
}
