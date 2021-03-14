using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;

    public int lastPos;
    public int score;
    public int lives;
    public bool gameOver;

    private float spawnRate = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 3;
        gameOver = false;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            gameOver = true;
            gameOverText.gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnTarget()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);

            if (spawnRate > 2)
            {
                for (float i = spawnRate; i != 0.0f; i--)
                {

                    yield return new WaitForSeconds(0.5f);
                    Instantiate(targets[index]);
                    spawnRate = Random.Range(1, 4);
                    index = Random.Range(0, targets.Count);
                }
            } else
            {
                Instantiate(targets[index]);
                spawnRate = Random.Range(1, 4);
            }

        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToTake)
    {
        if (lives != 0)
        {
            lives -= livesToTake;
            livesText.text = "Lives: " + lives;
        }
    }
}
