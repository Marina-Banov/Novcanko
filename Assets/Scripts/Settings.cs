using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle hardGameToggle, helpTextToggle;

    void Start()
    {
        hardGameToggle.isOn = PlayerPrefs.GetString("gameDifficulty") != "EasyGame";
        helpTextToggle.isOn = PlayerPrefs.GetString("helpTextVisibilty") == "true";
    }

    public void UpdateGameDifficulty(bool hard)
    {
        PlayerPrefs.SetString("gameDifficulty", hard ? "HardGame" : "EasyGame");
    }

    public void UpdateHelpTextVisibilty(bool visible)
    {
        PlayerPrefs.SetString("helpTextVisibilty", visible ? "true" : "false");
    }
}
