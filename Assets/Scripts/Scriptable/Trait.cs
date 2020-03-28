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
    Legendary,
}

public enum CategoryOfTrait
{
    Kimoi,
    Grotesque,
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
    private string traitNameJP;

    [SerializeField]
    private TypeOfTrait type;

    [SerializeField]
    private RarityOfTrait rarity;

    [SerializeField]
    private CategoryOfTrait category;

    [SerializeField]
    private float traitEnergy;//平均: 10

    [SerializeField]
    private float traitBaseValue;// 平均初期値: 100

    [SerializeField]
    private float traitBaseSympathy;// -100~100

    [SerializeField]
    private float traitValueStretch;//いらない説

    [SerializeField]
    private float traitValueVolatility; // 平均変化量: 3

    [SerializeField]
    private float traitOscillationFrequency;　//平均方向持続数: 3

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

    public string GetTraitNameJP()
    {
        return traitNameJP;
    }

    public TypeOfTrait GetTypeOfTrait()
    {
        return type;
    }

    public RarityOfTrait GetRarityOfTrait()
    {
        return rarity;
    }
    public float GetRarityForFloatValue()
    {
        switch (rarity)
        {
            case RarityOfTrait.Common:
                return 1.0f;
            case RarityOfTrait.Uncommon:
                return 1.05f;
            case RarityOfTrait.Rare:
                return 1.1f;
            case RarityOfTrait.Epic:
                return 1.15f;
            case RarityOfTrait.Legendary:
                return 1.2f;
            default:
                Debug.Log("invalid RarityOfTrait");
                return 1.0f;
        }
    }

    public float GetRarityForAppearanceProbability()
    {
        switch (rarity)
        {
            case RarityOfTrait.Common:
                return 0.50f;
            case RarityOfTrait.Uncommon:
                return 0.25f;
            case RarityOfTrait.Rare:
                return 0.15f;
            case RarityOfTrait.Epic:
                return 0.08f;
            case RarityOfTrait.Legendary:
                return 0.02f;
            default:
                Debug.Log("invalid RarityOfTrait");
                return 1.0f;
        }
    }

    public CategoryOfTrait GetCategoryOfTrait()
    {
        return category;
    }

    public float GetTraitEnergy()
    {
        return traitEnergy;
    }

    public float GetTraitBaseValue()
    {
        return traitBaseValue;
    }

    public float GetTraitBaseSympathy()
    {
        return traitBaseSympathy;
    }

    public float GetTraitValueVolatility()
    {
        return traitValueVolatility;
    }

    public float GetTraitValueStretch()
    {
        return traitValueStretch;
    }

    public float GetTraitOscillationFrequency()
    {
        return traitOscillationFrequency;
    }

    public string GetInformation()
    {
        return information;
    }
}