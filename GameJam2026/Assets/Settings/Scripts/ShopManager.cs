using TMPro;
using Unity.Android.Types;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("UI")]
    public TextMeshProUGUI BalanceText;


    public int Balance
    {
        get => GameManager.Instance.Balance;
    }


    void UpdateUI()
    {
        BalanceText.text = $"Balance: {Balance}";
    }

    /*private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/




    private void Start()
    {
        UpdateUI();
    }

    
    void Update()
    {
        
    }
}
