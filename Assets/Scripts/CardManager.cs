using System.Collections.Generic;
using UnityEngine;

    //カードを存続させるとUIの取得が面倒
    //カードを存続させないと過去の価格などがキープできない
public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public TraitDataBase traitDataBase;
    //sceneを新しくする毎にCardスクリプトは新しく作られる？
    //だとするとその都度すべて取得しなおす必要がある。

    private List<Card> cards = new List<Card>();
    private List<Trait> traits;

    private Dictionary<Trait, int> amountOfCardsInInventory = new Dictionary<Trait, int>();

    #region event
    public delegate void OnCardAmountChange();
    public event OnCardAmountChange onCardAmountChange;
    #endregion



    private void Awake()
    {
        //Debug.Log("Inventory Manager Awake called");

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
        traits = new List<Trait>(traitDataBase.GetTraitLists());
        onCardAmountChange+= UpdateSympathy;
        InitInventory();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //onCardAmountChange+= TopPanelManager.instance.UpdateText;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void InitInventory()
    {
        foreach (Trait trait in traits)
        {
            amountOfCardsInInventory[trait] = 0;
        }
    }
    
   
    public void GetAllCardScriptsInScene() //hierarchy上のカードオブジェクトのスクリプトをすべて取得
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
    }

    public void DiscardAllCardsScripts()
    {
        cards = new List<Card>();
}

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
        if (Engine.instance.matter >= card.BuyoutPrice)
        {
            amountOfCardsInInventory[card.trait] += amount;
            card.ChangeAmount(amount);
            Engine.instance.matter -= card.BuyoutPrice;
            onCardAmountChange();
            Debug.Log("buyout!");

        }
        //foreach (KeyValuePair<Trait, int> keyValuePair in amountOfCardsInInventory)
        //{
        //    Debug.Log(keyValuePair.Key + ":" + keyValuePair.Value);
        //}
    }

    public void MinusCardAmount(Card card, int amount)
    {
        if (card.amountOwned >= 1)
        {
            amountOfCardsInInventory[card.trait] -= amount;
            card.ChangeAmount(-amount);
            Engine.instance.matter += card.SellPrice;
            onCardAmountChange();
        }
        //foreach (KeyValuePair<Trait, int> keyValuePair in amountOfCardsInInventory)
        //{
        //    Debug.Log(keyValuePair.Key + ":" + keyValuePair.Value);
        //}
    }

    public void ConsumeCard(Card card, int amount)
    {
        if (card.amountOwned >= 1)
        {
            amountOfCardsInInventory[card.trait] -= amount;
            card.ChangeAmount(-amount);
            Engine.instance.LifeEnergy += card.trait.GetTraitEnergy();
            onCardAmountChange();
        }
    }

    public void SiphonHumanTraitsToCard(List<Trait> newTraits)
    {
        List<Trait> keys = new List<Trait>(amountOfCardsInInventory.Keys);

        foreach (Trait newTrait in newTraits)
        {
            foreach(Trait trait in keys)
            {
                if(trait.GetTraitName()== newTrait.GetTraitName())
                {
                    amountOfCardsInInventory[trait]++;
                    onCardAmountChange();
                    Debug.Log(trait.GetTraitName());
                    Debug.Log(amountOfCardsInInventory[trait]);
                }

            }
        }
    }

    public void UpdateCardsPrice(Dictionary<Trait,float> currentPrice)//最新の価格に反映して、過去のデータも与える
    {
        Debug.Log("UpdateCardsPrice");

        foreach (Card card in cards)
        {
            foreach(Trait trait in traits)
            {
                if (card.gameObject.name == trait.GetTraitName())
                {
                    card.UpdatePrice(currentPrice[trait]);
                    card.SetPriceHistory(EconomyManager.instance.pastPrice[trait]);
                    Debug.Log("UpdateCardsPrice");
                }
            }
        }
    }

    public void UpdateAllAmount()
    {
        foreach(Card card in cards)
        {
            card.UpdateAmount(amountOfCardsInInventory[card.trait]);
        }
        onCardAmountChange();
    }

    public void UpdateSympathy()
    {
        //カードの保有量が変われば必ずsympathyも変わる
        //matterとenergyは変わるとは限らない
        float sympathyPoint = 0;
        foreach(Trait trait in traits)
        {
            sympathyPoint += amountOfCardsInInventory[trait] * EconomyManager.instance.currentPrice[trait];
        }

        Engine.instance.sympathy = sympathyPoint;
        TopPanelManager.instance.UpdateText();
    }
}
