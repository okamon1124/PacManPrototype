using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    
    public bool PlayerEmpowered { get; private set; } = false;

    [SerializeField] private Material PlayerMaterial;
    [SerializeField] private Material PowerPelletMaterial;
    [SerializeField] private GameObject PowerUpModeText;

    private Renderer player_material;

    private void Start()
    {
        player_material = this.gameObject.GetComponent<Renderer>();
    }

    public void PlayerPowerUP()
    {
        PlayerEmpowered = true;
        
        player_material.material = PowerPelletMaterial;
        PowerUpModeText.SetActive(true);
        StartCoroutine(PlayerPowerUPcoroutine());
    }

    IEnumerator PlayerPowerUPcoroutine()
    {
        
        yield return new WaitForSeconds(5f);
        player_material.material = PlayerMaterial;
        PlayerEmpowered= false;
        PowerUpModeText.SetActive(false);
    }

}
