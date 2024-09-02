using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private float finalScore;
    public static bool gameOver;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text scoreFinalText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private MusicPlayer musicPlayer;
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (gameOver)
        {
            musicPlayer.isMusicPlaying = false;
            Time.timeScale = 0;
            finalScore = GameManager.Instance.GetScore();
            scoreFinalText.text = $"Votre score: {Mathf.RoundToInt(finalScore)}";
            CheckHighScore();
            highScoreText.text = $"Votre meilleur score: {Mathf.RoundToInt(PlayerPrefs.GetFloat("HighScore", 0f))}";
            gameOverPanel.SetActive(true);
        }
    }

    void CheckHighScore()
    {
        if (finalScore > PlayerPrefs.GetFloat("HighScore", 0f))
        {
            PlayerPrefs.SetFloat("HighScore", finalScore);
        }
    }
}
