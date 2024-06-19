using Unity.VisualScripting;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Activity", menuName = "ProcGen/Activity", order = 0)]
public class Activity : ScriptableObject
{
    public enum ActivityTypeEnum
    {
        Full_Slalom,
        Main_Slalom,
        Main_Switch,
        Rest
    }
    
    [EnumPaging] /* <- REQUIRED TO MAKE ENUMS WORK WITH ODIN !! */ [SerializeField] private ActivityTypeEnum typeEnum;
    [Range(1, 30)] [SerializeField] private float difficultyScore;
    [SerializeField] private GameObject[] geometryPrefabs;

    public GameObject GetGeoPrefabPublicRandom()
    {
        return geometryPrefabs[Random.Range(0, geometryPrefabs.Length)];
    }
}