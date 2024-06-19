using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityGeometry : MonoBehaviour
{
    [SerializeField] private Collider boundsCollider;
    public Bounds Bounds => boundsCollider.bounds;
    
    [Range(0, 30)] [SerializeField] private int difficultyScore;
    public float GetDifficultyScore => difficultyScore;

}
