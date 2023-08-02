using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private StartState startState;
    [SerializeField] private CountDownState countdownState;
    [SerializeField] private PlayState playState;
    [SerializeField] private EndState endState;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Player player;
    [SerializeField] private PipeManager pipeManager;
    private int score;

    public event System.Action OnScoreAbove5; // Event for score above 5
    public event System.Action OnScoreAbove10; // Event for score above 10
    public event System.Action OnScoreAbove20; // Event for score above 20

    private bool scoreAbove5Handled = false;
    private bool scoreAbove10Handled = false;
    private bool scoreAbove20Handled = false;

    private IState currentState;

    void Awake()
    {
        // Singleton pattern to ensure only one GameManager instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        SetState(startState);
    }

    void Update()
    {
        currentState.UpdateState();
    }

    public void SetState(IState newState)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = newState;
        currentState.EnterState();
    }

    public PlayState GetPlayState()
    {
        return playState;
    }

    public StartState GetStartState()
    {
        return startState;
    }

    public CountDownState GetCountDownStateState()
    {
        return countdownState;
    }

    public EndState GetEndState()
    {
        return endState;
    }

    public TextMeshProUGUI GetTextMeshGUI()
    {
        return textMesh;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public PipeManager GetPipeManager()
    {
        return pipeManager;
    }

    public void SetText(string s)
    {
        textMesh.text = s;
    }

    public void IncrementScore()
    {
        score++;

        // Check the score and trigger events accordingly
        if (score == 6 && !scoreAbove5Handled)
        {
            OnScoreAbove5?.Invoke();
            scoreAbove5Handled = true;
        }
        else if (score == 11 && !scoreAbove10Handled)
        {
            OnScoreAbove10?.Invoke();
            scoreAbove10Handled = true;
        }
        else if (score == 21 && !scoreAbove20Handled)
        {
            OnScoreAbove20?.Invoke();
            scoreAbove20Handled = true;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        scoreAbove5Handled = false;
        scoreAbove10Handled = false;
        scoreAbove20Handled = false;
    }
}