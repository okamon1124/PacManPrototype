using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Instantiate(PlayerGameObject, new Vector3(0f, 0.5f, 3f), Quaternion.identity);

    [SerializeField] GameObject GameOverUIpanel;
    [SerializeField] GameObject PlayerGameObject;
    [SerializeField] GameObject[] EnemyGameObjects;

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
