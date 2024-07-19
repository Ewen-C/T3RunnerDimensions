using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActivitySpawner : MonoBehaviour
{
    [SerializeField] private int maxProgression = 50; // Avancement des types de patterns avec les courbes
    [SerializeField, ReadOnly] private int numGeneratedPatterns; // Utilisé pour la progression
    
    private List<GameObject> patternsSpawned = new();
    private List<GameObject> disabledPatterns = new(); // Patterns désactivés, en cooldown 

    [SerializeField] private ActivitySequence firstSequence;
    [SerializeField] private List<ActivitySequence> randomSequences;
    
    [SerializeField] private List<Activity> firstActivities;
    [SerializeField] private List<WeightedActivity> randomActivities;
    [SerializeField] private List<ActivityPatternConstraint> patternConstraints;
    
    public List<GameObject> SpawnFirstPatterns(float currentPositionZ)
    {
        List<GameObject> firstPatternsPrefabs = new List<GameObject>();

        for (int i = 0; i < firstActivities.Count; i++)
        {
            firstPatternsPrefabs.Add(GenerateActivityPrefab(firstActivities[i], new Vector3(0, 0, currentPositionZ)));
            currentPositionZ += firstPatternsPrefabs[i].GetComponentInChildren<MeshRenderer>().transform.localScale.z;
        }
        
        return firstPatternsPrefabs;
    }

    public List<GameObject> SpawnFirstSequence(float patternPositionZ)
    {
        List<GameObject> firstPatterns = new();

        foreach (Activity.ActivityType sequenceActivityType in firstSequence.GetSequenceActivities)
        {
            firstPatterns.Add(SpawnActivity(patternPositionZ, sequenceActivityType));
        }
        
        return firstPatterns;
    }

    public List<GameObject> SpawnSequence(float patternPositionZ)
    {
        List<GameObject> newPatterns = new();
        ActivitySequence rdSequence = randomSequences[Random.Range(0, randomSequences.Count)];

        foreach (Activity.ActivityType sequenceActivityType in rdSequence.GetSequenceActivities)
        { 
            newPatterns.Add(SpawnActivity(patternPositionZ, sequenceActivityType));
        }
        
        return newPatterns;
    }
    
    public GameObject SpawnActivity(float patternPositionZ, Activity.ActivityType type)
    {
      // Activity newSequence = GenerateSequence();
      // ApplyProcessorChanges();
      // return GenerateSequenceGeometry(newSequence, new Vector3(0, 0, patternPositionZ));
  
        return GenerateActivityPrefab(GenerateActivity(type), new Vector3(0, 0, patternPositionZ));
    }

    public Activity GenerateActivity(Activity.ActivityType type)
    {
        float currentProgression = numGeneratedPatterns < maxProgression ? (float) numGeneratedPatterns / maxProgression : 1;
        WeightedActivity rdActivity = GetRdWeightedActivity(currentProgression);
        rdActivity.SetCooldown();
        
        // Todo? : cleanup patternsSpawned avec le historyCheck max trouvé
        foreach (WeightedActivity weightedActivity in randomActivities) weightedActivity.DecreaseCooldown();
        numGeneratedPatterns++;
        
        return rdActivity.ActPublic;
    }

    public GameObject GenerateActivityPrefab(Activity newActivity, Vector3 patternPositionZ)
    {
        GameObject newActivityPrefab = newActivity.GetGeoPrefabPublicRandom(disabledPatterns);
        patternsSpawned.Add(newActivityPrefab);
        
        DecreasePatternCooldown();
        ApplyRepetitionConstraints();
        
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


    private void DecreasePatternCooldown()
    {
        for (int i = 0; i < disabledPatterns.Count; i++)
        {
            if (disabledPatterns[i].GetComponent<ActivityPattern>().DecrementCooldownAndCheckZero())
                disabledPatterns.RemoveAt(i);
        }
    }
    
    // Tries to apply forced cooldown after pattern generation
    
    private void ApplyRepetitionConstraints()
    {
        foreach (ActivityPatternConstraint repConstraint in patternConstraints)
        {
            Debug.Log($"repConstraint : {repConstraint}");
            if (repConstraint.PatternRepetitionsCheck(patternsSpawned) && !disabledPatterns.Contains(repConstraint.TargetPattern))
            {
                repConstraint.TargetPattern.GetComponentInChildren<ActivityPattern>().SetCooldown(repConstraint.ForcedCooldown);
                disabledPatterns.Add(repConstraint.TargetPattern);
            }
        }
    }


    private void ApplyProcessorChanges()
    {
        
    }
}
