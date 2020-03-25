using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;
    public TraitDataBase traitDataBase;

    private List<Trait> traits; 
    private Dictionary<Trait, float> currentPrice;
    private Dictionary<Trait, float> priceModifier;
    private Dictionary<Trait, bool[]> priceOscillation;

    private void Awake()
    {
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
    void Start()
    {
        traits = new List<Trait>(traitDataBase.GetTraitLists());
        foreach(Trait trait in traits)
        {
            priceModifier[trait] = 1;
            currentPrice[trait] = trait.GetTraitBaseValue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<Trait,float> CalculateCurrentPrice()
    {
        //List<Trait> traits = new List<Trait>(currentPrice.Keys);
        foreach(Trait trait in traits)
        {
            float price = currentPrice[trait];
            price+=baseIncrease;
            float direction = priceOscillation[trait][Engine.instance.day]? 1:-1;
            price+=direction*trait.GetTraitValueVolatility()*

        }
        currentPrice
        return currentPrice;
    }

    public void ApplyCurrentPrice()
    {

    }

    internal void ValueMore(Trait trait)
    {
        priceModifier[trait] += 0.1f;
    }
}
