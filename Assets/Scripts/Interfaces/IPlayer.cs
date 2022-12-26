using UnityEngine.Events;

public interface IPlayer
{
    static bool IsBusy { get; set; }
    bool IsActive { get; set; }
    UnityAction OnTurnOver { get; set; }
    UnityAction OnStartTurn { get; set; }
    UnityAction<string> OnLoss { get; set; }
    void EndTurn();
    void ResetPlayer();
}
