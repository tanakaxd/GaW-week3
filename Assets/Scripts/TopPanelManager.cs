using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopPanelManager : MonoBehaviour
{
    public static TopPanelManager instance;
    public GameObject topPanel;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI lifeEnergyText;
    public TextMeshProUGUI matterText;
    public TextMeshProUGUI sympathyText;
    public TextMeshProUGUI hostilityText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        dayText.text = "Day: "+Engine.instance.day.ToString();
        levelText.text = "Level: "+Engine.instance.playerLevel.ToString();
        lifeEnergyText.text = "Energy: " + Engine.instance.lifeEnergy.ToString();
        matterText.text = "Matter: " + Engine.instance.matter.ToString() + "₥";
        sympathyText.text = "Sympahty: " + Engine.instance.sympathy.ToString();
        hostilityText.text = "Hostility: " + Engine.instance.hostility.ToString();
    }

    public void ActivatePanel()
    {
        topPanel.SetActive(true);
    }

    public void DeactivatePanel()
    {
        topPanel.SetActive(false);
    }
}
