using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUIManager : MonoBehaviour
{
    public GameObject endPanel;
    public Button endPopupButton;
    public Button yesButton;
    public Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        RegisterEndPanelPopup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RegisterEndPanelPopup()
    {
        endPopupButton.onClick.AddListener(() =>
        {
            endPanel.SetActive(true);
            yesButton.onClick.AddListener(() =>
            {
                Engine.instance.LoadNightScene();
            });
            closeButton.onClick.AddListener(() =>
            {
                closeButton.onClick.RemoveAllListeners();
                yesButton.onClick.RemoveAllListeners();
                endPanel.SetActive(false);
            });
        });
    }
}
