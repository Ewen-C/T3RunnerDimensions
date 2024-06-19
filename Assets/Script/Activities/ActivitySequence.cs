using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Sequence", menuName = "ProcGen/ActivitySequence", order = 0)]
public class ActivitySequence : ScriptableObject
{
     [EnumPaging] [SerializeField] private List<Activity.ActivityTypeEnum> sequenceActivities = new();
     public List<Activity.ActivityTypeEnum> GetSequence => sequenceActivities;
}
