using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinButton : MonoBehaviour
{
    [Header("Settings")]
    public int RequiredBalance = 1000000;
    public string WinSceneName = "win";

    [Header("UI References")]
    public Button BuyWinButton;
    public TextMeshProUGUI StatusText;

    private void Start()
    {
        if (BuyWinButton != null)
        {
            BuyWinButton.onClick.AddListener(CheckWinCondition);
        }
    }

    public void CheckWinCondition()
    {
        if (GameManager.Instance != null && GameManager.Instance.Balance >= RequiredBalance)
        {
            SceneManager.LoadScene(WinSceneName);
        }
        else
        {
            if (StatusText != null)
            {
                StatusText.text = "Insufficient Balance!";
            }
            Debug.Log("Insufficient Balance to pay off debt!");
        }
    }   
}
