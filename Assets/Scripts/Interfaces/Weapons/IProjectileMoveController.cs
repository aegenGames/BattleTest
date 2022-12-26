using System.Collections;

public interface IProjectileMoveController
{
	IEnumerator MoveToTarget(ICharacter target);
}
