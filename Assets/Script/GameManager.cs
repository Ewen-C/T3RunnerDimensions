using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform groundPrefab; // Le prefab du sol

    public float groundSpeed = 5f; // La vitesse de défilement du sol
    public int numGrounds = 3; // Nombre de sols à maintenir à l'écran
    public float spaceBetweenGround = 38f; // Espacement entre les sols

    [SerializeField] private ObstacleSpawner obstacleSpawner;

    private Transform player; // La référence au joueur
    private List<Transform> grounds = new List<Transform>(); // Liste des sols actuellement affichés

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouver le joueur par tag
        obstacleSpawner = GetComponent<ObstacleSpawner>();

        // Instancier les premiers sols
        for (int i = 0; i < numGrounds; i++)
        {
            SpawnGround();
        }
    }

    private void Update()
    {
        // Déplacer tous les sols vers le joueur
        foreach (Transform ground in grounds)
        {
            ground.position += Vector3.back * (groundSpeed * Time.deltaTime);
        }

        // Si le premier sol est derrière le joueur, le détruire et en instancier un nouveau
        if (grounds[0].position.z < (player.position.z - 30f))
        {
            Destroy(grounds[0].gameObject);
            grounds.RemoveAt(0);
            SpawnGround();
        }
    }

    private void SpawnGround()
    {
        // Calculer la position du prochain sol en fonction du dernier sol de la liste
        Vector3 spawnPosition = player.position - new Vector3(0, 0.5f, -16f);
        if (grounds.Count > 0)
        {
            spawnPosition = grounds[^1].position + Vector3.forward * spaceBetweenGround;
        }
        Transform newGround = Instantiate(groundPrefab, spawnPosition, Quaternion.identity);
        grounds.Add(newGround);
        
        // Appeler SpawnObstacle pour générer des obstacles sur le nouveau sol
        bool spawnInDimensionA = Random.value < 0.5; // 50% chance pour chaque dimension
        obstacleSpawner.SpawnObstacle(newGround, spawnInDimensionA);
    }
}