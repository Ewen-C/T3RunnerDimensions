using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform obstaclePrefabA; // Le prefab de l'obstacle A
    public Transform obstaclePrefabB; // Le prefab de l'obstacle B
    
    public GameObject SpawnPatternStart(Vector3 patternPosition)
    {
        Object patternStart = Resources.Load("Prefabs/ObstaclePatterns/PatternStart");
        return Instantiate(patternStart, patternPosition, Quaternion.identity).GameObject(); 
    }
    
    public GameObject SpawnPatternTuto(Vector3 patternPosition)
    {
        Object patternTuto = Resources.Load("Prefabs/ObstaclePatterns/PatternTuto");
        return Instantiate(patternTuto, patternPosition, Quaternion.identity).GameObject(); 
    }

    public GameObject SpawnPattern(Vector3 patternPosition)
    {
        var patternList = Resources.LoadAll("Prefabs/ObstaclePatterns/RandomPatterns", typeof(GameObject));
        var selectedPattern = patternList[Random.Range(0, patternList.Length)];
        Debug.Log("Spawned " + selectedPattern);
        
        return Instantiate(selectedPattern, patternPosition, Quaternion.identity).GameObject();
    }
}
