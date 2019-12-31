using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public int deaths;
    private LevelChanger lvl;
    private void Start()
    {
        deaths = PlayerPrefs.GetInt("Deaths", deaths);
        lvl = GameObject.Find("Levelchanger").GetComponent<LevelChanger>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // count player deaths and save to pc in tmp folder
        deaths ++;
        PlayerPrefs.SetInt("Deaths", deaths);
        PlayerPrefs.Save();
        //Debug.Log(deaths);
        //if death counter goes to 7, reset deaths counter
        if (deaths == 7)
        {
            PlayerPrefs.SetInt("Deaths", -1);
        }
        //set scene to current deahts counter
        lvl.FadeToLevel(deaths);
    }
}
