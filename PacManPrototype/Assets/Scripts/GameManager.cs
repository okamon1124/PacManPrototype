using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    //Instantiate(PlayerGameObject, new Vector3(0f, 0.5f, 3f), Quaternion.identity);

    [SerializeField] GameObject GameOverUIpanel;
    [SerializeField] GameObject PlayerGameObject;
    [SerializeField] GameObject[] EnemyGameObjects;

    [SerializeField] int RemainingLives = 3;
    [SerializeField] TMP_Text RemainingLivesUItext;


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


    public void GameOver()
    {
        GameOverUIpanel.SetActive(true);

        for (int i = 0;i < EnemyGameObjects.Length;i++)
        {
            Destroy(EnemyGameObjects[i]);
        }

        Destroy(PlayerGameObject);
    }


}
