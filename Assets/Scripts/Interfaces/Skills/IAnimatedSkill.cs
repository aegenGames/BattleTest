using System.Collections;
using UnityEngine.Events;

public interface IAnimatedSkill
{
	UnityAction<string, bool> SkillAnimationEvent { get; set; }
	delegate IEnumerator WaitAnimationStateHandler(string nameState, bool value);
	event WaitAnimationStateHandler WaitAnimationStateEvent;
}