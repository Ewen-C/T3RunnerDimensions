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

    public GameObject GetGeoPrefabPublicRandom()
    {
        return geometryPrefabs[Random.Range(0, geometryPrefabs.Length)];
    }
}