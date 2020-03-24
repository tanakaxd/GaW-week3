using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Card> cards;

    private Dictionary<Card, int> amountOfEachCard = new Dictionary<Card, int>();

    private void Awake()
    {
        Debug.Log("Awake called");

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        InitInventory();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void InitInventory()
    {
        cards.ForEach((card) =>
        {
            amountOfEachCard[card] = 0;
        });
    }

    public void PlusCardAmount(Card card, int amount)
    {
        if (Engine.instance.matter >= card.currentPrice)
        {
            amountOfEachCard[card] += amount;
            card.amountOwned+=amount;
        }
        //foreach(KeyValuePair<Card,int> keyValuePair in amountOfEachCard)
        //{
        //    Debug.Log(keyValuePair.Key +":"+ keyValuePair.Value);
        //}
    }

    public void MinusCardAmount(Card card, int amount)
    {
        if (card.amountOwned >= 1)
        {
            amountOfEachCard[card] -= amount;
            card.amountOwned-=amount;
        }
        //foreach(KeyValuePair<Card,int> keyValuePair in amountOfEachCard)
        //{
        //    Debug.Log(keyValuePair.Key +":"+ keyValuePair.Value);
        //}
    }
}