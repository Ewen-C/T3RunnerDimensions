using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float groundMoveSpeed = 10f; // La vitesse de défilement du sol
    public int maxGrounds = 7; // Nombre de sols à maintenir à l'écran
    public float spaceBetweenGround = 20f; // Espacement entre les sols

    [SerializeField] private ObstacleSpawner obstacleSpawner;

    private Transform player; // La référence au joueur
    private List<GameObject> patterns = new(); // Liste des sols actuellement affichés

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouver le joueur par tag
        obstacleSpawner = GetComponent<ObstacleSpawner>();

        // Instancier les premiers sols
        patterns.Add(obstacleSpawner.SpawnPatternStart(GetSpawnPosition()));
        patterns.Add(obstacleSpawner.SpawnPatternStart(GetSpawnPosition()));
        patterns.Add(obstacleSpawner.SpawnPatternTuto(GetSpawnPosition()));
        
        for (int i = patterns.Count; i < maxGrounds; i++)
        {
            CreateAndRegisterPattern();
        }
    }

    private void Update()
    {
        // Déplacer tous les sols vers le joueur
        foreach (GameObject pattern in patterns)
        {
            pattern.transform.position += Vector3.back * (groundMoveSpeed * Time.deltaTime);
        }

        RemovePreviousPattern();
    }
    
    private void RemovePreviousPattern()
    {
        // Si le premier pattern est derrière le joueur, le détruire 
        if (patterns.Count <= 0 || !(patterns[0].transform.position.z < (player.position.z - 30f))) return;
        Destroy(patterns[0]);
        patterns.RemoveAt(0);
        CreateAndRegisterPattern(); // Instancier un nouveau pattern
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CreateAndRegisterPattern()
    {
        patterns.Add(obstacleSpawner.SpawnPattern(GetSpawnPosition()));
    }

    private Vector3 GetSpawnPosition()
    {
        // TODO : Calcul dynamique de la position des sols à la place de -35
        return new Vector3(0, player.position.y, player.position.z) - new Vector3(0, 0.15f, -35f * patterns.Count);
    }
}