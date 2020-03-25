using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SocietyDataBase", menuName = "SocietyDataBase")]
public class SocietyDataBase : ScriptableObject
{

    [SerializeField]
    private List<Society> societyLists = new List<Society>();


    //　アイテムリストを返す
    public List<Society> GetSocietyLists()
    {
        return societyLists;
    }
}
