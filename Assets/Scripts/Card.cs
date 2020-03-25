using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Trait trait;
    public TextMeshProUGUI description;
    public GameObject dealPanel;
    public TextMeshProUGUI dealPanelDescription;

    //public DealPanelManager dealPanelManager;
    public Button buyButton;
    public Button sellButton;
    public Button closeButton;

    [HideInInspector] public float currentPrice = 100;
    private float buyoutModifier = 1.1f;
    private float sellModifier = 0.9f;
    public float BuyoutPrice { get { return currentPrice * buyoutModifier; } }
    public float SellPrice { get { return currentPrice * sellModifier; } }
    private List<float> pastPrices = new List<float>();

    //private float yesterdayPrice;
    // private float dayBeforeYesterdayPrice;
    [HideInInspector]
    public int amountOwned = 0;

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Display();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Display()
    {
        string text = trait.GetTraitName() + "\n" + trait.GetRarityOfTrait() + "\n";
        text += currentPrice + "\n" + "amount: " + amountOwned;
        description.text = text;
    }

    public void DisplayDetail()
    {
        string text = "Card Name: " + trait.GetTraitName() + "\n" + "Rarity: " + trait.GetRarityOfTrait() + "\n";
        text += "Current Price: " + currentPrice;
        //+ "\n" + "昨日の価格: " + pastPrices[pastPrices.Count]
        //+ "\n" + "おとといの価格: " + pastPrices[pastPrices.Count] + "\n";
        dealPanelDescription.text = text;

        RegisterDeal();
        RegisterClose();

        dealPanel.SetActive(true);

    }

    public void UpdatePrice(float price)
    {
        pastPrices.Add(currentPrice);
        currentPrice = price;
        Display();
    }

    public void UpdateAmount(int amount)
    {
        if (amountOwned + amount >= 0)
        {
            amountOwned += amount;
            Display();
        }
        else
        {
            Debug.Log("invalid amount");
        }
    }

    private void RegisterDeal()
    {
        buyButton.onClick.AddListener(() =>
        {
            //invoke a event?
            //Broker内部でWhenCardBoughtを宣言しておく
            //Broker.instance.WhenCardBought?.Invoke();

            //だけどパネルを開いたときにその都度ボタンにイベントを登録する必要がある。
            //イベントの内容がそれぞれのカードに依存している
            //ボタンオブジェクトは一つしかない。それを使いまわす

            CardManager.instance.PlusCardAmount(this, 1);
        });
        sellButton.onClick.AddListener(() =>
        {
            CardManager.instance.MinusCardAmount(this, 1);
        });
    }

    private void RegisterClose()
    {
        closeButton.onClick.AddListener(() =>
        {
            dealPanel.SetActive(false);
            buyButton.onClick.RemoveAllListeners();
            sellButton.onClick.RemoveAllListeners();
        });
    }
}