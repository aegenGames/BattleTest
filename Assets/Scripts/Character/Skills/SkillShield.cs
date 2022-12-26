using UnityEngine;

public class SkillShield : BaseSkill
{
	[SerializeField]
	[Min(1)]
	private int _protectionValue = 2;
	[SerializeField]
	[Min(1)]
	private int _duration = 3;

	public override void StartSkill(ICharacter target)
	{
		base.StartSkill(target);
		target.TakeShield(_protectionValue, _duration);
		OnSkillFinished.Invoke();
	}
}
