using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityGeometry : MonoBehaviour
{
    [SerializeField] private Collider boundsCollider;
    public Bounds Bounds => boundsCollider.bounds;

}
