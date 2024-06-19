using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Activity", menuName = "ProcGen/Activity", order = 0)]
public class Activity : ScriptableObject
{
    public enum ActivityTypeEnum
    {
        Start_0_Empty,
        Start_1_Tuto,
        Full_Slalom,
        Main_Slalom, 
        Main_Switch,
        Mixed,
        Rest,
    }
    
    [EnumPaging] /* <- REQUIRED TO MAKE ENUMS WORK WITH ODIN !! */ [SerializeField] private ActivityTypeEnum typeEnum;
    [SerializeField] private GameObject[] geometryPrefabs;

    public GameObject GetGeoPrefabPublicRandom()
    {
        return geometryPrefabs[Random.Range(0, geometryPrefabs.Length)];
    }
}