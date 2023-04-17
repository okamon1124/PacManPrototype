using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    [SerializeField] int RemainingLives = 3;

    GameObject PlayerGameObject;
    GameObject[] EnemyGameObjects;
    GameObject GameOverPanel;
    GameObject WinTheGamePanel;
    GameObject RedPanel;
    
    TMP_Text RemainingLivesText;
    TMP_Text ScoreText;

    private int score = 0;
    private int TargetScore;

    public static GameManager instance;

    private void Awake()
    {
        instance= this;
    }

    private void OnDestroy()
    {
        if (instance != null)
        {
            Destroy(instance);
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
        TargetScore = GameObject.FindGameObjectsWithTag("pac_dot").Length;

        RemainingLivesText.text = "Remaing lives: " + RemainingLives.ToString();
        ScoreText.text = "score: " + score.ToString();
    }

    public void HitByEnemy()
    {
        RemainingLives--;
        RemainingLivesText.text = "Remaing lives: " + RemainingLives.ToString();
        StartCoroutine(ShowRedPanel());
        if (RemainingLives <= 0) 
        {
            GameOver();
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
        score += 1;
        ScoreText.text = "score: " + score.ToString();
        if( score == TargetScore)
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
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator ShowRedPanel()
    {
        RedPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RedPanel.SetActive(false);
    }
}
