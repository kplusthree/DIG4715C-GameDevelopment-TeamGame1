using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public GameObject hazard2;
    public GameObject hazard3;
    public GameObject hazard4;
    public GameObject hazard5;
    [HideInInspector]
    public int score;
    [HideInInspector]
    public int astCounter;
    [HideInInspector]
    public int lives;
    [HideInInspector]
    public bool gameOver;
    [HideInInspector]
    public bool restart;
    [HideInInspector]
    public bool win;
    [HideInInspector]
    public bool firstRound;
    [HideInInspector]
    public bool secondRound;
    [HideInInspector]
    public bool waited;
    [HideInInspector]
    public bool lost;
    [HideInInspector]
    public Quaternion spawnRotation;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    public Vector3 spawnValues;
    public int hazardCount;

    public AudioClip gameMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        astCounter = 0;
        lives = 3;
        spawnRotation = Quaternion.identity;
        scoreText.text = "Score: " + score;
        restartText.text = "";
        gameOverText.text = "";

        gameOver = false;
        restart = false;
        firstRound = true;
        waited = false;
        lost = false;

        musicSource.clip = gameMusic;
        musicSource.Play();
        SpawnAsteroids();
        StartCoroutine(Waiting(30));
    }

    // Update is called once per frame
    void Update()
    {
        // checks if the player is trying to quit the game
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (restart == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (firstRound == true && waited == true)
        {
            waited = false;
            SpawnUFO();
        }

        if (secondRound == true && waited == true)
        {
            waited = false;
            SpawnTinyUFO();
        }

        if (lives == 0 && lost == false)
        {
            gameOver = true;
            GameOver();
        }

        if ((astCounter >= 28) && (gameOver == false) && secondRound == false)
        {
            secondRound = true;
            firstRound = false;
            hazardCount = 2;
            SpawnAsteroids();
            StartCoroutine(Waiting(40));
        }

        if ((astCounter >= 70) && (gameOver == false))
        {
            win = true;
            Win();
        }

        if (gameOver || win)
        {
            restartText.text = "Press 'R' for Restart";
            restart = true;
        }
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < hazardCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Instantiate(hazard, spawnPosition, spawnRotation);
        }
        for (int i = 0; i < hazardCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), -spawnValues.y, spawnValues.z);
            Instantiate(hazard, spawnPosition, spawnRotation);
        }
        for (int i = 0; i < 1; i++)
        {
            Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
            Instantiate(hazard, spawnPosition, spawnRotation);
        }
        for (int i = 0; i < 1; i++)
        {
            Vector3 spawnPosition = new Vector3(-spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
            Instantiate(hazard, spawnPosition, spawnRotation);
        }
    }

    void SpawnUFO()
    {
        Vector3 spawnPosition = new Vector3(-spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
        Instantiate(hazard4, spawnPosition, spawnRotation);
    }

    void SpawnTinyUFO()
    {
        Vector3 spawnPosition = new Vector3(-spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
        Instantiate(hazard5, spawnPosition, spawnRotation);
    }

    public void BreakUp(Vector3 location, int whichBreak)
    {
        if (whichBreak == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(hazard2, location, spawnRotation);
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(hazard3, location, spawnRotation);
            }
        }
    }

    IEnumerator Waiting(int time)
    {
        yield return new WaitForSeconds(time);
        waited = true;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScore();
    }

    public void Win()
    {
        gameOverText.text = "You Win! 'Esc' To Quit";
        musicSource.Stop();
        musicSource.clip = winMusic;
        musicSource.Play();
        gameOver = true;
    }

    public void GameOver()
    {
        lost = true;
        gameOverText.text = "Game Over! 'Esc' To Quit";
        musicSource.Stop();
        musicSource.clip = loseMusic;
        musicSource.Play();
    }
}
