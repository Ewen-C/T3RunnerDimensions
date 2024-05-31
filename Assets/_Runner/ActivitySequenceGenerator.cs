using System;
using System.Collections.Generic;
using System.Linq;
using Runner;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActivitySequenceGenerator : MonoBehaviour
{
    [SerializeField] private List<WeightedActivity> possibleActivities;
    [SerializeField] private int sequenceActivitiesCount = 20;
    [SerializeField] private List<ActivityRepetitionConstraint> repetitionConstraints;

    private List<Activity> activitySequence = new();

    private void Start()
    {
        if (possibleActivities.Count == 0)
        {
            Debug.Log("No activities registered in ActivitySequenceGenerator");
            return;
        }
        
        GenerateSequence();
        GenerateSequenceGeometry();
        DebugSequenceActivities();
    }

    public void GenerateSequence()
    {
        for (int i = 0; i < sequenceActivitiesCount; i++)
        {
            float progression = i / (float)(sequenceActivitiesCount - 1);
            WeightedActivity rdActivity = GetRdWeightedActivity(progression);
            rdActivity.SetCooldown();
            
            activitySequence.Add(rdActivity.ActPublic);

            foreach (WeightedActivity weightedActivity in possibleActivities)
            {
                weightedActivity.DecreaseCooldown();
            }
        }
    }

    // Gets randomly the activity using the weighted ratio - any weight values may be used
    private WeightedActivity GetRdWeightedActivity(float progression)
    {
        ApplyRepetitionConstraints();
        
        float totalWeights = possibleActivities.Sum(x => x.GetWeightAtTime(progression));
        float rd = Random.value * totalWeights;

        for (int i = 0; i < possibleActivities.Count; i++)
        {
            if (rd < possibleActivities[i].GetWeightAtTime(progression))
                return possibleActivities[i];

            rd -= possibleActivities[i].GetWeightAtTime(progression);
        }

        return possibleActivities.Last();
    }

    private void ApplyRepetitionConstraints()
    {
        foreach (ActivityRepetitionConstraint repConstraint in repetitionConstraints)
        {
            possibleActivities.First(x => x.ActPublic == repConstraint.ActPublic)
                .SetCustomCooldown(repConstraint.ForcedCooldown);
        }
    }

    public void GenerateSequenceGeometry()
    {
        float sequenceZ = 0f;
        
        foreach (Activity activity in activitySequence)
        {
            GameObject newActivity = activity.GetGeoPrefabPublicRandom();
            float newActivityZSize = newActivity.GetComponentInChildren<MeshRenderer>().transform.localScale.z;
            
            Vector3 pos = new Vector3(0, 0, sequenceZ + newActivityZSize/2);
            ActivityGeometry newGeo = Instantiate(newActivity, pos, Quaternion.identity).GetComponent<ActivityGeometry>();
            sequenceZ += newGeo.Bounds.size.z;
        }
    }

    private void DebugSequenceActivities()
    {
        string msgLog = "";
        foreach (WeightedActivity wActivity in possibleActivities)
        {
            int count = activitySequence.Count(a => a == wActivity.ActPublic);
            msgLog += $"Generated {wActivity.ActPublic} count : {count}\n";
        }

        Debug.Log(msgLog);
    }

}