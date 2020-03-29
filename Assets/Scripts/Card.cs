using System;
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
    public LineRenderer chartLine;

    public Button buyButton;
    public Button sellButton;
    public Button consumeButton;
    public Button closeButton;

    [HideInInspector] public float currentPrice = 100; 
    [HideInInspector] public float currentSympathy = 100; 
    private float buyoutModifier = 1.01f;
    private float sellModifier = 0.99f;
    public float BuyoutPrice { get { return currentPrice * buyoutModifier ; } }//* trait.GetRarityForFloatValue()
    public float SellPrice { get { return currentPrice * sellModifier ; } }//* trait.GetRarityForFloatValue()
    //public float Sympathy { get { return currentPrice * EconomyManager.instance.sympathyModifier[trait]; } }
    private List<float> pastPrices = new List<float>();

    //private float yesterdayPrice;
    // private float dayBeforeYesterdayPrice;
    [HideInInspector]
    public int amountOwned = 0;

    // Start is called before the first frame update
    private void Start()
    {
        Display();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Display()
    {
        string text = trait.GetTraitName() + "\n\n" + Enum.GetName(typeof(RarityOfTrait), trait.GetRarityOfTrait()) + "\n";
        text += "E:"+trait.GetTraitEnergy() + "\n" + "P:"+currentPrice+"₥" + "\n" +"S:"+currentSympathy+ "\n" + "Owned: " + amountOwned;
        description.text = text;
    }

    public void DisplayDetail()    //inspectorから登録する関数
    {
        //MyDebug.List(pastPrices);
        string text = "Card Name: " + trait.GetTraitName().ToUpper() + "\n" + "Rarity: " + trait.GetRarityOfTrait().ToString() + "\n\n";
        text += "When Consumed:\n" +"   Life Energy: " + trait.GetTraitEnergy() + "\n" + "   Sympathy: " + currentSympathy + "\n\n";
        text += "Current Price: " + currentPrice + "\n" + "   Buy Price: " + BuyoutPrice + "\n" + "   Sell Price: " + SellPrice+"\n";
        int dayBefore = 1;
        for (int i = pastPrices.Count; i > 0 &&i> pastPrices.Count-3; i--)
        {
            text += "\n" + "Price "+dayBefore + " day before: " + pastPrices[i-1];
            dayBefore++;
        }
        dealPanelDescription.text = text;

        //Debug.DrawLine()グラフが書ける？Updateでフレームごとに呼ぶ必要あり
        DrawChart();

        RegisterDeal();
        RegisterClose();

        dealPanel.SetActive(true);
    }

    private void DrawChart()
    {
        chartLine.positionCount = pastPrices.Count+1;//SetPositionsを使う前に必須
        Vector3[] positions = new Vector3[pastPrices.Count+1];
        for (int i = 0; i < pastPrices.Count; i++)
        {
            Vector3 position = new Vector3(i * 10, pastPrices[i], 0);
            positions[i]=position;
        }
        positions[positions.Length-1] = new Vector3((positions.Length - 1) * 10, currentPrice, 0);
        chartLine.SetPositions(positions);
    }

    public void UpdatePrice(float[] prices)//prices[0]:modifiedPrice, prices[1]:modifiedSympathy
    {
        //pastPrices.Add(currentPrice);
        currentPrice = prices[0];
        currentSympathy = prices[1];
        Display();
    }

    public void UpdateAmount(int amount)
    {
        amountOwned = amount;
        Display();
    }

    public void ChangeAmount(int amount)
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

        consumeButton.onClick.AddListener(() =>
        {
            CardManager.instance.ConsumeCard(this, 1);
        });
    }

    private void RegisterClose()
    {
        closeButton.onClick.AddListener(() =>
        {
            dealPanel.SetActive(false);
            buyButton.onClick.RemoveAllListeners();
            sellButton.onClick.RemoveAllListeners();
            consumeButton.onClick.RemoveAllListeners();
        });
    }

    public void SetPriceHistory(List<float> priceHistory)
    {
        pastPrices = priceHistory;
    }

    private void OnDisable()
    {
        EconomyManager.instance.AddTodaysPrice(trait, currentPrice);
    }
}