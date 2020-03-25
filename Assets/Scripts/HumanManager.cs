using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
    public TraitDataBase traitDataBase;

    public List<Human> humen;

    private List<Trait> traits;
    private int minimunTraits = 5;
    private int maximamTraits = 8;
    // Start is called before the first frame update
    void Start()
    {
        traits = new List<Trait>(traitDataBase.GetTraitLists());

        for (int i = 0; i < humen.Count; i++)
        {
            GenerateHuman(humen[i]);
            //DisplayHuman();

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
        int randomNumber = Random.Range(minimunTraits,maximamTraits);
        int count = 0;
        while (true)
        {
            if (randomNumber == human.traits.Count || human.traits.Count==traits.Count)
            {
                break;
            }

            human.traits.Add(traits[Random.Range(0, traits.Count - 1)]);

            count++;

            if (count > 100)
            {
                break;
            }
        }
    }


    //inspectorから登録する関数
    public void ReloadHumen()
    {
        for (int i = 0; i < humen.Count; i++)
        {
            NullifyHuman(humen[i]);
            GenerateHuman(humen[i]);
            //DisplayHuman();
            

        }
        ActivateAllHumen();
    }

    private void NullifyHuman(Human human)
    {
        human.traits = new List<Trait>();
    }

    //void DisplayHuman()
    //{

    //}
}
