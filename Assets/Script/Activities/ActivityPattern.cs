using UnityEngine;

public class ActivityPattern : MonoBehaviour
{
    [Range(0, 30)] [SerializeField] private int difficultyScore;
    public float GetDifficultyScore => difficultyScore;
    [SerializeField] private Collider boundsCollider;
    public Bounds Bounds => boundsCollider.bounds;
    [SerializeField] private int cooldown = 0;
    
    public void SetCooldown(int newCooldown) => cooldown = newCooldown;
    public bool DecrementCooldownAndCheckZero()
    {
        cooldown--;
        return cooldown == 0;
    }
}
