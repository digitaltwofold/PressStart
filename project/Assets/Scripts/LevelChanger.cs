﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChanger : MonoBehaviour
{
    public Animator anim;
    private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
        //if player presses start, reset level without incrementing deaths counter
        if (Input.GetButtonDown("Submit"))
        {
            FadeToLevel(1);
        }
    }
    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        anim.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
