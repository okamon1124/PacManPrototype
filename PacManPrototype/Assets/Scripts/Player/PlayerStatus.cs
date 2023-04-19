using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    
    public bool PlayerEmpowered { get; private set; } = false;

    [SerializeField] private Material PlayerMaterial;
    [SerializeField] private Material PowerPelletMaterial;
    [SerializeField] private float EmpoweredDuration = 10f;
    private GameObject PowerUpModeText;

    GameObject[] Enemies;

    private void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        PowerUpModeText = GameObject.Find("PowerUpModeText");
        PowerUpModeText.SetActive(false);
    }

    public void PlayerPowerUP()
    {
        PlayerEmpowered = true;
        CallRunAwayMethod(true);
        var meshes = transform.GetChild(0);
        for (int i = 0; i < meshes.childCount; i++)
        {
            meshes.GetChild(i).GetComponent<Renderer>().material = PowerPelletMaterial;
        }

        PowerUpModeText.SetActive(true);
        StartCoroutine(PlayerPowerUPcoroutine());
    }

    IEnumerator PlayerPowerUPcoroutine()
    {
        yield return new WaitForSeconds(EmpoweredDuration);
        var meshes = transform.GetChild(0);
        for (int i = 0; i < meshes.childCount; i++)
        {
            meshes.GetChild(i).GetComponent<Renderer>().material = PlayerMaterial;
        }

        PlayerEmpowered = false;
        CallRunAwayMethod(false);
        PowerUpModeText.SetActive(false);
    }

    void CallRunAwayMethod(bool playerEmpowered)
    {
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<EnemyController>().RunAwayFromPlayer(playerEmpowered);
        }
    }
}
