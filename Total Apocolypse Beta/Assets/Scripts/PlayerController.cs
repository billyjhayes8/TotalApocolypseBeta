using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Variables
    public float speed;
    public float jummpForce = 2.5f;
    public bool isOnGround = true;
    public float health = 100.0f;
    public TextMeshProUGUI healthBoostText;
    public AudioSource powerUpSound;

    //public Animator anim;

    private float propellForce = 2.0f;
    private Rigidbody playerRb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        speed = 50.0f;

        playerRb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        powerUpSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        //If the game is active, then the player can move. (used mainly before game starts)
        if(gameManager.isGameActive == true)
        {
            PlayerMovement();
        }

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

    //Displays the health boost text for 3 seconds then sets the objects activity to false
    IEnumerator powerUpTextCountdown()
    {
        yield return new WaitForSeconds(3);
        healthBoostText.gameObject.SetActive(false);
    }

    //Collision detection
    private void OnCollisionEnter(Collision collision)
    {
        //If the player is on the ground, sets the speed
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            speed = 50.0f;
        }

        //If the player hits a normal zombie, it minuses 15 from health
        if (collision.gameObject.CompareTag("Zombie Vertical") || collision.gameObject.CompareTag("Zombie Horizontal"))
        {
            health -= 15;
        }

        //If player hits crawler zombie, it minuseds 20
        if (collision.gameObject.CompareTag("Crawler"))
        {
            health -= 20;
        }

        //If player hits the fire, minuses 30 from health
        if (collision.gameObject.CompareTag("Fire"))
        {
            health -= 30;
        }

        //If the player ends up in a crawler dark entrance, destroys the player + ends game
        if (health <= 0)
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }

        //If the player hits the power up, adds 10 health + plays the power up sound
        if (collision.gameObject.CompareTag("Power Up"))
        {
            health += 10.0f;
            healthBoostText.gameObject.SetActive(true);
            powerUpSound.Play();
            StartCoroutine(powerUpTextCountdown());
        }

        // Once the player touches the win zone (Evac Zone) area, the game will end and declare the player the winner
        if (collision.gameObject.CompareTag("Win Zone"))
        {
            gameManager.WinGame();
        }

        // This will propell the player back once they hit an enemy zombie
        if (collision.gameObject.CompareTag("Zombie Vertical") || collision.gameObject.CompareTag("Zombie Horizontal")|| collision.gameObject.CompareTag("Crawler"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = transform.position - collision.gameObject.transform.position;

            playerRb.AddForce(awayFromPlayer * propellForce, ForceMode.Impulse);
        }
    }
}
