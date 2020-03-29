using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CozmoController : MonoBehaviour
{
    //private bool standStill = false;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StandStill()
    {
        animator.SetBool("Stand Still", true);
    }
}
