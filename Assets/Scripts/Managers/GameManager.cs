using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private string playerName;
    private PlayerController playerController;
    private GameObject playerUI;
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
        
        // if in gameover scene player can reload level or return to entry menu
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
        SceneManager.LoadScene($"Level{++currectSceneIndex}");
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private IEnumerator LoadNextPlayableSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene($"Level{++currectSceneIndex}");
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void RelaodCurrentPlayableScene()
    {
        SceneManager.LoadScene($"Level{currectSceneIndex}");
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log("Mode: " + mode);
        SetActivePlayableScene();
        InstantiatePlayer();
        RefreshPlayerData();
        UpdateCoinOnSceneLoad();
        ShowLevelAtUI();
        Debug.Log("OnSceneLoad actions has called");
    }

    private void InstantiatePlayer()
    {
        Instantiate(Resources.Load(playerName),
        Vector2.zero, 
        Quaternion.identity
        );
    }

    private void RefreshPlayerData()
    {
        playerController = PlayerController.staticController;
        playerUI = playerController.transform.Find($"{playerName}OverlayUI").gameObject;
    }

    private void SetActivePlayableScene()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName($"Level{currectSceneIndex}"));
        Debug.Log("Active scene: " + SceneManager.GetActiveScene().name);
    }

    private void ShowLevelAtUI()
    {
        playerUI.transform.Find("LevelShow").GetComponent<TextMeshProUGUI>().text = $"Level {currectSceneIndex}";
    }

    private void UpdateCoinOnSceneLoad()
    {
        playerUI.transform.Find("CoinValue").GetComponent<TextMeshProUGUI>().text = 
        GetComponent<CoinManager>().Coins.ToString();
    }

    public void UpdateUICoinNumber(int pickedCoins)
    {
        playerUI.transform.Find("CoinValue").GetComponent<TextMeshProUGUI>().text = pickedCoins.ToString();
    }

    public void Victory()
    {
        // show victory
        playerController.transform.Find($"{playerName}OverlayUI/WinText").gameObject.SetActive(true);
        SceneManager.sceneLoaded -= OnSceneLoad;
        StartCoroutine(LoadNextPlayableSceneWithDelay(3f));
    }

    public void Defeat()
    {
        gameOver = true;
        SceneManager.sceneLoaded -= OnSceneLoad;
        SceneManager.LoadScene("GameOver");
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }
}
