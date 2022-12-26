using System.Collections;

public interface IAnimated
{
	void SetAnimationTrigger(string nameTrigger, bool value);
	IEnumerator WaitAnimationState(string nameTrigger, bool value);
}
