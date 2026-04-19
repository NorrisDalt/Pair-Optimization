using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;
    public TMP_Text wavesText;

    private int wavesSurvived = 0;

    void Awake()
    {
        Instance = this;
    }

    public void SetRound(int wave)
    {
        wavesSurvived = wave;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        wavesText.text = "Waves Survived: " + wavesSurvived;

        Time.timeScale = 0f;
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
