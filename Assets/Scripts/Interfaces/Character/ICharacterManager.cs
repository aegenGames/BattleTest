using UnityEngine.Events;

public interface ICharacterManager
{
	UnityAction OnCharacterDied { get; set; }
	void ActivateCharacters();
	void DeactivateCharacters();
	void ResetManager();
}
