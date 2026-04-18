using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;
    public TMP_Text roundsText;

    private int roundsSurvived = 0;

    void Awake()
    {
        Instance = this;
    }

    public void AddRound()
    {
        roundsSurvived++;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        roundsText.text = "Rounds Survived: " + roundsSurvived;

        Time.timeScale = 0f; // freeze game
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
