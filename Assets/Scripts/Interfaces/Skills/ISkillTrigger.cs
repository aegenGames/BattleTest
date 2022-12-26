using UnityEngine.Events;

public interface ISkillTrigger : IGameObjectli
{
	ICharacter SelfTarget { get; set; }
	UnityAction<ICharacter> OnHitObject { get; set; }
	void SetSkill(ISkill skill);
	void HitObject(ICharacter target);
}
