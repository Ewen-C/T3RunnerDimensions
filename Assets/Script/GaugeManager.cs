using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GaugeManager : MonoBehaviour
{
    public static GaugeManager Instance { get; private set; }
    public float gaugeAmount = 0f;
    public bool shieldActive = false;
    public float decreaseRate = 12f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (shieldActive)
        {
            gaugeAmount -= decreaseRate * Time.deltaTime;
            if (gaugeAmount <= 0)
            {
                gaugeAmount = 0;
                shieldActive = false;
                // TODO: Update jauge to white
            }
            // TODO: Update UI for score
        }
    }

    public void AddPoints(int points)
    {
        if (!shieldActive && gaugeAmount < 99)
        {
            gaugeAmount += points;
            if (gaugeAmount >= 99)
            {
                gaugeAmount = 99;
                shieldActive = true;
                // TODO: Update jauge to yellow
            }
            // TODO: Update UI for score
        }
        else if (shieldActive)
        {
            gaugeAmount += points;
            if (gaugeAmount > 99) gaugeAmount = 99;  // Cap the GaugeAmount at 99 even in shield mode
            // TODO: Update UI for score
        }
    }
}
