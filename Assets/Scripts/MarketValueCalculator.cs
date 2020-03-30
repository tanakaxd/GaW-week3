using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketValueCalculator : MonoBehaviour
{
    public Text marketCapitalizationText;
    public Text grossWealthText;

    private float marketCapitalization = 0;
    private float grossWealth = 0;
    // Start is called before the first frame update
    void Start()
    {
        CalculateMarketCapitalization();
        CardManager.instance.onCardAmountChange += CalculateMarketCapitalization;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CalculateMarketCapitalization()
    {
        float sum = 0;
        foreach(Card card in CardManager.instance.cards)
        {
            sum += card.amountOwned * card.currentPrice;
        }
        marketCapitalization = sum;
        marketCapitalizationText.text = sum.ToString()+"₥";

        CalculateGrossWealth();
    }

    private void CalculateGrossWealth()
    {
        grossWealth = marketCapitalization + Engine.instance.matter;
        grossWealthText.text = grossWealth.ToString() + "₥";

    }

    private void OnDisable()
    {
        CardManager.instance.onCardAmountChange -= CalculateMarketCapitalization;
    }
}
