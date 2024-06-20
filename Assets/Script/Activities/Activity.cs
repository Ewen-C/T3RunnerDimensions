using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Activity", menuName = "ProcGen/Activity", order = 0)]
public class Activity : ScriptableObject
{
    [SerializeField] private GameObject[] geometryPrefabs;
    private List<GameObject> disabledPatterns;
    
    public GameObject GetGeoPrefabPublicRandom()
    {
        List<GameObject> availablePatterns = new();

        for (int i = 0; i < geometryPrefabs.Length; i++)
            if (!disabledPatterns.Contains(geometryPrefabs[i])) availablePatterns.Add(geometryPrefabs[i]);

        // DecreaseCooldown();
        
        if(availablePatterns.Count == 0) Debug.Log($"No patterns left in {name} !");
        
        return availablePatterns[Random.Range(0, availablePatterns.Count)]; 
    }

    public void DisablePatternWithCooldown(GameObject targetPattern, int cooldown)
    {
        targetPattern.GetComponentInChildren<ActivityPattern>().SetCooldown(cooldown);
        disabledPatterns.Add(targetPattern);
    }

    public void DecreaseCooldown()
    {
        for (int i = 0; i < disabledPatterns.Count; i++)
        {
            if (disabledPatterns[i].GetComponent<ActivityPattern>().DecrementCooldownAndCheckZero())
                disabledPatterns.RemoveAt(i);
        }
    }
}