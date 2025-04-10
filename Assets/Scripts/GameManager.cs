using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text scoreText;
    public Text timeText;
    public GameObject gameOverPanel;
    public Text finalScoreText;
    public Text finalTimeText;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver(int score, float time)
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Score: " + score;
        finalTimeText.text = "Time: " + time.ToString("F1") + "s";
    }

    void Update()
    {
        if (!gameOverPanel.activeSelf)
        {
            scoreText.text = "Score: " + FindObjectOfType<SnakeController>().GetComponentsInChildren<Transform>().Length;
            timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString("F1") + "s";
        }

        if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
