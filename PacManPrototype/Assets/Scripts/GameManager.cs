using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverUIpanel;
    [SerializeField] GameObject PlayerGameObject;
    [SerializeField] GameObject[] EnemyGameObjects;

    [SerializeField] int RemainingLives = 3;
    [SerializeField] TMP_Text RemainingLivesUItext;

    [SerializeField] int score = 0;
    [SerializeField] TMP_Text ScoreUItext;
    [SerializeField] int TargetScore = 3;

    [SerializeField] GameObject WinTheGameUIpanel;


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
        RemainingLivesUItext.text = "Remaing lives: " + RemainingLives.ToString();
        ScoreUItext.text = "score: " + score.ToString();
    }

    public void HitByEnemy()
    {
        RemainingLives--;
        RemainingLivesUItext.text = "Remaing lives: " + RemainingLives.ToString();
        if (RemainingLives <= 0) 
        {
            GameOver();
        }
    }


    private void GameOver()
    {
        GameOverUIpanel.SetActive(true);

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
        ScoreUItext.text = "score: " + score.ToString();
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
}
