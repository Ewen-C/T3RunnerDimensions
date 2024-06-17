using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public float groundMoveSpeed = 10f; // La vitesse de défilement des patterns
    public int maxGrounds = 7; // Nombre de patterns à maintenir à l'écran
    private float patternRemovalDistance = 30f; // Distance du joueur où les patterns sont détruits

    private ObstacleSpawner obstacleSpawner;

    private Transform player; // La référence au joueur
    private List<GameObject> patterns = new(); // Liste des patterns actuellement affichés

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouver le joueur par tag
        obstacleSpawner = GetComponent<ObstacleSpawner>();

        // Instancie les premiers sols
        patterns = new List<GameObject>(obstacleSpawner.SpawnFirstPatterns(player.position));
         
        // Instancie le reste des sols
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
        // Si le premier pattern de la liste est derrière le joueur, le détruire 
        if (patterns.Count <= 0 || !(patterns[0].transform.position.z < (player.position.z - patternRemovalDistance))) return;
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
        return new Vector3(0, player.position.y, player.position.z) - new Vector3(0, 0, -35f * patterns.Count);
    }
}