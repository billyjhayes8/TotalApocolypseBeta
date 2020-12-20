using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class destroys the fire object after a period of time
public class DestroyObject : MonoBehaviour
{
    public GameObject powerUp;

    // Start is called before the first frame update
    void Start()
    { 

        if (gameObject.CompareTag("Fire"))
        {
            Destroy(gameObject, 5); // destroy particle after X seconds
        }
    }

    //Destroys the power up object when the player hits it
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Power Up") && collision.gameObject.CompareTag("Player"))
        {
            Destroy(powerUp);
        }
    }
}
