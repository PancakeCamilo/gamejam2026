using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("Buttons")]
    public Button BuyFood; // 10,000 item
    public Button BuyEvil; // 100,000 item
    public Button BuyDebt;

    [Header("UI")]
    public TextMeshProUGUI BalanceText;

    public int Balance
    {
        get => GameManager.Instance != null ? GameManager.Instance.Balance : 0;
        set
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Balance = value;
            }
        }
    }

    private void Start()
    {
        // 1. Hook up button listeners ONCE when the scene loads
        if (BuyFood != null) BuyFood.onClick.AddListener(BuyCheck1);
        if (BuyEvil != null) BuyEvil.onClick.AddListener(BuyCheck2);

        // 2. Refresh UI text on start
        UpdateUI();
    }

    public void UpdateUI()
    {
        // 3. Null-check ensures Unity won't crash if BalanceText isn't assigned yet
        if (BalanceText != null)
        {
            BalanceText.text = $"Balance: {Balance}";
        }
    }

    public void BuyCheck1()
    {
        if (Balance >= 10000)
        {
            Balance -= 10000;
            UpdateUI();
        }
    }

    /* public void BuyCheck2()
    {
        if (Balance >= 100000)
        {
            Balance -= 100000;
            UpdateUI();
        }
    }
}
