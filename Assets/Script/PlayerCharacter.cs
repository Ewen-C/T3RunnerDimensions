using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    private int collisionCount = 0; // Compteur de collisions
    
    public Text hitText;
    public GameObject feedbackPrefab;
    public float displayTime = 0.2f;
    
    public int dimensionALayer;
    public int dimensionBLayer;

    [SerializeField] private Material materialDimensionA;
    [SerializeField] private Material materialDimensionB;
    [SerializeField] private Material materialObstaclesA;
    [SerializeField] private Material materialObstaclesB;
    
    public enum Dimension { DimensionA, DimensionB }
    public Dimension currentDimension = Dimension.DimensionA;
    
    void Start()
    {
        // Initialisation pour s'assurer que l'état initial est correct
        UpdateMaterialTransparency();
    }
    
    void Update()
    {
        hitText.text = "Hit : " + collisionCount; // Met à jour le texte affiché
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obstacle")) return;
        
        collisionCount++; // Incrémenter le compteur de collisions
        GameObject feedbackInstance = Instantiate(feedbackPrefab, transform.position, Quaternion.identity);
        Debug.Log("Collision detected! Total hits: " + collisionCount);
        Destroy(feedbackInstance, displayTime);
    }
    
    public void SwitchDimension()
    {
        currentDimension = (currentDimension == Dimension.DimensionA) ? Dimension.DimensionB : Dimension.DimensionA;
        gameObject.layer = (currentDimension == Dimension.DimensionA) ? dimensionALayer : dimensionBLayer;
        GetComponent<Renderer>().material = 
            (currentDimension == Dimension.DimensionA) ? materialDimensionA : materialDimensionB;
        
        UpdateMaterialTransparency();
        
        Debug.Log("Switched to Dimension" + (currentDimension == Dimension.DimensionA ? "A" : " B"));
    }
    
    private void UpdateMaterialTransparency()
    {
        Color currentColorA = materialObstaclesA.GetColor(Shader.PropertyToID("_BaseColor"));
        Color currentColorB = materialObstaclesB.GetColor(Shader.PropertyToID("_BaseColor"));

        currentColorA.a = currentDimension == Dimension.DimensionA ? 0.8f : 0.1f;
        currentColorB.a = currentDimension == Dimension.DimensionB ? 0.8f : 0.1f;
        
        materialObstaclesA.SetColor(Shader.PropertyToID("_BaseColor"), currentColorA);
        materialObstaclesB.SetColor(Shader.PropertyToID("_BaseColor"), currentColorB);
    }
}
