using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class UITips : MonoBehaviour
{

    Animator animator;

    Text text;
    bool ready = true;
    public void Start() {
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
        ready = false;  
    }
    public void writeTip(string tip)
    {
        text = GetComponent<Text>();
        animator = GetComponent<Animator>();
        text.text = tip;
        animator.SetTrigger("beginTip");
        Invoke("endAnim", 5f);
    }

    public void endAnim() {
        ready = true;
    }

    public bool isReady()
    {
        return ready;
    }
}