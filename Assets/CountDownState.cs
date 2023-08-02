using TMPro;
using UnityEngine;

public class CountDownState : MonoBehaviour, IState
{
    private int countdownValue = 3;
    private float timeSinceLastCountdown = 0f;
    private TextMeshProUGUI textMesh;

    public void EnterState()
    {
        textMesh = GameManager.Instance.GetTextMeshGUI();
        UpdateCountdownText();
    }

    public void UpdateState()
    {
        timeSinceLastCountdown += Time.unscaledDeltaTime;

        // Check if it's time to update the countdown text
        if (timeSinceLastCountdown >= 1f)
        {
            countdownValue--;
            UpdateCountdownText();
            timeSinceLastCountdown = 0f;

            // If countdown is complete, transition to PlayState
            if (countdownValue <= 0)
            {
                GameManager.Instance.SetState(GameManager.Instance.GetPlayState());
                countdownValue = 3;

            }
        }
    }

    public void ExitState()
    {
    }

    private void UpdateCountdownText()
    {
        if (countdownValue > 0)
        {
            textMesh.text = countdownValue.ToString();
        }
        else
        {
            textMesh.text = "Go!";
        }
    }
    
}