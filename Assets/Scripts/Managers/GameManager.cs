using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private string playerName;
    public static int currectSceneIndex = 0;
    private bool gameOver;

    private void Awake() {
        Debug.Log("GameManager Awake");
        if (instance == null) instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void ShowCurrentSceneBuildIndex() {
        Debug.Log("Scene build index: " + SceneManager.GetActiveScene().buildIndex);
    }

    private void Update() {
        ActionHandler();
    }

    public void ActionHandler()
    {
        // If in menu and space bar pressedthen pick random class for playing
        if (currectSceneIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var randomValue = Random.Range(0,2);
                if (randomValue == 1)
                {
                    playerName = "Warrior";
                }
                else
                {
                    playerName = "Mage";
                }
                LoadNextPlayableScene();
            }
        }
        
        // if in gameover scene player can reload level or
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RelaodCurrentPlayableScene();
                gameOver = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("EntryMenu");
                Destroy(this.gameObject);
                gameOver = false;
            }
        }

        // if not in menu then player can stop the game by pressing escape
        if (currectSceneIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
            }
        }
    }

    public void LoadNextPlayableScene()
    {
        SceneManager.LoadScene($"Level{++currectSceneIndex}{playerName}");
    }

    private IEnumerator LoadNextPlayableSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene($"Level{++currectSceneIndex}{playerName}");
    }

    private void RelaodCurrentPlayableScene()
    {
        SceneManager.LoadScene($"Level{currectSceneIndex}{playerName}");
    }

    public void Victory()
    {
        // show victory
        PlayerController.staticController.transform.Find($"{playerName}OverlayUI/WinText").gameObject.SetActive(true);
        StartCoroutine(LoadNextPlayableSceneWithDelay(3f));
    }

    public void Defeat()
    {
        gameOver = true;
        SceneManager.LoadScene("GameOver");
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }
}
