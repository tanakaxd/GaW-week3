using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Human : MonoBehaviour
{
    public GameObject siphonUI;
    public Button siphonButton;
    public Button closeButton;
    public TextMeshProUGUI siphonPanelDescription;
    public SpriteDataBase spriteDataBase;
    public Image image;

    private int analyzedEnergy=0;
    private int analyzedMatter=0;
    private int analyzedSympahty=0;

    [HideInInspector] public List<Trait> traits = new List<Trait>();
    private Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display()
    {
        //spriteをランダムに選んで表示させる処理
        sprite = spriteDataBase.GetRandomSprite();
        image.sprite = sprite;
    }


    //inspectorから登録する関数
    public void Popup()
    {
        siphonUI.SetActive(true);

        UpdateDescription();
        RegisterSiphon();
        RegisterClose();


    }

    void RegisterSiphon()
    {
        siphonButton.onClick.AddListener(() =>
        {
            CardManager.instance.SiphonHumanTriatsToCard(traits);
            gameObject.SetActive(false);

            siphonUI.SetActive(false);
            siphonButton.onClick.RemoveAllListeners();
            closeButton.onClick.RemoveAllListeners();
        });
        
    }


    void RegisterClose()
    {
        closeButton.onClick.AddListener(() =>
        {
            siphonUI.SetActive(false);
            siphonButton.onClick.RemoveAllListeners();
            closeButton.onClick.RemoveAllListeners();
        });

    }

    void UpdateDescription()
    {
        string text = "test"; //AnalyzeHuman();
        siphonPanelDescription.text = text;
    }

    /*
    void AnalyzeHuman()
    {
        foreach(Trait trait in traits)
        {
            //EconomyManagerからのデータが必要
            analyzedEnergy += (int)trait.GetTraitEnergy();
            analyzedMatter += (int)trait.GetTraitBaseValue();
            analyzedSympathy += (int)trait.gettrat();

        }
    }
    */
}
