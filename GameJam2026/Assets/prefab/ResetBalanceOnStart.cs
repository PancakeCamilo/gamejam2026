using UnityEngine;

public class ResetBalanceOnStart : MonoBehaviour
{
    [Header("Settings")]
    public int DefaultBalance = 1000;

    private void Start()
    {
        ResetBalance();
    }

    public void ResetBalance()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Balance = DefaultBalance;
            Debug.Log($"Balance reset to {DefaultBalance}");
        }
        else
        {
            Debug.LogWarning("GameManager instance not found to reset balance!");
        }
    }
}