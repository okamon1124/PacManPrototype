using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    
    public bool PlayerEmpowered { get; private set; } = false;

    [SerializeField] private Material PlayerMaterial;
    [SerializeField] private Material PowerPelletMaterial;
    private GameObject PowerUpModeText;

    private Renderer player_material;

    GameObject[] Enemies;

    private void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        player_material = this.gameObject.GetComponent<Renderer>();
        PowerUpModeText = GameObject.Find("PowerUpModeText");
        PowerUpModeText.SetActive(false);
    }

    public void PlayerPowerUP()
    {
        PlayerEmpowered = true;
        CallRunAwayMethod(true);
        player_material.material = PowerPelletMaterial;
        PowerUpModeText.SetActive(true);
        StartCoroutine(PlayerPowerUPcoroutine());
    }

    IEnumerator PlayerPowerUPcoroutine()
    {
        yield return new WaitForSeconds(5f);
        player_material.material = PlayerMaterial;
        PlayerEmpowered= false;
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
