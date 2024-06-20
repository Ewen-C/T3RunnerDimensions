using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class ActivityPatternConstraint
{
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private GameObject pattern;
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private int maxRepetitions;
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private int constraintHistoryDepth;
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private int forcedCooldown;

    public int ForcedCooldown => forcedCooldown;
    public GameObject TargetPattern => pattern;

    // Check si une des constraintes s'applique
    public bool PatternRepetitionsCheck(List<GameObject> sequencePatterns)
    {
        int skipIndex = Mathf.Max(0, sequencePatterns.Count - 1 - constraintHistoryDepth);
        int samepatternCount = sequencePatterns.Skip(skipIndex).Count(x => x == pattern);
        
        return samepatternCount >= maxRepetitions;
    }
}