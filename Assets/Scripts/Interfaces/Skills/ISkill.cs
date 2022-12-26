using UnityEngine;
using UnityEngine.Events;

public interface ISkill
{
	UnityEvent OnSkillFinished { get; }
	Sprite IconSprite { get; }
	LayerMask TargetLayer { get; }
	void StartSkill(ICharacter target);
	void BreakSkill();
}
