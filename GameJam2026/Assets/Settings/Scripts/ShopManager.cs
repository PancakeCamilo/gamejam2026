using TMPro;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public Button BuyFood;
    public Button BuyEvil;
    public Button BuyDebt;

    [Header("UI")]
    public TextMeshProUGUI BalanceText;


    public int Balance
    {
        get => GameManager.Instance.Balance;
    }


    void UpdateUI()
    {
        BalanceText.text = $"Balance: {Balance}";
        BuyFood.onClick.AddListener(BuyCheck1);
        BuyFood.onClick.AddListener(BuyCheck2);
    }
    public void BuyCheck1()
    {
        if (Balance >= 10000)
        {
             GameManager.Instance.Balance -= 10000;
        }
        else
        {

        }
    }

    public void BuyCheck2()
    {
        if (Balance >= 100000)
        {
             = Balance - 100000;
        }
        else
        {

        }
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
        GameManager BalanceToTake = GetComponent<GameManager>();

    }


    void Update()
    {
        
    }
}
