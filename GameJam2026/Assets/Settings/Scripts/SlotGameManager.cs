using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlotGameManager : MonoBehaviour
{
    [Header("Reels")]
    public Reel[] reels;

    [Header("UI")]
    public Button SpinButton;
    public Button AddButton; //increase bet amount
    public Button SubtractButton;// reduce bet amount
    public TextMeshProUGUI BalanceText;
    public TextMeshProUGUI BetText;
    public TextMeshProUGUI StatusText;

    [Header("Balance")]
    public int StartingBalance = 1000;   // used if there's no GameManager in the scene
    private int localBalance;

    public int Balance
    {
        get => GameManager.Instance != null ? GameManager.Instance.Balance : localBalance;
        set
        {
            if (GameManager.Instance != null) GameManager.Instance.Balance = value;
            else localBalance = value;
        }
    }

    [Header("Game Values")]
    public int CurrentBet = 10;
    public int MinBet = 10;              // becomes the last bet made
    public int BetIncreasePerSpin = 5;

    private void Awake()
    {
        localBalance = StartingBalance;
    }

    private void Start()
    {
        if (GameManager.Instance == null)
            Debug.LogWarning("No GameManager in scene - using StartingBalance instead.", this);

        UpdateUI();
        SpinButton.onClick.AddListener(StartSpin);
        AddButton.onClick.AddListener(IncreaseBet);
        SubtractButton.onClick.AddListener(DecreaseBet);
    }

    void UpdateUI()
    {
        if (BalanceText != null) BalanceText.text = $"Balance: {Balance}";
        if (BetText != null) BetText.text = $"Bet: {CurrentBet} (Min: {MinBet})";
    }

    public void IncreaseBet()
    {
        if (CurrentBet + 10 <= Balance)
        {
            CurrentBet += 10;
            UpdateUI();
        }
    }

    public void DecreaseBet()
    {
        if (CurrentBet - 10 >= MinBet)
        {
            CurrentBet -= 10;
            UpdateUI();
        }
    }

    public void StartSpin()
    {
        if (Balance < CurrentBet)
        {
            StatusText.text = "Insufficient Balance!";
            if (Balance > 0)
            {
                SceneManager.LoadScene("lose");
            }

            return;
        }
        Balance -= CurrentBet;
        UpdateUI();
        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        ToggleButtons(false);
        StatusText.text = "Spinning...";
        for (int i = 0; i < reels.Length; i++)
        {
            StartCoroutine(reels[i].SpinReel(1.0f + (i * 0.5f)));
        }
        foreach (var reel in reels)
        {
            yield return new WaitUntil(() => !reel.IsSpinning);
        }
        CheckWin();
        RaiseMinBet();
        UpdateUI();
        ToggleButtons(true);
    }

    private void CheckWin()
    {
        int r1 = reels[0].ResultIndex;
        int r2 = reels[1].ResultIndex;
        int r3 = reels[2].ResultIndex;
        if (r1 == r2 && r2 == r3)
        {
            int winAmount = CurrentBet * 10;
            Balance += winAmount;
            StatusText.text = $"JACKPOT! You Win! +{winAmount}";
        }
        else if (r1 == r2 || r2 == r3 || r1 == r3)
        {
            int winAmount = CurrentBet * 2;
            Balance += winAmount;
            StatusText.text = $"Pair! Won! +{winAmount}";
        }
        else
        {
            StatusText.text = "You Lose! Try Again";
        }

        UpdateUI();

        if (Balance <= 0)
        {
            SceneManager.LoadScene("lose");
        }
    }

    void RaiseMinBet()
    {
        int lastBet = CurrentBet;

        MinBet = lastBet;                          // can't go below last bet
        CurrentBet = lastBet + BetIncreasePerSpin; // auto +5 for next round

        if (Balance < CurrentBet)
            StatusText.text += $"\nCan't afford next bet ({CurrentBet})!";
    }

    void ToggleButtons(bool state)
    {
        SpinButton.interactable = state;
        AddButton.interactable = state;
        SubtractButton.interactable = state;
    }
}