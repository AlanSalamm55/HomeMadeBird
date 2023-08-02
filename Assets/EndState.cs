using UnityEngine;

public class EndState : MonoBehaviour, IState
{
    private float originalTimeScale;

    public void EnterState()
    {
        Time.timeScale = 0f;
        GameManager.Instance.GetPipeManager().StopSpawning();
        GameManager.Instance.SetText("Game Over \n score:" + GameManager.Instance.GetScore() +
                                     "  \n press Enter to go to the main menu");
    }

    public void UpdateState()
    {
        // Check for keyboard Enter key press to transition to PlayState
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.SetState(GameManager.Instance.GetStartState());
        }
    }

    public void ExitState()
    {
        GameManager.Instance.GetPipeManager().ResetPipes();
        GameManager.Instance.GetPipeManager().ResetSpeed();

        Time.timeScale = 1f;
    }
}