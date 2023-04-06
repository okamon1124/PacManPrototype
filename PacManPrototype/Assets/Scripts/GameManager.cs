using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject PlayerGameObject;
    [SerializeField] GameObject[] EnemyGameObjects;

    [SerializeField] int RemainingLives = 3;
    [SerializeField] TMP_Text RemainingLivesText;

    [SerializeField] int score = 0;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] int TargetScore = 3;
    private int TargetNumberOfEnemyToKill;
    private int CurrentNumberOfEnemyToKill = 0;

    [SerializeField] GameObject WinTheGameUIpanel;
    [SerializeField] GameObject RedPanel;

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
        RemainingLivesText.text = "Remaing lives: " + RemainingLives.ToString();
        ScoreText.text = "score: " + score.ToString();
        TargetNumberOfEnemyToKill = EnemyGameObjects.Length;
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
        WinTheGameUIpanel.SetActive(true);
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

    public void KillEnemy()
    {
        CurrentNumberOfEnemyToKill += 1;
        if(CurrentNumberOfEnemyToKill == TargetNumberOfEnemyToKill) 
        {
            WinTheGame();
        }
    }

    IEnumerator ShowRedPanel()
    {
        RedPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RedPanel.SetActive(false);
    }
}
