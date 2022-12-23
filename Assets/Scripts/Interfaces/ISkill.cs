using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public interface ISkill
{
	UnityEvent OnSkillFinished { get; }
	Sprite IconSprite { get; }
	LayerMask TargetLayer { get; }
	void StartSkill(Character target); 
	void BreakSkill();

	UnityAction<string, bool> AttackAnimationEvent { get; set; }
	delegate IEnumerator WaitAnimationStateHandler(string nameState, bool value);
	event WaitAnimationStateHandler WaitAnimationStateEvent;
	delegate IEnumerator ReturnOnStartPositionHandler();
	event ReturnOnStartPositionHandler ReturnOnStartPositionEvent;
	delegate IEnumerator MoveToEnemyHandler(Transform target, float distanceForAttack);
	event MoveToEnemyHandler MoveToEnemyEvent;
}
