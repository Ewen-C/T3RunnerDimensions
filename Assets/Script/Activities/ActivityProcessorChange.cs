using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class ActivityProcessorChange
{
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private Activity[] activitiesCondition;
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private int constraintHistoryDepth = 5;

    // public Activity ActPublic => activity;
    //
    // public bool IsConstraintActivityAllowed(List<Activity> sequenceActivities)
    // {
    //     int skipIndex = Mathf.Max(0, sequenceActivities.Count - 1 - constraintHistoryDepth);
    //     int sameActivityCount = sequenceActivities.Skip(skipIndex).Count(x => x == activity);
    //     
    //     return sameActivityCount < maxRepetitions;
    // }
}