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
    public List<string> GetNewsDescription()
    {
        List<string> texts = new List<string>();
        foreach(Trait trait in traitInfluenced)
        {
            string text = "";
            text += "- Trait \"" + trait.GetTraitName().ToUpper() + "\" Increases its Value in the Anoma";
            texts.Add(text);
        }
        
        return texts;
    }
}
