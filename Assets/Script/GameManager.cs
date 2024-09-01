using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private Text scoreText;
    [SerializeField] private float scoreToAdd;
    [SerializeField] private float secondForScore;
    
    private float score = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(IncrementScoreEverySecond());
    }
    
    private IEnumerator IncrementScoreEverySecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondForScore/scoreToAdd);
            float pointsToAdd = 1;

            // Double le score si la jauge est en phase jaune
            if (GaugeManager.Instance.IsYellowPhase)
            {
                pointsToAdd *= 2;
            }
            
            score += pointsToAdd;
            UpdateScoreText();
        }
    }
    
    private void UpdateScoreText()
    {
        scoreText.text = $"{Mathf.RoundToInt(score)}"; // Utilisation de Mathf.RoundToInt pour éviter les décimales
    }

    public float GetScore()
    {
        return score;
    }
}