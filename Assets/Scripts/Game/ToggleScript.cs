using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour {

	private Toggle m_Toggle;

    void Start() {
    	//Debug.Log("Početni Difficulty is prefsa: " + PlayerPrefs.GetString("gameDifficulty"));
        m_Toggle = GetComponent<Toggle>();
        if (m_Toggle.name == PlayerPrefs.GetString("gameDifficulty")) {
        	m_Toggle.isOn = true;
        }
        m_Toggle.onValueChanged.AddListener(delegate {
        	ToggleValueChanged(m_Toggle);
        	});
        //Debug.Log("Toggle is: " + m_Toggle.isOn + " ime: " + " " + m_Toggle.name);
    }

    void ToggleValueChanged(Toggle change) {
    	//Debug.Log("Toggle is: " + m_Toggle.isOn + " ime: " + " " + m_Toggle.name);
    	if (m_Toggle.isOn) {
        	PlayerPrefs.SetString("gameDifficulty", m_Toggle.name);
        }
    	//PlayerPrefs.SetString("gameDifficulty", m_Toggle.name);
    	//TREBA SLOŽITI TOGGLE DA BUDE NA PRAVOM MJESTU
    	//Debug.Log("Promjenjeni Difficulty is prefsa: " + PlayerPrefs.GetString("gameDifficulty"));
    }
}

