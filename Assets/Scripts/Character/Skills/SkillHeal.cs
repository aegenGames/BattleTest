using UnityEngine;

public class SkillHeal : BaseSkill
{
	[SerializeField]
	[Min(1)]
	private int _valueHealing = 1;

	public override void StartSkill(Character target)
	{
		base.StartSkill(target);
		target.TakeHeal(_valueHealing);
		target.ApplyEffects(_effects);
		OnSkillFinished.Invoke();
	}
}
