using UnityEngine;

public class PlayState : MonoBehaviour, IState
{
    private Player player;

    public void EnterState()
    {
        player = GameManager.Instance.GetPlayer();

        GameManager.Instance.ResetScore();
        GameManager.Instance.SetText(GameManager.Instance.GetScore().ToString());
        player.MakeControllable();
        GameManager.Instance.GetPipeManager().StartSpawning();


        GameManager.Instance.OnScoreAbove5 += HandleScoreAbove5;
        GameManager.Instance.OnScoreAbove10 += HandleScoreAbove10;
        GameManager.Instance.OnScoreAbove20 += HandleScoreAbove20;
        player.OnPassRightSidePipe += HandlePlayerPassRightSidePipe;
    }

    public void UpdateState()
    {
        GameManager.Instance.SetText(GameManager.Instance.GetScore().ToString());


        if (GameManager.Instance.GetPlayer().IsCollidingWithWall())
        {
            GameManager.Instance.SetState(GameManager.Instance.GetEndState());
        }
    }


    private void HandlePlayerPassRightSidePipe()
    {
        // Increment the score when the player passes by the right side of a pipe
        GameManager.Instance.IncrementScore();
    }

    // Event handler for score above 5
    private void HandleScoreAbove5()
    {
        GameManager.Instance.GetPipeManager().IncreaseSpeed();
    }

    // Event handler for score above 10
    private void HandleScoreAbove10()
    {
        GameManager.Instance.GetPipeManager().IncreaseSpeed();
    }

    // Event handler for score above 20
    private void HandleScoreAbove20()
    {
        GameManager.Instance.GetPipeManager().IncreaseSpeed();
    }

    public void ExitState()
    {
        if (player != null)
        {
            player.OnPassRightSidePipe -= HandlePlayerPassRightSidePipe;
            player = null;
        }


        GameManager.Instance.OnScoreAbove5 -= HandleScoreAbove5;
        GameManager.Instance.OnScoreAbove10 -= HandleScoreAbove10;
        GameManager.Instance.OnScoreAbove20 -= HandleScoreAbove20;

    }
}