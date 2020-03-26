using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NightUIManager : MonoBehaviour
{
    public TextMeshProUGUI sympathy;
    public TextMeshProUGUI hostility;
    public TextMeshProUGUI result;
    public Button nextDayButton;

    // Start is called before the first frame update
    void Start()
    {
        DisplayScore();
        StartCoroutine(DisplayResult());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayScore()
    {
        sympathy.text = Engine.instance.sympathy.ToString();
        hostility.text = Engine.instance.hostility.ToString();
    }

    IEnumerator DisplayResult()
    {
        string text = "";
        if(Engine.instance.sympathy>= Engine.instance.hostility)
        {
            text = "YOU SURVIVED!";
            yield return new WaitForSeconds(2);
            result.text = text;
            ShowButton();
        }
        else
        {
            text = "YOU DIED...\nGAME OVER";
            yield return new WaitForSeconds(2);
            result.text = text;
        }
    }

    void ShowButton()
    {
        nextDayButton.onClick.AddListener(() =>
        {
            Engine.instance.LoadSiphonScene();
        });
        nextDayButton.gameObject.SetActive(true);
    }
}
