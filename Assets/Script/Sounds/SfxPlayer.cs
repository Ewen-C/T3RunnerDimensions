using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.deathSound, transform.position);
    }
}
