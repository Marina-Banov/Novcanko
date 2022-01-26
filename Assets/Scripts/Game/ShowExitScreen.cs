using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowExitScreen : MonoBehaviour
{
    public Canvas canvas;
    public GameObject panel;
    public GameObject blur;
    public TextMeshProUGUI lastLevel, showLevel, highscore;

    public void showPanel()
    {
        if (panel.activeSelf == true)
        {
            panel.gameObject.SetActive(false);
            blur.gameObject.SetActive(false);
        } else
        {
            if (System.Convert.ToInt32(PlayerPrefs.GetString("highscore")) < System.Convert.ToInt32(lastLevel.text))
            {
                PlayerPrefs.SetString("highscore", (System.Convert.ToInt32(lastLevel.text) - 1).ToString());
                Debug.Log(PlayerPrefs.GetString("highscore"));
            }
            showLevel.text = (System.Convert.ToInt32(lastLevel.text) - 1).ToString();
            highscore.text = "TVOJ OSOBNI REKORD: " + PlayerPrefs.GetString("highscore");
            panel.gameObject.SetActive(true);
            blur.gameObject.SetActive(true);
        }
    }
}
