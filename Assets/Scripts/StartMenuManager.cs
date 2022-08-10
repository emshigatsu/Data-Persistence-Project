using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//[DefaultExecutionOrder(1000)]
public class StartMenuManager : MonoBehaviour
{
    
    public Button firstButton;
    public Button secondButton;
    public int score;
    public TextMeshProUGUI bestScoreText;
    public string bestPlayerName;
    void Start()
    {
       bestPlayerName = MainManager.Instance.bestPlayer;
       score = MainManager.Instance.highScore;
       // playerName = MainManager.Instance.playerName2;
       bestScoreText.text = "Best Score :" + bestPlayerName + ": " + score;
        //Debug.Log(score);
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag("brick");
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

   public void Exit()
    { 
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
  }
}
