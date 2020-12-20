using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Variables
    public PlayerController playerController;
    public GameObject gameOverPanel;
    public GameObject startPanel;
    public GameObject winPanel;
    public GameObject crawler;
    public GameObject fire;
    public Button restartButton;
    public TextMeshProUGUI healthText;
    public float health = 100.0f;
    public bool isGameActive = false;

    private float spawnDelay = 2.0f;
    private float spawnInterval = 2.0f;

    private string url = "https://github.com/billyjhayes8/TotalApocolypseBeta";

    private void Start()
    {
        //Starts the game before the first frame
        StartGame();

        InvokeRepeating("SpawnCrawler", spawnDelay, spawnInterval);

        InvokeRepeating("SpawnFire", 0, 2.0f);

    }

    // Update is called once per frame
    void Update()
    {

        UpdateHealth();

        if (health <= 0)
        {
            isGameActive = false;
            healthText.gameObject.SetActive(false);
        }
    }

    //This will spawn fire in random places within a certain space
    void SpawnFire()
    {
        Vector3 fireSpawn = new Vector3(Random.Range(-3.5f, 3.5f), .25f, Random.Range(10.0f, 15.0f));

        Instantiate(fire, fireSpawn, Quaternion.identity);
    }

    //This will instantiate crawler zombie objects at a certain point.
    void SpawnCrawler()
    {
        Vector3 crawler1Spawn = new Vector3(3.5f, 0.25f, 23.5f);
        Vector3 crawler2Spawn = new Vector3(3.5f, 0.25f, 27.5f);
        Vector3 crawler3Spawn = new Vector3(3.5f, 0.25f, 31.5f);

        Instantiate(crawler, crawler1Spawn, Quaternion.identity);
        Instantiate(crawler, crawler2Spawn, Quaternion.identity);
        Instantiate(crawler, crawler3Spawn, Quaternion.identity);

    }

    //This will bring up the win panel when the player enters the evac zone
    public void WinGame()
    {
        winPanel.gameObject.SetActive(true);
        isGameActive = false;
        playerController.speed = 0;
    }

    //This updates the health on the UI
    void UpdateHealth()
    {
        health = playerController.health;
        healthText.gameObject.SetActive(true);
        healthText.text = "Health: " + health;
    }

    //If the restart button is clicked, this will reload the scene and restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameActive = true;
    }

    //Once the game is over, this will bring up the game over panel
    public void GameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
        isGameActive = false;
    }

    // At the beginning of the game, this will bring up the start game panel
    public void StartGame()
    {
        startPanel.gameObject.SetActive(true);
    }

    // Once Start Game button is clicked, this will get rid of the start game panel
    public void setIsGameActiveTrue()
    {
        isGameActive = true;
        startPanel.gameObject.SetActive(false);
    }

    //This will quit the game 
    public void QuitGame()
    {
        Application.Quit();
    }

    //Opens the Github repository 
    public void UrlOpen()
    {
        Application.OpenURL(url);
    }
}
