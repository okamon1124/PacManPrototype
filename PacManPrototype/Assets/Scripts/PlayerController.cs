using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float MovementSpeed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.position += new Vector3(0f, 0f, 1f * MovementSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.position += new Vector3(0f, 0f, -1f * MovementSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.position += new Vector3(1f * MovementSpeed * Time.deltaTime, 0f, 0f);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.position += new Vector3(-1f * MovementSpeed * Time.deltaTime, 0f, 0f);
        }
    }
}
