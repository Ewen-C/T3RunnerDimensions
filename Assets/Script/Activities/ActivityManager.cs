using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    public float groundMoveSpeed = 10f; // La vitesse de défilement des patterns
    public int maxGrounds = 7; // Nombre de patterns à maintenir à l'écran
    private float playerPositionZ; // La référence au joueur
    public float patternRemovalDistance = 15f; // Distance du joueur où les patterns sont détruits

    private ActivitySpawner activitySpawner;
    private List<GameObject> spawnedPatterns = new(); // Prefabs des patterns
    
    [SerializeField] private float startingPatternDifficulty = 30;
    [SerializeField] private float patternDifficultyIncrement = 5;
    [SerializeField] private float maxPatternDifficulty = 100;
    private float currentPatternDifficulty;
    
    private void Start()
    {
        playerPositionZ = GameObject.FindGameObjectWithTag("Player").transform.position.z;
        activitySpawner = GetComponent<ActivitySpawner>();
        
        // Instancie les premiers sols
        float firstPatternPositionZ = playerPositionZ - patternRemovalDistance;
        spawnedPatterns = new List<GameObject>(activitySpawner.SpawnFirstPatterns(firstPatternPositionZ));
         
        // Instancie le reste des sols, jusqu'à atteindre maxGrounds
        for (int i = spawnedPatterns.Count; i < maxGrounds; i++)
        {
            CreateAndRegisterActivity();
        }
    }

    private void Update() // FixedUpdate ?
    {
        // Déplacer tous les sols vers le joueur
        foreach (GameObject pattern in spawnedPatterns)
        {
            pattern.transform.position += Vector3.back * (groundMoveSpeed * Time.deltaTime);
        }

        RemovePreviousActivityAndSpawn();
    }
    
    private void RemovePreviousActivityAndSpawn()
    {
        // Si le premier pattern de la liste est derrière le joueur, le détruire (return condition inverse)
        if (spawnedPatterns.Count == 0 || spawnedPatterns[0].transform.position.z > playerPositionZ - patternRemovalDistance) return;
        
        Destroy(spawnedPatterns[0]);
        spawnedPatterns.RemoveAt(0);
        CreateAndRegisterActivity(); // Instancie un nouveau pattern
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CreateAndRegisterActivity()
    {
        spawnedPatterns.Add(activitySpawner.SpawnActivity(GetSpawnPositionZ()));
    }

    private float GetSpawnPositionZ()
    {
        float currentPositionZ = spawnedPatterns[0].transform.position.z -
                                 (spawnedPatterns[0].GetComponentInChildren<MeshRenderer>().transform.localScale.z) / 2;

        for (int i = 0; i < spawnedPatterns.Count; i++)
        {
            currentPositionZ += spawnedPatterns[i].GetComponentInChildren<MeshRenderer>().transform.localScale.z;
        }

        return currentPositionZ;
    }
}
