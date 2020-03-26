using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;
    public TraitDataBase traitDataBase;

    private List<Trait> traits;
    private float baseIncrease=0.5f;
    private float upwardProbability=0.7f;

    //この部分が冗長？
    //何個も配列を作るくらいなら例えばCardオブジェクトにパラメータとして個別に持たせる？
    [HideInInspector]public Dictionary<Trait, float> currentPrice;
    [HideInInspector]public Dictionary<Trait, List<float>> pastPrice;
    private Dictionary<Trait, float> priceModifier;
    private Dictionary<Trait, List<bool>> priceOscillation;

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
        traits = new List<Trait>(traitDataBase.GetTraitLists());
        currentPrice = new Dictionary<Trait, float>();
        pastPrice = new Dictionary<Trait, List<float>>();
        priceModifier = new Dictionary<Trait, float>();
        priceOscillation = new Dictionary<Trait, List<bool>>();
        foreach (Trait trait in traits)
        {
            pastPrice[trait] = new List<float>();

            currentPrice[trait] = trait.GetTraitBaseValue();
            priceModifier[trait] = 1;
        }
        GenerateOscillation();
    }
    // Start is called before the first frame update
    void Start()
    {
        //MyDebug.Dictionary(currentPrice);
        //foreach(KeyValuePair<Trait,List<bool>> keyValuePair in priceOscillation)
        //{
        //    Debug.Log(keyValuePair.Key);
        //    Debug.Log(keyValuePair.Value.Count);
        //    MyDebug.List(keyValuePair.Value);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateOscillation()
    {
        foreach (Trait trait in traits)
        {
            priceOscillation[trait] = new List<bool>();

            float frequency = trait.GetTraitOscillationFrequency();
            bool direction = UnityEngine.Random.Range(0f,1.0f)<upwardProbability? true:false;//starting upper direction

            for (int i = 0; i < 30; i++)//30=arbitrary
            {
                if (priceOscillation[trait].Count >= 30)
                    break;
                //double num = RandomGaussian(frequency, 1);
                //Debug.Log(num);
                double numUnity = RandomGaussianUnity(frequency, 1);
                //Debug.Log(numUnity);
                int rows = (int)numUnity;
                //Debug.Log(rows);
                rows = Mathf.Max(1, rows);
                //Debug.Log(UnityEngine.Random.Range(0f,1f));
                for (int j = 0; j < rows; j++)
                {
                    //Debug.Log(priceOscillation[trait]);
                    //MyDebug.List(priceOscillation[trait]);
                    priceOscillation[trait].Add(direction);
                }
                direction = !direction;
            }
        }
    }

    public Dictionary<Trait,float> CalculateCurrentPrice()
    {
        //List<Trait> traits = new List<Trait>(currentPrice.Keys);
        foreach(Trait trait in traits)
        {
            pastPrice[trait].Add(currentPrice[trait]);

            float price = currentPrice[trait];
            Debug.Log("start"+price);
            float direction = priceOscillation[trait][Engine.instance.day-1]? 1:-1;
            Debug.Log(direction);

            float deltaValue = (float)RandomGaussian(trait.GetTraitValueVolatility(),1);
            Debug.Log(deltaValue);

            deltaValue = Mathf.Max(0, deltaValue);
            Debug.Log(deltaValue);

            price += direction * deltaValue;
            price+=baseIncrease;
            Debug.Log("end"+price);

            currentPrice[trait] = price;
        }
        return currentPrice;
    }

    public void ApplyCurrentPrice()
    {

    }

    internal void ValueMore(Trait trait)
    {
        priceModifier[trait] += 0.1f;
    }

    public static double RandomGaussian(float average, float sigma)
    {
        var rnd = new System.Random();

        double X, Y;
        double Z1;

        X = rnd.NextDouble();
        Y = rnd.NextDouble();

        Z1 = sigma * Math.Sqrt(-2.0 * Math.Log(X)) * Math.Cos(2.0 * Math.PI * Y)+average;
        //Z2 = Math.Sqrt(-2.0 * Math.Log(X)) * Math.Sin(2.0 * Math.PI * Y);

        return Z1;
    }

    public static double RandomGaussianUnity(float average, float sigma)
    {
        double X, Y;
        double Z1;

        X = UnityEngine.Random.value;
        Y = UnityEngine.Random.value;
        //Debug.Log(X);
        //Debug.Log(Y);

        Z1 = sigma * Math.Sqrt(-2.0 * Math.Log(X)) * Math.Cos(2.0 * Math.PI * Y) + average;
        //Z2 = Math.Sqrt(-2.0 * Math.Log(X)) * Math.Sin(2.0 * Math.PI * Y);

        return Z1;
    }
}

