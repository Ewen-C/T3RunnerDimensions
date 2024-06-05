using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActivitySpawner : MonoBehaviour
{
    [SerializeField] private int maxProgression = 50; // Avancement des types de patterns avec les courbes
    [SerializeField, ReadOnly] private int numGeneratedPatterns = 0; // Utilisé pour la progression
    
    private List<Activity> activitySequence = new(); // Patterns générés
    
    // [SerializeField] private Object[] possibleActivities;
    [SerializeField] private List<WeightedActivity> possibleActivities;
    [SerializeField] private List<ActivityRepetitionConstraint> repetitionConstraints;

    // Les 1ers patterns sont fixes, donc pas des activités
    public List<GameObject> SpawnFirstPatterns(float playerPositionZ)
    {
        // Todo : Get all activities with Resources.LoadAll 
        // patternList = Resources.LoadAll("Prefabs/ObstaclePatterns/RandomPatterns", typeof(GameObject));
        // if (patternList.Length == 0) Debug.Log("No activities registered in ActivitySpawner");
        
        List<GameObject> resPatterns = new List<GameObject>();

        Vector3 currentPosition = new Vector3(0, 0, playerPositionZ);
        Object patternStart = Resources.Load("Prefabs/ObstaclePatterns/PatternStart");
        Object patternTuto = Resources.Load("Prefabs/ObstaclePatterns/PatternTuto");
        
        // Todo : retirer le -35f
        resPatterns.Add(Instantiate(patternStart, currentPosition, Quaternion.identity).GameObject());
        currentPosition -= new Vector3(0, 0, -35f);
        resPatterns.Add(Instantiate(patternStart, currentPosition, Quaternion.identity).GameObject());
        currentPosition -= new Vector3(0, 0, -35f);
        resPatterns.Add(Instantiate(patternTuto, currentPosition, Quaternion.identity).GameObject());

        return resPatterns;
    }
    
    public GameObject SpawnActivity(Vector3 patternPosition)
    {
        return GenerateSequenceGeometry(GenerateSequence(), patternPosition);
    }

    public Activity GenerateSequence()
    {
        float currentProgression = numGeneratedPatterns < maxProgression ? (float) numGeneratedPatterns / maxProgression : 1;
        WeightedActivity rdActivity = GetRdWeightedActivity(currentProgression);
        rdActivity.SetCooldown();
        
        activitySequence.Add(rdActivity.ActPublic);
        foreach (WeightedActivity weightedActivity in possibleActivities) weightedActivity.DecreaseCooldown();
        ApplyRepetitionConstraints();
        
        // Todo : cleanup activitySequence avec le constraintHistoryDepth max trouvé
        numGeneratedPatterns++;
        
        return rdActivity.ActPublic;
    }

    public GameObject GenerateSequenceGeometry(Activity newActivity, Vector3 patternPosition)
    {
        GameObject newActivityPrefab = newActivity.GetGeoPrefabPublicRandom();
        float newActivityZSize = newActivityPrefab.GetComponentInChildren<MeshRenderer>().transform.localScale.z;
        Vector3 newActivityPos = patternPosition + new Vector3(0, 0, newActivityZSize/2);
        return Instantiate(newActivityPrefab, newActivityPos, Quaternion.identity);
    }

    // Gets randomly the activity using the weighted ratio - any weight values may be used
    private WeightedActivity GetRdWeightedActivity(float progression)
    {
        float totalWeights = possibleActivities.Sum(x => x.GetWeightAtTime(progression));
        float rd = Random.value * totalWeights;

        for (int i = 0; i < possibleActivities.Count; i++)
        {
            if (rd < possibleActivities[i].GetWeightAtTime(progression))
                return possibleActivities[i];

            rd -= possibleActivities[i].GetWeightAtTime(progression);
        }
        
        if(possibleActivities.Count == 0)
        {
            Debug.Log("No activities left !");
            return null;
        }
        return possibleActivities.Last();
    }

    // Tries to apply forced cooldown after pattern generation
    private void ApplyRepetitionConstraints()
    {
        foreach (ActivityRepetitionConstraint repConstraint in repetitionConstraints)
        {
            if (!repConstraint.IsConstraintActivityAllowed(activitySequence))
            {
                possibleActivities.First(x => x.ActPublic == repConstraint.ActPublic)
                    .SetCustomCooldown(repConstraint.ForcedCooldown);
            }
        }
    }
}
