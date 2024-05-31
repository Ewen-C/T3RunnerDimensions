using UnityEngine;


[CreateAssetMenu(fileName = "Activity", menuName = "ProcGen/Activity", order = 0)]
public class Activity : ScriptableObject
{
    [SerializeField] private GameObject[] geometryPrefabs;

    public GameObject GetGeoPrefabPublicRandom()
    {
        return geometryPrefabs[Random.Range(0, geometryPrefabs.Length)];
    }
}