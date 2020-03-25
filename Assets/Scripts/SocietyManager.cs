using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocietyManager : MonoBehaviour
{
    public SocietyDataBase societyDataBase;

    private List<Society> societies;
    private int societiesPerDay=3;

    // Start is called before the first frame update
    void Start()
    {
        societies = new List<Society>(societyDataBase.GetSocietyLists());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OccurSociety()
    {
        for (int i = 0; i < societiesPerDay; i++)
        {
            Society society = societies[Random.Range(0, societies.Count)];
            Trait[] traits = society.GetTraitInfluenced();
            Trait trait=traits[Random.Range(0, traits.Length)];
            EconomyManager.instance.ValueMore(trait);
        }
    }

    public void PopupSociety()
    {

    }
}
