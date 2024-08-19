using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int dimensionALayer;
    [SerializeField] private int dimensionBLayer;
    [SerializeField] private Material materialPlayerA;
    [SerializeField] private Material materialPlayerB;

    [SerializeField] private bool debugMode;

    private new Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (debugMode) return;
        
        if (other.CompareTag("ObstacleDimension") || other.CompareTag("ObstacleFixe"))
        {
            Debug.Log("HIT " + other.tag);
            PlayerManager.gameOver = true;
            GetComponentInParent<PlayerController>().UpdateGameOver(true);
        } 
    }
    
    public void UpdateDimension(DimensionManager.Dimension newDimension)
    {
        gameObject.layer = (newDimension == DimensionManager.Dimension.DimensionA) ? dimensionALayer : dimensionBLayer;
        renderer.material = (newDimension == DimensionManager.Dimension.DimensionA) ? materialPlayerA : materialPlayerB;

        // Debug.Log("Switched to Dimension" + (currentDimension == Dimension.DimensionA ? "A" : " B"));
    }
}
