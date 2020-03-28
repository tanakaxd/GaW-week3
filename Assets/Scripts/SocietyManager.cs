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
    private int modifiedTraitsPerSociety=6;
    private float societyOccurenceRate=0.2f;

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
        if (Random.Range(0f, 1f) > societyOccurenceRate)
        {
            return;
        }

        Society society = societies[Random.Range(0, societies.Count)];
        Trait[] traits = society.GetTraitInfluenced();

        List<Trait> chosenTraits = new List<Trait>();
        for (int i = 0; i < modifiedTraitsPerSociety; i++)
        {
            chosenTraits.Add(traits[Random.Range(0, traits.Length)]);
        }


        EconomyManager.instance.TweakModifier(society, chosenTraits);
        

        PublishGloomyberg(society, chosenTraits);
        PopupSociety();
    }



    public void PopupSociety()
    {
        newsUI.gameObject.SetActive(true);
    }

    private void PublishGloomyberg(Society society, List<Trait> chosenTraits)
    {

        newsTitle.text = society.GetNewsTitle();

        for (int i = 0; i < chosenTraits.Count; i++)
        {
            newsInfluence[i].text = society.GetNewsDescription(chosenTraits[i]);
        }

    }
}
