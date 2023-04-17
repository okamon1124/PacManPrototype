using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus= gameObject.GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        var collide_list = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (var collide in collide_list)
        {
            if (collide.tag == "pac_dot")
            {
                Destroy(collide.gameObject);
                GameManager.instance.GainScore();
                DontDestroyData.instance.DestroyedPacDotsName.Add(collide.gameObject.name);
            }
            else if (collide.tag == "power_pellet")
            {
                Destroy(collide.gameObject);
                playerStatus.PlayerPowerUP();
            }
            else if (collide.tag == "Enemy")
            {
                if(playerStatus.PlayerEmpowered == false)
                {
                    GameManager.instance.HitByEnemy();
                }
                else
                {
                    Destroy(collide.gameObject);
                }
            }
        }
    }

}
