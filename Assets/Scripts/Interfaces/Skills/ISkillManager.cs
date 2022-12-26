public interface ISkillManager : IMovementSkill, IAnimatedSkill
{
	public ICharacter SelfTarget { get; set; }
	public void ActivateTrigger();
	public void DeactivatedTrigger();
	public void ResetManager();
}
