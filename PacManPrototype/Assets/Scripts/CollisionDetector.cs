using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStatus;

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && playerStatus.PlayerEmpowered == false)
        {
            GameManager.instance.HitByEnemy();
        }
        else if (collision.gameObject.tag == "Enemy" && playerStatus.PlayerEmpowered == true)
        {
            Destroy(collision.gameObject);
            //GameManager.instance.KillEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pac_dot")
        {
            GameManager.instance.GainScore();
        }
        else if((other.gameObject.tag == "power_pellet"))
        {
            playerStatus.PlayerPowerUP();
        }
    }
}
