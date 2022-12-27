using UnityEngine;

public class SkillHeal : BaseSkill
{
	[SerializeField]
	[Min(1)]
	private int _valueHealing = 1;

	public override void StartSkill(ICharacter target)
	{
		base.StartSkill(target);
		target.TakeHeal(_valueHealing);
		IEffectable targetEffects = target as IEffectable;
		targetEffects?.ApplyEffects(_effects);
		OnSkillFinished.Invoke();
	}
}
