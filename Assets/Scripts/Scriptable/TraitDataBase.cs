using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TraitDataBase", menuName = "TraitDataBase")]
public class TraitDataBase : ScriptableObject
{

    [SerializeField]
    private List<Trait> traitLists = new List<Trait>();


    //　アイテムリストを返す
    public List<Trait> GetTraitLists()
    {
        return traitLists;
    }
}
