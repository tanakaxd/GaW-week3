using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Society", menuName = "Society")]
public class Society : ScriptableObject
{

    [SerializeField]
    private int societyID;

    [SerializeField]
    private string societyName;

    [SerializeField]
    private Trait[] traitInfluenced;//このSocietyが価値を上昇させるTrait

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
    public string GetInformation()
    {
        return information;
    }

    public Trait[] GetTraitInfluenced()
    {
        return traitInfluenced;
    }

    //public void Influence()
    //{
    //    influence.Apply();
    //}
}
