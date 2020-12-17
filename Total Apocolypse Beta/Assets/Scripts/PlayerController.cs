using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    public float speed = 50.0f;
    public float jummpForce = 2.5f;
    public bool isOnGround = true;
    public float health = 100.0f;

    //public Animator anim;

    private float propellForce = 2.0f;
    private Rigidbody playerRb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the game is active, then the player can move. (used mainly before game starts)
        if(gameManager.isGameActive == true)
        {
            PlayerMovement();
        }

        /*
        if (Input.GetKey("w") && Input.GetKey("a") && Input.GetKey("s") && Input.GetKey("d"))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        */

    }

    //Player movement method - allows for full movement for the player as well as a jumping ability
    void PlayerMovement()
    {
        //local variables used for movement direction
        float movementHorizontal = Input.GetAxis("Vertical");
        float movementVertical = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(movementHorizontal, 0.0f, movementVertical);

        playerRb.AddForce(movement * speed * Time.deltaTime);

        //code for player jumping ability, only when on the ground & player cannot move while in the air
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jummpForce, ForceMode.Impulse);
            isOnGround = false;
            speed = 0.0f;
        }
    }

    //Collision detection - whether or not player is on ground + hits enemy so health can be updated
    // +  when health is 0 then destroy player and end game
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            speed = 50.0f;
        }

        if (collision.gameObject.CompareTag("Zombie Vertical") || collision.gameObject.CompareTag("Zombie Horizontal"))
        {
            health -= 10;
        }

        if (collision.gameObject.CompareTag("Crawler"))
        {
            health -= 15;
        }

        if (collision.gameObject.CompareTag("Fire"))
        {
            health -= 20;
        }

        if(health <= 0 || collision.gameObject.CompareTag("Dark Entrance"))
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }

        if(collision.gameObject.CompareTag("Win Zone"))
        {
            gameManager.WinGame();
        }

        if (collision.gameObject.CompareTag("Zombie Vertical") || collision.gameObject.CompareTag("Zombie Horizontal")|| collision.gameObject.CompareTag("Crawler"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = transform.position - collision.gameObject.transform.position;

            playerRb.AddForce(awayFromPlayer * propellForce, ForceMode.Impulse);
        }
    }
}
