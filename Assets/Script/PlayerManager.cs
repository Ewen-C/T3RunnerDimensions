using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    [SerializeField] private GameObject gameOverPanel;
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
            gameOverPanel.SetActive(true);
        }
    }
}
