using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLvl : MonoBehaviour {

   public void selectDifficulty() {
    	string difficulty = (PlayerPrefs.GetString("gameDifficulty"));
    	//Debug.Log("Difficulty is prefsa: " + difficulty);
    	if (difficulty.Length < 3) {
    		SceneManager.LoadScene("EasyGame");
    	} else {
    		switch (difficulty) {
	    		case "EasyGame":
	    			NextLvl.levelNumber = 1;
					NextLvl.rightAnswer = 0;
					NextLvl.proceed = false;
	    			SceneManager.LoadScene("GameEasy");
	    			Debug.Log("Highscore: " + PlayerPrefs.GetInt("highscoreEasy"));
	    			break;
	    		case "HardGame":
	    			SceneManager.LoadScene("GameHard");
	    			break;
	    		case "CustomGame":
	    			SceneManager.LoadScene("GameCustom");
	    			break;
	    	}
    	}
    }
}

