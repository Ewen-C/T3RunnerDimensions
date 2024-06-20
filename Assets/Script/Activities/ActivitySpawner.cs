using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActivitySpawner : MonoBehaviour
{
    [SerializeField] private int maxProgression = 50; // Avancement des types de patterns avec les courbes
    [SerializeField, ReadOnly] private int numGeneratedPatterns; // Utilisé pour la progression
    
    // private List<Activity> activitySequence = new(); // Activités des patterns spawnés
    private List<GameObject> patternSequence = new(); // Prefabs des patterns spawnés
    
    [SerializeField] private List<Activity> firstActivities;
    [SerializeField] private List<WeightedActivity> randomActivities;
    [SerializeField] private List<ActivityPatternConstraint> patternConstraints;

    public List<GameObject> SpawnFirstPatterns(float currentPositionZ)
    {
        List<GameObject> firstPatternsPrefabs = new List<GameObject>();

        for (int i = 0; i < firstActivities.Count; i++)
        {
            firstPatternsPrefabs.Add(GenerateSequenceGeometry(firstActivities[i], new Vector3(0, 0, currentPositionZ)));
            currentPositionZ += firstPatternsPrefabs[i].GetComponentInChildren<MeshRenderer>().transform.localScale.z;
        }
        
        return firstPatternsPrefabs;
    }
    
    public GameObject SpawnActivity(float patternPositionZ)
    {
        return GenerateSequenceGeometry(GenerateSequence(), new Vector3(0, 0, patternPositionZ));
    }

    public Activity GenerateSequence()
    {
        float currentProgression = numGeneratedPatterns < maxProgression ? (float) numGeneratedPatterns / maxProgression : 1;
        WeightedActivity rdActivity = GetRdWeightedActivity(currentProgression);
        rdActivity.SetCooldown();
        
        // Todo? : cleanup activitySequence avec le constraintHistoryDepth max trouvé
        // activitySequence.Add(rdActivity.ActPublic);
        foreach (WeightedActivity weightedActivity in randomActivities) weightedActivity.DecreaseCooldown();
        numGeneratedPatterns++;
        
        return rdActivity.ActPublic;
    }

    public GameObject GenerateSequenceGeometry(Activity newActivity, Vector3 patternPositionZ)
    {
        GameObject newActivityPrefab = newActivity.GetGeoPrefabPublicRandom();
        patternSequence.Add(newActivityPrefab);
        
        foreach (WeightedActivity weightedActivity in randomActivities) weightedActivity.ActPublic.DecreaseCooldown();
        ApplyRepetitionConstraints(newActivity);
        
        float newActivityZSize = newActivityPrefab.GetComponentInChildren<MeshRenderer>().transform.localScale.z;
        Vector3 newActivityPos = patternPositionZ + new Vector3(0, 0, newActivityZSize/2);
        return Instantiate(newActivityPrefab, newActivityPos, Quaternion.identity);
    }

    // Gets randomly the activity using the weighted ratio - any weight values may be used
    private WeightedActivity GetRdWeightedActivity(float progression)
    {
        float totalWeights = randomActivities.Sum(x => x.GetWeightAtTime(progression));
        float rd = Random.value * totalWeights;

        for (int i = 0; i < randomActivities.Count; i++)
        {
            if (rd < randomActivities[i].GetWeightAtTime(progression))
                return randomActivities[i];

            rd -= randomActivities[i].GetWeightAtTime(progression);
        }
        
        if(randomActivities.Count == 0)
        {
            Debug.Log("No activities left !");
            return null;
        }
        return randomActivities.Last();
    }

    
    // Tries to apply forced cooldown after pattern generation
    
    private void ApplyRepetitionConstraints(Activity newActivity)
    {
        foreach (ActivityPatternConstraint repConstraint in patternConstraints)
        {
            if (repConstraint.PatternRepetitionsCheck(patternSequence))
                newActivity.DisablePatternWithCooldown(repConstraint.TargetPattern, repConstraint.ForcedCooldown);
        }
    }
}
