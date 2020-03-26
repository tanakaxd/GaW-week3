using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Engine : MonoBehaviour
{
    public static Engine instance;
    public int day = 1;
    public int playerLevel = 1;
    public float matter = 1000;
    public float lifeEnergy = 0;
    public float sympathy = 0;
    public float hostility = 10;
    public bool debug = true;

    private EconomyManager economyManager;
    private CardManager cardManager;
    private SocietyManager societyManager;

    private void Awake()
    {
        Debug.Log("Engine Awake called");

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        economyManager = GetComponent<EconomyManager>();
        cardManager = GetComponent<CardManager>();
        //societyManager = GetComponent<SocietyManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (debug)
        {
            //StartCoroutine(temploop());
            //Test();
        }
    }

    private IEnumerator temploop()
    {
        while (true)
        {
            societyManager = GameObject.Find("SocietyManager").GetComponent<SocietyManager>();
            societyManager.OccurSociety();
            societyManager.PopupSociety();
            cardManager.UpdateCardsPrice(economyManager.CalculateCurrentPrice());
            cardManager.ActivateCards();
            yield return new WaitForSeconds(1);
        }
    }

    private void Test()
    {
        societyManager = GameObject.Find("SocietyManager").GetComponent<SocietyManager>();
        societyManager.OccurSociety();
        societyManager.PopupSociety();
        cardManager.UpdateCardsPrice(economyManager.CalculateCurrentPrice());
        cardManager.ActivateCards();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //Engineは常に存続し続けるので、シーンの始まりと同時にstart()で何かを呼び出すということができない。
    //あるシーンがロードされたらという処理を追加しておく必要がある

    //取引シーンをロードする
    public void LoadDealScene()
    {
        SceneManager.sceneLoaded += DealSceneLoaded;
        SceneManager.LoadScene("DealScene");
    }

    //取引シーンがロードされたら
    private void DealSceneLoaded(Scene dealScene, LoadSceneMode mode)
    {
        SceneManager.sceneUnloaded += DealSceneUnloaded;
        cardManager.GetAllCardScriptsInScene();
        societyManager = GameObject.Find("SocietyManager").GetComponent<SocietyManager>();
        societyManager.OccurSociety();
        societyManager.PopupSociety();
        cardManager.UpdateCardsPrice(economyManager.CalculateCurrentPrice());
        cardManager.UpdateAllAmount();
        //cardManager.ActivateCards();
    }

    //取引シーンがアンロードされたら
    private void DealSceneUnloaded(Scene dealScene)
    {
        SceneManager.sceneLoaded -= DealSceneLoaded;
        SceneManager.sceneUnloaded -= DealSceneUnloaded;
        //cardManager.DeactivateCards();
    }

    //Siphonシーンをロードする
    public void LoadSiphonScene()
    {
        SceneManager.sceneLoaded += SiphonSceneLoaded;
        SceneManager.LoadScene("SiphonScene");
    }

    //Siphonシーンがロードされたら
    private void SiphonSceneLoaded(Scene dealScene, LoadSceneMode mode)
    {
        SceneManager.sceneUnloaded += SiphonSceneUnloaded;
    }

    //Siphonシーンがアンロードされたら
    private void SiphonSceneUnloaded(Scene dealScene)
    {
        SceneManager.sceneLoaded -= SiphonSceneLoaded;
        SceneManager.sceneUnloaded -= SiphonSceneUnloaded;
    }
}