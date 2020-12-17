using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will be used for enemies which move vertically 
public class Zombie : MonoBehaviour
{
    //Variables
    public float zombieSpeed,crawlerSpeed, xMin, xMax, zMin, zMax, yPos = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        zombieSpeed = Random.Range(2.0f, 4.0f);
        crawlerSpeed = Random.Range(1.0f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Zombie Vertical"))
        {
            MoveVertical();
        }

        if(gameObject.CompareTag("Zombie Horizontal"))
        {
            MoveHorizontal();
        }

        if (gameObject.CompareTag("Crawler"))
        { 
            CrawlerMovement();
        }
    }

    // Tells the crawlers to move
    void CrawlerMovement()
    {
        transform.Translate(Vector3.left * Time.deltaTime * crawlerSpeed);

            if (transform.position.x < -3.5)
            {
                Destroy(gameObject);
            }
    }

    //Zombies move vertically
    void MoveVertical()
    {
        //Move the enemy back and forth along the x axis
       transform.position = new Vector3(Mathf.PingPong(Time.time * zombieSpeed, 3), transform.position.y, transform.position.z);
    }

    //Zombies move horizontally
    void MoveHorizontal()
    {
       transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * zombieSpeed, 3));
    }

    //Zombies move diagonally
    void MoveDiagonal()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * zombieSpeed, 3), transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * zombieSpeed, 3));
    }
}
