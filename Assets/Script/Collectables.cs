using UnityEngine;
using UnityEngine.VFX;

public class Collectables : MonoBehaviour
{
    [SerializeField] private VisualEffect idleEffect;
    [SerializeField] private VisualEffect collectEffect;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int pointsToAdd = GaugeManager.Instance.IsYellowPhase ? GaugeManager.Instance.pointsPerCollectableYellow : GaugeManager.Instance.pointsPerCollectableWhite;
            GaugeManager.Instance.AddPoints(pointsToAdd);
            //Debug.Log("Points add : " + pointsToAdd);
            
            idleEffect.enabled = false;
            idleEffect.Stop();
            collectEffect.enabled = true;
            collectEffect.Play();
            // Will be destroyed when the pattern goes away
        }
    }
}