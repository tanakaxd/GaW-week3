using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = Engine.instance.gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
