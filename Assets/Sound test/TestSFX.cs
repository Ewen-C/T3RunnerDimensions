using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSFX : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.collectSound, this.transform.position);
    }
}