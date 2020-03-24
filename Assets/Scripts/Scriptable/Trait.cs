using System;
using UnityEngine;
    public enum TypeOfTrait
    {
        Negative,
        Positive
    }
    public enum RarityOfTrait
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }


[Serializable]
[CreateAssetMenu(fileName = "Trait", menuName = "Trait")]
public class Trait : ScriptableObject
{

    [SerializeField]
    private int traitID;

    [SerializeField]
    private string traitName;

    [SerializeField]
    private TypeOfTrait kindOfTrait;

    [SerializeField]
    private RarityOfTrait kindOfRare;

    [SerializeField]
    private float traitEnergy;

    [SerializeField]
    private float traitBaseValue;

    [SerializeField]
    private float traitValueStretch;

    [SerializeField]
    private float traitValueVolatility;


    [SerializeField]
    private string information;
    /*
    - カード
  - ニンゲンが生まれ持った能力や機会
  - 属性
    - 生命エネルギー
      - 固定値
    - 獲得経路
      - 遺伝、能力
      - 機会、環境
    - 価格（同情ポイント）
      - 基底価格
      - 現在価格
        - 通貨換算＝同情ポイント＊レア度補正
    - 価格の変動しやすさ
      - どれだけ現在価格が動きやすいか
    - 変動の幅
      - 二つの価格の乖離しやすさ
    - 天変地異確率
    - レア度
      - コレクターに高く売れる
      */
    public int GettraitID()
    {
        return traitID;
    }

    public string GetTraitName()
    {
        return traitName;
    }

    public TypeOfTrait GetTypeOfTrait()
    {
        return kindOfTrait;
    }
    public RarityOfTrait GetRarityOfTrait()
    {
        return kindOfRare;
    }


    public float GetTraitEnergy()
    {
        return traitEnergy;
    }
    public float GetTraitBaseValue()
    {
        return traitBaseValue;
    }
    public float GetTraitValueVolatility()
    {
        return traitValueVolatility;
    }
    public float GetTraitValueStretch()
    {
        return traitValueStretch;
    }


    public string GetInformation()
    {
        return information;
    }
}