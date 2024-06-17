using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class ObstacleSpawner : MonoBehaviour
{
    private Object[] patternList;

    public List<GameObject> SpawnFirstPatterns(Vector3 playerPosition)
    {
        patternList = Resources.LoadAll("Prefabs/ObstaclePatterns/RandomPatterns", typeof(GameObject));
        List<GameObject> resPatterns = new List<GameObject>();

        Vector3 currentPosition = new Vector3(0, playerPosition.y, playerPosition.z);
        Object patternStart = Resources.Load("Prefabs/ObstaclePatterns/PatternStart");
        Object patternTuto = Resources.Load("Prefabs/ObstaclePatterns/PatternTuto");
        
        resPatterns.Add(Instantiate(patternStart, currentPosition, Quaternion.identity).GameObject());
        currentPosition -= new Vector3(0, 0, -35f);
        resPatterns.Add(Instantiate(patternStart, currentPosition, Quaternion.identity).GameObject());
        currentPosition -= new Vector3(0, 0, -35f);
        resPatterns.Add(Instantiate(patternTuto, currentPosition, Quaternion.identity).GameObject());

        return resPatterns;
    }
    
    public GameObject SpawnPattern(Vector3 patternPosition)
    {
        var selectedPattern = patternList[Random.Range(0, patternList.Length)];
        Debug.Log("Spawned " + selectedPattern);
        
        return Instantiate(selectedPattern, patternPosition, Quaternion.identity).GameObject();
    }
}
