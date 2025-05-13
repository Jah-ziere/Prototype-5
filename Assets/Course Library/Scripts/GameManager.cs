using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;

    public GameObject pauseScreen;
    private bool isPaused = false;


    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText; 
    public bool isGameActive; 

    public int lives = 3;
    public TextMeshProUGUI livesText;


    public Button restartButton;

    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());

        score = 0;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        spawnRate/= difficulty;

        lives = 3;
        UpdateLives(0); // display starting lives

    }


    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            //waits a few seconds before spawning a new object into the scene
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        //adds more to the score number 
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
       gameOverText.gameObject.SetActive(true);
       isGameActive = false;

       restartButton.gameObject.SetActive(true);
       Time.timeScale = 1;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void UpdateLives(int livesToChange)
    {
    lives += livesToChange;
    livesText.text = "Lives: " + lives;

    if (lives <= 0)
    {
        GameOver();
    }
    }



    // Update is called once per frame
    void Update()
    {
        if (isGameActive && Input.GetKeyDown(KeyCode.Escape))
    {
        TogglePause();
    }

    }

    public void TogglePause()
    {
    isPaused = !isPaused;
    pauseScreen.SetActive(isPaused);

    if (isPaused)
    {
        Time.timeScale = 0;
    }
    else
    {
        Time.timeScale = 1;
    }
}


}
