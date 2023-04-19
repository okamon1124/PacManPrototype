using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int TotalLives { get; private set; } = 3;

    private GameObject PlayerGameObject;
    private GameObject[] EnemyGameObjects;
    private GameObject GameOverPanel;
    private GameObject WinTheGamePanel;
    private GameObject RedPanel;
    
    private TMP_Text RemainingLivesText;
    private TMP_Text ScoreText;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        PlayerGameObject = GameObject.FindWithTag("Player");
        EnemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        GameOverPanel = GameObject.Find("GameOverPanel");
        GameOverPanel.SetActive(false);
        WinTheGamePanel = GameObject.Find("WinTheGamePanel");
        WinTheGamePanel.SetActive(false);
        RedPanel = GameObject.Find("RedPanel");
        RedPanel.SetActive(false);
        RemainingLivesText = GameObject.Find("RemainingLivesText").GetComponent<TMP_Text>();
        ScoreText= GameObject.Find("ScoreText").GetComponent<TMP_Text>();

        DetroyPacDots();

        RemainingLivesText.text = "Remaing lives: " + DontDestroyData.instance.remaining_lives.ToString();
        ScoreText.text = "score: " + DontDestroyData.instance.static_score.ToString();
    }

    public void HitByEnemy()
    {
        DontDestroyData.instance.remaining_lives--;
        RemainingLivesText.text = "Remaing lives: " + DontDestroyData.instance.remaining_lives.ToString();
        StartCoroutine(ShowRedPanel());

        if (DontDestroyData.instance.remaining_lives <= 0) 
        {
            GameOver();
        }
        else
        {
            Revive();
        }
    }

    private void GameOver()
    {
        GameOverPanel.SetActive(true);

        for (int i = 0;i < EnemyGameObjects.Length;i++)
        {
            Destroy(EnemyGameObjects[i]);
        }

        Destroy(PlayerGameObject);

        StartCoroutine(BackToMenu());
    }

    public void GainScore()
    {
        DontDestroyData.instance.static_score += 1;
        ScoreText.text = "score: " + DontDestroyData.instance.static_score.ToString();
        if( DontDestroyData.instance.static_score == DontDestroyData.instance.target_score)
        {
            WinTheGame();
        }
    }

    private void WinTheGame()
    {
        for (int i = 0; i < EnemyGameObjects.Length; i++)
        {
            Destroy(EnemyGameObjects[i]);
        }

        Destroy(PlayerGameObject);
        WinTheGamePanel.SetActive(true);
        StartCoroutine(BackToMenu());
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2f);
        Destroy(GameObject.FindGameObjectWithTag("DontDestroyData"));
        SceneManager.LoadScene(0);
    }

    IEnumerator ShowRedPanel()
    {
        RedPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RedPanel.SetActive(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Revive()
    {
        SceneManager.LoadScene(1);
    }

    private void DetroyPacDots()
    {
        var PacDotsArray = GameObject.FindGameObjectsWithTag("pac_dot");
        var PacDotsList = new List<GameObject>();
        foreach (var pacdot in PacDotsArray)
        {
            PacDotsList.Add(pacdot);
        }
        foreach(var pacdot in PacDotsList)
        {
            if (DontDestroyData.instance.DestroyedPacDotsName.Contains(pacdot.name))
            {
                Destroy(pacdot);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToStartMenuButton()
    {
        Destroy(GameObject.FindGameObjectWithTag("DontDestroyData"));
        SceneManager.LoadScene(0);
    }
}
