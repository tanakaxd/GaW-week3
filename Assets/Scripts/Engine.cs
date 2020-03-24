using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    public static Engine instance;
    [HideInInspector]
    public int matter;
    [HideInInspector]
    public int lifeEnergy;
    [HideInInspector]
    public int sympathy;
    [HideInInspector]
    public int hostility;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
