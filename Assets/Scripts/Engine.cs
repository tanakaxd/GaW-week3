using UnityEngine;
using UnityEngine.SceneManagement;

public class Engine : MonoBehaviour
{
    public static Engine instance;
    public int day = 1;
    public float matter = 1000;
    public int lifeEnergy = 0;
    public int sympathy = 0;
    public int hostility = 10;
    public int playerLevel = 1;

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
        societyManager = GetComponent<SocietyManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //Engineは常に存続し続けるので、start()で何かを呼び出すということができない。
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
        societyManager.OccurSociety();
        societyManager.PopupSociety();
        cardManager.UpdateCardsPrice(economyManager.CalculateCurrentPrice());
        cardManager.ActivateCards();
    }

    //取引シーンがアンロードされたら
    private void DealSceneUnloaded(Scene dealScene)
    {
        SceneManager.sceneLoaded -= DealSceneLoaded;
        SceneManager.sceneUnloaded -= DealSceneUnloaded;
        cardManager.DeactivateCards();
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