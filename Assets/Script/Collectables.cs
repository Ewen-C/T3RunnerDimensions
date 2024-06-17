using UnityEngine;

public class Collectables : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int pointsToAdd = GaugeManager.Instance.IsYellowPhase ? GaugeManager.Instance.pointsPerCollectableYellow : GaugeManager.Instance.pointsPerCollectableWhite;
            GaugeManager.Instance.AddPoints(pointsToAdd);
            //Debug.Log("Points add : " + pointsToAdd);
            Destroy(gameObject);
        }
    }
}