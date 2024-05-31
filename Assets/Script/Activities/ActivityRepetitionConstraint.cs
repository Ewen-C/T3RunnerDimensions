using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class ActivityRepetitionConstraint
{
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private Activity activity;
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private int maxRepetitions = 1;
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private int constraintHistoryDepth = 1;
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private int forcedCooldown = 1;

    public Activity ActPublic => activity;
    public int ForcedCooldown => forcedCooldown;

    public bool IsConstraintActivityAllowed(List<Activity> sequenceActivities)
    {
        int skipIndex = Mathf.Max(0, sequenceActivities.Count - 1 - constraintHistoryDepth);
        int sameActivityCount = sequenceActivities.Skip(skipIndex).Count(x => x == activity);
        
        return sameActivityCount < maxRepetitions;
    }
}