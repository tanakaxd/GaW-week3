using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SocietyManager : MonoBehaviour
{
    public SocietyDataBase societyDataBase;
    public GameObject newsUI;
    public Button closeButton;
    public TextMeshProUGUI newsTitle;
    public Transform newsInfluenceContainer;
    public List<TextMeshProUGUI> newsInfluence;

    private List<Society> societies;
    private int societiesPerDay=1;

    private void Awake()
    {
        societies = new List<Society>(societyDataBase.GetSocietyLists());
        
    }
    // Start is called before the first frame update
    void Start()
    {
        RegisterClose();
    }

    private void RegisterClose()
    {
        closeButton.onClick.AddListener(() =>
        {
            newsUI.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OccurSociety()
    {
        Society society = null;
        Trait[] traits=null;

        for (int i = 0; i < societiesPerDay; i++)
        {
            society = societies[Random.Range(0, societies.Count)];
            traits = society.GetTraitInfluenced();
            Trait trait=traits[Random.Range(0, traits.Length)];
            EconomyManager.instance.ValueMore(trait);
        }

        PublishGloomyberg(society, traits);
    }



    public void PopupSociety()
    {
        newsUI.gameObject.SetActive(true);
    }

    private void PublishGloomyberg(Society society, Trait[] traits)
    {

        newsTitle.text = society.GetNewsTitle();

        for (int i = 0; i < society.GetNewsDescription().Count; i++)
        {
            newsInfluence[i].text = society.GetNewsDescription()[i];
        }
    }
}
