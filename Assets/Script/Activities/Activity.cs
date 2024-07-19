using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Activity", menuName = "ProcGen/Activity", order = 0)]
public class Activity : ScriptableObject
{
    public enum ActivityType
    {
        Start_Empty,
        Start_Tuto,
        Full_Slalom,
        Main_Slalom, 
        Main_Switch,
        Mixed,
        Rest,
    }
    
[EnumPaging] /* <- REQUIRED TO MAKE ENUMS WORK WITH ODIN !! */ [SerializeField] private ActivityType activityType;
[SerializeField] private GameObject[] geometryPrefabs;
    
    public GameObject GetGeoPrefabPublicRandom(List<GameObject> disabledPatterns)
    {
        List<GameObject> availablePatterns = new();

        for (int i = 0; i < geometryPrefabs.Length; i++)
            if (!disabledPatterns.Contains(geometryPrefabs[i])) availablePatterns.Add(geometryPrefabs[i]);

        if(availablePatterns.Count == 0) Debug.Log($"No patterns left in {name} !");
        
        return availablePatterns[Random.Range(0, availablePatterns.Count)]; 
    }
}