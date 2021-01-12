using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    [SerializeField] LevelList levelList;
    List<Level> loadedLevels;
	string difficulty;
	int maxLevel, currentLevel;
	int rightAnswer = 0;
	float amountNumber, givenNumber = 0;
	public TextMeshProUGUI levelTxt, amountTxt, givenTxt;

	public GameObject gameComplete;

	void Start()
    {
        string jsonString = System.IO.File.ReadAllText(StartMenu.levelsSavePath);
        levelList = JsonUtility.FromJson<LevelList>(jsonString);
        difficulty = PlayerPrefs.GetString("gameDifficulty");
		loadedLevels = (difficulty == "EasyGame") ? levelList.easy : levelList.hard;
		givenTxt.enabled = (PlayerPrefs.GetString("helpTextVisibilty") == "true");
		maxLevel = loadedLevels.Count - 1;
		currentLevel = System.Math.Max(0, loadedLevels.FindIndex(NotCompleted));
		// currentLevel = 0;
		SetLevel();
	}

	bool NotCompleted(Level l)
    {
		return !l.completed;
    }

	void SetLevel()
    {
		amountNumber = loadedLevels[currentLevel].goalAmount;
		levelTxt.text = "Level: " + (currentLevel + 1).ToString() + "/" + (maxLevel + 1).ToString();
		amountTxt.text = "Teta prodavačica traži " + amountNumber.ToString() + "kn";
		UpdateGiven(-givenNumber);
	}

	public void UpdateGiven(float offset)
    {
		givenNumber += offset;
		givenTxt.text = "Za sada imaš " + givenNumber.ToString() + "kn";
	}

	public void ProceedLevel()
	{
		if (givenNumber == amountNumber)
		{
			RightAnswer();
		}
		else
		{
			WrongAnswer();
		}

		if (currentLevel == maxLevel)
		{
			EndGame();
			return;
		}

		currentLevel++;
		SetLevel();
		DeleteObjects();
	}

	void DeleteObjects()
	{
		GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("cashReg");
		foreach (GameObject gameObject in foundObjects)
		{
			Destroy(gameObject);
		}
	}

	void RightAnswer()
	{
		loadedLevels[currentLevel].completed = true;
		if (difficulty == "EasyGame")
        {
			levelList.easy[currentLevel].completed = true;
		}
		else
        {
			levelList.hard[currentLevel].completed = true;
		}
		System.IO.File.WriteAllText(StartMenu.levelsSavePath, JsonUtility.ToJson(levelList));
		// ZVUK BRAVO
		rightAnswer++;
		Debug.Log("broj tocnih" + rightAnswer);
	}

	void WrongAnswer()
	{
		/// ZVUK KRIVO! I PORUKA NA PAR SEKUNDI - KRIVO, ALI SAMO HRABRO NAPRIJED?
	}

	void EndGame()
	{

		if (PlayerPrefs.GetInt("highscoreEasy") < rightAnswer)
		{
			PlayerPrefs.SetInt("highscoreEasy", rightAnswer);
		}
		gameComplete.gameObject.SetActive(true);
		GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");

		float scorePers = (float)rightAnswer / ((float)maxLevel+1);
		Debug.Log(scorePers);
		int starNum = 0;
		if (scorePers >= 0.8) starNum = 5;
		else if (scorePers < 0.8 && scorePers >= 0.6) starNum = 4;
		else if (scorePers < 0.6 && scorePers >= 0.4) starNum = 3;
		else if (scorePers < 0.4 && scorePers >= 0.2) starNum = 2;
		else starNum = 1;
		for (int i  = 0; i < starNum; i++) {
			if (int.Parse(stars[i].name) == i+1) {
				 stars[i].transform.GetChild(0).gameObject.SetActive(true);
			}
		}
	}
}
