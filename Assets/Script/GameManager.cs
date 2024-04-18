using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float groundMoveSpeed = 10f; // La vitesse de défilement du sol
    public int maxGrounds = 6; // Nombre de sols à maintenir à l'écran
    public float spaceBetweenGround = 20f; // Espacement entre les sols

    [SerializeField] private ObstacleSpawner obstacleSpawner;

    private Transform player; // La référence au joueur
    private List<Transform> grounds = new(); // Liste des sols actuellement affichés

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouver le joueur par tag
        obstacleSpawner = GetComponent<ObstacleSpawner>();

        // Instancier les premiers sols
        for (int i = 0; i < maxGrounds; i++)
        {
            CreateAndRegisterPattern();
        }
    }

    private void Update()
    {
        // Déplacer tous les sols vers le joueur
        foreach (Transform ground in grounds)
        {
            ground.position += Vector3.back * (groundMoveSpeed * Time.deltaTime);
        }

        RemovePreviousGround();
    }
    
    private void RemovePreviousGround()
    {
        // Si le premier sol est derrière le joueur, détruire le Prefab pattern et en instancier un nouveau
        if (grounds.Count <= 0 || !(grounds[0].position.z < (player.position.z - 30f))) return;
        Destroy(grounds[0].parent.gameObject);
        grounds.RemoveAt(0);
        CreateAndRegisterPattern();
    }

    private void CreateAndRegisterPattern()
    {
        // TODO : Calcul dynamique de la taille des sols à la place de -80
        
        Vector3 spawnPosition = new Vector3(0, player.position.y, player.position.z) - new Vector3(0, 0.5f, -35f * grounds.Count);
        GameObject newPattern = obstacleSpawner.SpawnPattern(spawnPosition);
        Transform newGroundPosition = newPattern.transform.GetChild(0).transform; // Returns ground transform
        grounds.Add(newGroundPosition);
    }
}