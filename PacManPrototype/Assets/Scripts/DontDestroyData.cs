using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyData : MonoBehaviour
{
    public static DontDestroyData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
        target_score = GameObject.FindGameObjectsWithTag("pac_dot").Length;
        remaining_lives = GameManager.instance.TotalLives;
    }

    public List<string> DestroyedPacDotsName = new List<string>();
    public int static_score;
    public int target_score;
    public int remaining_lives;

}
