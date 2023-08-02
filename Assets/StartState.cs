using UnityEngine;

public class StartState : MonoBehaviour, IState
{
    public void EnterState()
    {
        GameManager.Instance.GetTextMeshGUI().gameObject.SetActive(true);
        GameManager.Instance.GetPlayer().gameObject.SetActive(false);
        GameManager.Instance.GetPlayer().ResetPlayer();
        GameManager.Instance.GetPlayer().Freeze();
        GameManager.Instance.SetText("Press Enter To Play");
    }

    public void UpdateState()
    {
        // Check for keyboard Enter key press to transition to PlayState
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.SetState(GameManager.Instance.GetCountDownStateState());
        }
    }

    public void ExitState()
    {
        GameManager.Instance.GetPlayer().gameObject.SetActive(true);

    }
}