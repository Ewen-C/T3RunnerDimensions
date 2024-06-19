using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int dimensionALayer;
    [SerializeField] private int dimensionBLayer;
    [SerializeField] private Material materialDimensionA;
    [SerializeField] private Material materialDimensionB;
    [SerializeField] private Material materialObstaclesA;
    [SerializeField] private Material materialObstaclesB;
    
    public enum Dimension { DimensionA, DimensionB }
    public Dimension currentDimension = Dimension.DimensionA;

    public Material skybox_Red;
    public Material skybox_Blue;
    
    void Start()
    {
        // Initialisation pour s assurer que l état initial est correct
        UpdateMaterialTransparency();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleDimension"))
        {
            Debug.Log("HIT Dimension!");
            PlayerManager.gameOver = true;
        }
        else if (other.CompareTag("ObstacleFixe"))
        {
            Debug.Log("HIT Fixe!");
            PlayerManager.gameOver = true;
        } 
    }
    
    public void SwitchDimension()
    {
        currentDimension = (currentDimension == Dimension.DimensionA) ? Dimension.DimensionB : Dimension.DimensionA;
        gameObject.layer = (currentDimension == Dimension.DimensionA) ? dimensionALayer : dimensionBLayer;
        GetComponent<Renderer>().material = 
            (currentDimension == Dimension.DimensionA) ? materialDimensionA : materialDimensionB;
        
        UpdateMaterialTransparency();
        ChangeSkybox();
        
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

    private void ChangeSkybox()
    {
        if (currentDimension == Dimension.DimensionA)
        {
            RenderSettings.skybox = skybox_Blue;
        }
        else if (currentDimension == Dimension.DimensionB)
        {
            RenderSettings.skybox = skybox_Red;
        }
    }
}
