using UnityEngine;

public class ActivityPattern : MonoBehaviour
{
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
