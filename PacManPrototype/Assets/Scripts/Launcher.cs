using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Launcher : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
}
