using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
    public TraitDataBase traitDataBase;

    public List<Human> humen;

    private List<Trait> traits;
    private int minimunTraits = 3;
    private int maximamTraits = 9;
    private int averageTraits = 4;
    private float averageIncrement = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        traits = new List<Trait>(traitDataBase.GetTraitLists());

        for (int i = 0; i < humen.Count; i++)
        {
            GenerateHuman(humen[i]);
            humen[i].AnalyzeHuman();
            humen[i].Display();

        }

        ActivateAllHumen();


    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActivateAllHumen()
    {
        foreach(Human human in humen)
        {
            human.gameObject.SetActive(true);
        }
    }


    void GenerateHuman(Human human)
    {
        int randomNumber = (int)MyRandom.RandomGaussianUnity(averageTraits+(Engine.instance.playerLevel-1)* averageIncrement,1);
        randomNumber = Mathf.Clamp(randomNumber,minimunTraits, maximamTraits);
        Debug.Log(randomNumber);

        int count = 0;
        while (true)
        {
            if (randomNumber == human.traits.Count || human.traits.Count==traits.Count)
            {
                break;
            }

            Trait potentialTrait = traits[Random.Range(0, traits.Count)];
            if (potentialTrait.GetRarityForAppearanceProbability() >= Random.Range(0f, 1f))
            {
                human.traits.Add(potentialTrait);
            }

            count++;

            if (count > 300)
            {
                break;
            }
        }
    }


    //inspectorから登録する関数
    public void ReloadHumen()
    {
        if (Engine.instance.matter >= 100)
        {
            for (int i = 0; i < humen.Count; i++)
            {
                NullifyHuman(humen[i]);
                GenerateHuman(humen[i]);
                humen[i].AnalyzeHuman();
                humen[i].Display();


            }
            ActivateAllHumen();
            Engine.instance.matter -= 100;
            TopPanelManager.instance.UpdateText();
        }
    }

    private void NullifyHuman(Human human)
    {
        human.InitHuman();
    }

    //void DisplayHuman()
    //{

    //}
}
