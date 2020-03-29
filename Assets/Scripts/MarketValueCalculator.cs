using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketValueCalculator : MonoBehaviour
{
    public Text marketCapitalizationText;
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
        marketCapitalizationText.text = sum.ToString()+"₥";
    }

    private void OnDisable()
    {
        CardManager.instance.onCardAmountChange -= CalculateMarketCapitalization;
    }
}
