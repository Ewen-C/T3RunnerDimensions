using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform obstaclePrefabA; // Le prefab de l'obstacle A
    public Transform obstaclePrefabB; // Le prefab de l'obstacle B
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObstacle(Transform ground, bool spawnInDimensionA)
    {
        if (!(Random.value < 0.9f)) return; // 90% de chance de créer un obstacle
        
        float positionX = Random.Range(-2f, 2f); // Position aléatoire sur l'axe X
        Vector3 obstaclePosition = new Vector3(positionX, 0.5f, 0); // Ajuster Y si nécessaire pour s'assurer que l'obstacle repose sur le sol
        obstaclePosition += ground.position; // Placer l'obstacle relativement à la position du sol
            
        // Choisir le prefab en fonction de la dimension
        Transform chosenPrefab = spawnInDimensionA ? obstaclePrefabA : obstaclePrefabB;
            
        // Instancier l'obstacle sur le sol
        Transform newObstacle = Instantiate(chosenPrefab, obstaclePosition, Quaternion.identity, ground);
        newObstacle.gameObject.layer = spawnInDimensionA ? LayerMask.NameToLayer("DimensionA") : LayerMask.NameToLayer("DimensionB");
    }
}
