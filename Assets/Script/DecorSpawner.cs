using System;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections;

public class DecorSpawner : MonoBehaviour
{
    public enum Type { Column, Arc }
    private Type currentDecorType = Type.Column;
    
    [SerializeField] private int nbSpawnTypeChange = 25;
    private int currentNbSpawn = 0;
    
    [Serializable, EnumPaging]
    public struct StructDecors {
        public Type type;
        public GameObject prefabToSpawn;
    }
    public StructDecors[] arrayItemsToSpawn;
}