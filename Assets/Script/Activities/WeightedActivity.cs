using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
[InlineProperty(LabelWidth = 50)]
public class WeightedActivity
{
    [HorizontalGroup(180)]
    [SerializeField] private Activity activity;
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private float Weight = 1f; // Proba initiale (à comparer aux autres)
    [HorizontalGroup(150)]
    [SerializeField] private AnimationCurve curveEvolution; // Variation du la proba avec le temps
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private int maxCooldown = 1; // Empêche les patterns de se répéter toutes les x intérations
    [HorizontalGroup(100), LabelWidth(50)]
    [SerializeField] private int currentCooldown;
    
    public Activity ActPublic => activity;
    public void SetCooldown() => currentCooldown = maxCooldown;
    public void DecreaseCooldown() => currentCooldown--;
    public float GetWeightAtTime(float t) => currentCooldown > 0 ? 0 : curveEvolution.Evaluate(t) * Weight;
    
    public void SetCustomCooldown(int newCooldown) => currentCooldown = newCooldown;
}