using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public TraitDataBase traitDataBase;
    //sceneを新しくする毎にCardスクリプトは新しく作られる？
    //だとするとその都度すべて取得しなおす必要がある。

    private List<Card> cards = new List<Card>();
    private List<Trait> traits;

    private Dictionary<Card, int> amountOfCardsInInventory = new Dictionary<Card, int>();

    private void Awake()
    {
        Debug.Log("Inventory Manager Awake called");

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        traits = new List<Trait>(traitDataBase.GetTraitLists());

        InitInventory();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void InitInventory()
    {
        foreach (Trait trait in traits)
        {
            Card card = GameObject.Find(trait.GetTraitName()).GetComponent<Card>();
            if (card == null)
            {
                Debug.LogError(trait.GetTraitName() + " is invalid name");
            }
            else
            {
                cards.Add(card);
            }

        }

        cards.ForEach((card) =>
        {
            amountOfCardsInInventory[card] = 0;
        });
    }
    /*
    public void ReloadInventory()
    {
        foreach(Card card in cards)
        {
            foreach(Trait trait in traits)
            {
                if(card.)
            }
        }
    }
    */

    public void ActivateCards()
    {
        foreach(Card card in cards)
        {
            card.gameObject.SetActive(true);
        }
    }

    public void DeactivateCards()
    {
        foreach (Card card in cards)
        {
            card.gameObject.SetActive(false);
        }
    }

    public void PlusCardAmount(Card card, int amount)
    {
        Debug.Log(Engine.instance.matter);

        if (Engine.instance.matter >= card.BuyoutPrice)
        {
            amountOfCardsInInventory[card] += amount;
            card.UpdateAmount(amount);
            Engine.instance.matter -= card.BuyoutPrice;
            TopPanelManager.instance.UpdateText();
            Debug.Log("buyout!");

        }
        foreach (KeyValuePair<Card, int> keyValuePair in amountOfCardsInInventory)
        {
            Debug.Log(keyValuePair.Key + ":" + keyValuePair.Value);
        }
    }

    public void MinusCardAmount(Card card, int amount)
    {
        if (card.amountOwned >= 1)
        {
            amountOfCardsInInventory[card] -= amount;
            card.UpdateAmount(-amount);
            Engine.instance.matter += card.SellPrice;
            TopPanelManager.instance.UpdateText();
        }
        foreach (KeyValuePair<Card, int> keyValuePair in amountOfCardsInInventory)
        {
            Debug.Log(keyValuePair.Key + ":" + keyValuePair.Value);
        }
    }

    public void SiphonHumanTriatsToCard(List<Trait> newTraits)
    {
        List<Card> cardAsKey = new List<Card>(amountOfCardsInInventory.Keys);

        foreach (Trait trait in newTraits)
        {
            foreach (Card card in cardAsKey)
            {
                if(trait.GetTraitName()== card.gameObject.name)
                {
                    amountOfCardsInInventory[card]++;
                    Debug.Log(card.name);
                    Debug.Log(amountOfCardsInInventory[card]);
                }
            }
        }
    }

    public void UpdateCardsPrice(Dictionary<Trait,float> currentPrice)
    {

    }
}