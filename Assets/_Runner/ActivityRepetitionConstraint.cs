using System.Collections.Generic;
using System.Linq;
using Runner;
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
        int skip = Mathf.Max(0, sequenceActivities.Count - constraintHistoryDepth - 1);
        int sameActivityCount = sequenceActivities.Skip(skip).Count(x => x == activity);
        return sameActivityCount < maxRepetitions;
    }
}