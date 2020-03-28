using System;
using System.Collections.Generic;
using UnityEngine;

    public enum Effector
    {
        value,
        volatility,
        sympathy
    }
[Serializable]
[CreateAssetMenu(fileName = "Society", menuName = "Society")]
public class Society : ScriptableObject
{

    [SerializeField]
    private int societyID;

    [SerializeField]
    private string societyName;

    [SerializeField]
    private Trait[] traitInfluenced;//このSocietyが影響を与えるTrait

    [SerializeField]
    private Effector effector;


    [SerializeField]
    private string newsTitle;
    [SerializeField]
    private string information;

    //[SerializeField]
    //private Influence influence;

    public int GetsocietyID()
    {
        return societyID;
    }

    public string GetSocietyName()
    {
        return societyName;
    }
    public Effector GetEffector()
    {
        return effector;
    }
    public string GetInformation()
    {
        return information;
    }

    public string GetNewsTitle()
    {
        return newsTitle;
    }

    public Trait[] GetTraitInfluenced()
    {
        return traitInfluenced;
    }

    //public void Influence()
    //{
    //    influence.Apply();
    //}
    public string GetNewsDescription(Trait trait)
    {
        string text = "";

        if (societyName == "Constagram")
        {
            text += "- Trait \"" + trait.GetTraitName().ToUpper() + "\" Increases its PRICE in the ANOMA";
        }
        else if (societyName == "Pewtuber")
        {
            text += "- Trait \"" + trait.GetTraitName().ToUpper() + "\" Increases its VOLATILITY in the ANOMA";
        }
        else if (societyName == "Today")
        {
            text += "- Trait \"" + trait.GetTraitName().ToUpper() + "\" Scales its SYMPATHY in the ANOMA";
        }
        else
        {
            Debug.LogError("invalid societyName");
        }

        return text;
    }
}
