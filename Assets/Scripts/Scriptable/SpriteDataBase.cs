using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SpriteDataBase", menuName = "SpriteDataBase")]
public class SpriteDataBase : ScriptableObject
{

    [SerializeField]
    private List<Sprite> spriteLists = new List<Sprite>();


    public List<Sprite> GetSpriteLists()
    {
        return spriteLists;
    }

    public Sprite GetRandomSprite()
    {
        return spriteLists[Random.Range(0, spriteLists.Count)];
    }
}
