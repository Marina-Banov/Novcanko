using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameObject panel;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        panel.gameObject.SetActive(false);
    }
}