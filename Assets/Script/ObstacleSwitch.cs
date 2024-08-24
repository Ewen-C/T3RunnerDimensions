using UnityEngine;

public class ObstacleSwitch : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float crossfadeDuration = 0.2f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlaySwitchAnimation(DimensionManager.Dimension newDimension)
    {
        if (gameObject.layer == LayerMask.NameToLayer("DimensionA"))
        {
            animator.CrossFade(newDimension == DimensionManager.Dimension.DimensionA ? 
                "appear" : "dissolve", crossfadeDuration);
        }
        else
        {
            animator.CrossFade(newDimension == DimensionManager.Dimension.DimensionA ? 
                "dissolve" : "appear", crossfadeDuration);
        }
    }
}
