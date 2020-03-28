using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Human : MonoBehaviour
{
    #region siphonUI
    public GameObject siphonUI;
    public Button siphonButton;
    public Button closeButton;
    public TextMeshProUGUI siphonPanelDescription;
    public SpriteDataBase spriteDataBase;
    //public Image image;
    #endregion

    #region transitionUI
    public GameObject transitionUI;
    public TextMeshProUGUI resultText;
    public Button okButton;
    #endregion

    public TextureManager textureManager;

    private int analyzedEnergy=0;
    private int analyzedMatter=0;
    private int analyzedSympathy=0;
    [HideInInspector]public string analyzedResult="";

    private float[] levelToFuzziness = {0.5f,0.4f,0.3f,0.2f,0.1f};
    private float sigmaOnAnalysis = 0.5f;

    [HideInInspector] public List<Trait> traits = new List<Trait>();
    //private Sprite sprite;
    private RawImage image;
    private void Awake()
    {
        image = GetComponent<RawImage>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitHuman()
    {
        analyzedEnergy = 0;
        analyzedMatter = 0;
        analyzedSympathy = 0;
        analyzedResult = "";
        traits = new List<Trait>();
    }

    public void Display()
    {
        //textureをランダムに選んで表示させる処理
        image.texture = textureManager.GetRandomTexture();
 
    }


    //inspectorから登録する関数
    public void Popup()
    {
        siphonUI.SetActive(true);

        UpdateHumanDescription(); // popup毎に登録しなおす必要がある
        RegisterSiphon();
        RegisterClose();


    }

    void RegisterSiphon()
    {
        siphonButton.onClick.AddListener(() =>
        {
            CardManager.instance.SiphonHumanTraitsToCard(traits);
            gameObject.SetActive(false);

            siphonUI.SetActive(false);
            siphonButton.onClick.RemoveAllListeners();
            closeButton.onClick.RemoveAllListeners();

            resultText.text = GetResultText();
            okButton.onClick.AddListener(() =>
            {
                Engine.instance.LoadDealScene();
            });
            transitionUI.SetActive(true);
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

    public void UpdateHumanDescription()
    {
        
        siphonPanelDescription.text = analyzedResult;
    }
    public void AnalyzeHuman()
    {
        string text="";

        float fuzziness = levelToFuzziness[Mathf.Min(Engine.instance.playerLevel-1,4)];//{0.5f,0.4f,0.3f,0.2f,0.1f}

        foreach (Trait trait in traits)
        {
            int posOrNega = trait.GetTypeOfTrait() == TypeOfTrait.Positive ? 1 : -1;

            analyzedEnergy += (int)MyRandom.RandomGaussianUnity(trait.GetTraitEnergy(), trait.GetTraitEnergy()*fuzziness*sigmaOnAnalysis);
            analyzedMatter += (int)MyRandom.RandomGaussianUnity(EconomyManager.instance.currentPrice[trait]
                , EconomyManager.instance.currentPrice[trait] * fuzziness * sigmaOnAnalysis);
            analyzedSympathy += (int)MyRandom.RandomGaussianUnity(EconomyManager.instance.currentPrice[trait]*posOrNega
                , EconomyManager.instance.currentPrice[trait]*fuzziness*sigmaOnAnalysis);
        }
        text += "ANALYSIS COMPLETED...\n" + "Analysis Accuracy Level: " + Engine.instance.playerLevel + "\n\n";
        text += "Extrapolated Life Energy: " + analyzedEnergy + "\n" + "Extrapolated Matter: " + analyzedMatter+"₥\n"
            + "Extrapolated Sympathy: " + analyzedSympathy + "\n";
        text += "Extrapolated Trait:"+"\n";
        
        List<Trait> tempTraits = new List<Trait>(traits);
        for (int i = 0; i < Engine.instance.playerLevel; i++)
        {
            int randomIndex = Random.Range(0, tempTraits.Count);
            text+= "   "+tempTraits[randomIndex].GetTraitName()+"\n";
            tempTraits.RemoveAt(randomIndex);
        }
        analyzedResult = text;
    }

    private string GetResultText()
    {
        string text = "Siphoned Cards:\n\n";
        foreach(Trait trait in traits)
        {
            text += trait.GetTraitName() + "\n";
        }
        return text;
    }
}
