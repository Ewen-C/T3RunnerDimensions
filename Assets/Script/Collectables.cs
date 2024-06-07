using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public int value = 3;  // La valeur de base pour la jauge blanche

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GaugeManager.Instance.AddPoints(value);
            Destroy(gameObject);
        }
    }
}
