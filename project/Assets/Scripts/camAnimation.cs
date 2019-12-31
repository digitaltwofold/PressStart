using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camAnimation : MonoBehaviour
{
    //Get Camera Object and trigger
    private Animator anim;
    private Trigger1 triggerOne;
    private bool isBack = false;

    void Start()
    {
        //get animation component and trigger from Trigger1 Script
        anim = GetComponent<Animator>();
        triggerOne = GameObject.Find("firstCam").GetComponent<Trigger1>();
    }

    void Update()
    {
        //set isBack to trigger1 bool, if true go back to cam state 0, if false let animation play until finished
            isBack = triggerOne.back;
            anim.SetBool("FirstTrigger", triggerOne.trigger);
        if (isBack == true)
        {
            anim.SetTrigger("back");
            Debug.Log("FOLLOWUP");
        }
    }
}
