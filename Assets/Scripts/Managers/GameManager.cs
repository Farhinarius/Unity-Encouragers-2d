using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static int currectSceneIndex;
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
        // If in menu
        if (currectSceneIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextScene();
            }
        }
        
        // if in gameover scene
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RelaodCurrentScene();
                gameOver = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("EntryMenu");
                Destroy(this.gameObject);
                gameOver = false;
            }
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene($"Level{++currectSceneIndex}");
    }

    private IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene($"Level{++currectSceneIndex}");
    }

    private void RelaodCurrentScene()
    {
        SceneManager.LoadScene($"Level{currectSceneIndex}");
    }

    public void Victory()
    {
        // show victory
        PlayerController.staticController.transform.Find("MageOverlayUI/WinText").gameObject.SetActive(true);
        StartCoroutine(LoadNextSceneWithDelay(3f));
    }

    public void Defeat()
    {
        gameOver = true;
        SceneManager.LoadScene("GameOver");
    }
}
