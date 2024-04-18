using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform obstaclePrefabA; // Le prefab de l'obstacle A
    public Transform obstaclePrefabB; // Le prefab de l'obstacle B

    public GameObject SpawnPattern(Vector3 patternPosition)
    {
        var patternList = Resources.LoadAll("Prefabs/ObstaclePatterns", typeof(GameObject));
        var selectedPattern = patternList[Random.Range(0, patternList.Length)];
        Debug.Log("Spawned " + selectedPattern);
        
        return Instantiate(selectedPattern, patternPosition, Quaternion.identity).GameObject();
    }
}
