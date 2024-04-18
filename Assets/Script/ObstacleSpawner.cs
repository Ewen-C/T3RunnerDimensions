using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform obstaclePrefabA; // Le prefab de l'obstacle A
    public Transform obstaclePrefabB; // Le prefab de l'obstacle B

    public void SpawnObstacle(Transform ground)
    {
        if (Random.value < 0.1f) return; // 90% de chance de créer un obstacle
        
        bool spawnInDimensionA = Random.value < 0.5; // 50% chance pour chaque dimension
        float positionX = Random.Range(-2f, 2f); // Position aléatoire sur l'axe X
        
        Vector3 obstaclePosition = new Vector3(positionX, 0.5f, 0); // Ajuster Y si nécessaire pour s'assurer que l'obstacle repose sur le sol
        obstaclePosition += ground.position; // Placer l'obstacle relativement à la position du sol
            
        // Choisir le prefab en fonction de la dimension
        Transform chosenPrefab = spawnInDimensionA ? obstaclePrefabA : obstaclePrefabB;
            
        // Instancier l'obstacle sur le sol
        Transform newObstacle = Instantiate(chosenPrefab, obstaclePosition, Quaternion.identity, ground);
        newObstacle.gameObject.layer = spawnInDimensionA ? LayerMask.NameToLayer("DimensionA") : LayerMask.NameToLayer("DimensionB");
    }

    public GameObject SpawnPattern(Vector3 patternPosition)
    {
        var patternList = Resources.LoadAll("Prefabs/ObstaclePatterns", typeof(GameObject));
        var selectedPattern = patternList[Random.Range(0, patternList.Length)];
        Debug.Log("Spawned " + selectedPattern);
        
        return Instantiate(selectedPattern, patternPosition, Quaternion.identity).GameObject();
    }
}
