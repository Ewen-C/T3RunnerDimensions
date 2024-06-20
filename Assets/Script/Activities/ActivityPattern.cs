using UnityEngine;

public class ActivityPattern : MonoBehaviour
{
    [SerializeField] private Collider boundsCollider;
    public Bounds Bounds => boundsCollider.bounds;
    private int cooldown;
    
    public void SetCooldown(int newCooldown) => cooldown = newCooldown;
    public bool DecrementCooldownAndCheckZero()
    {
        cooldown--;
        return cooldown == 0;
    }
}
