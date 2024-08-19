using UnityEngine;
using UnityEngine.VFX;

public class DimensionManager : MonoBehaviour
{
    public enum Dimension { DimensionA, DimensionB }
    private Dimension currentDimension = Dimension.DimensionA;
    
    [SerializeField] private Material materialObstaclesA;
    [SerializeField] private Material materialObstaclesB;
    
    [SerializeField] private Material skybox_Red;
    [SerializeField] private Material skybox_Blue;
    [SerializeField] private Material baseRoad;
    [SerializeField] private VisualEffect vfxVitesse;

    [SerializeField] private PlayerCharacter playerCharacter;
    
    // Start is called before the first frame update
    void Start()
    {
        ApplyDimensionChanges();
    }

    public void SwitchDimension()
    {
        currentDimension = (currentDimension == Dimension.DimensionA) ? Dimension.DimensionB : Dimension.DimensionA;
        ApplyDimensionChanges();
    }

    private void ApplyDimensionChanges()
    {
        playerCharacter.UpdateDimension(currentDimension);
        UpdateObtaclesMaterial();
        UpdateSkybox();
        UpdateHdri();
        UpdateShaders();
    }

    private void UpdateObtaclesMaterial()
    {
        Color currentColorA = materialObstaclesA.GetColor(Shader.PropertyToID("_BaseColor"));
        Color currentColorB = materialObstaclesB.GetColor(Shader.PropertyToID("_BaseColor"));

        currentColorA.a = currentDimension == Dimension.DimensionA ? 0.8f : 0.1f;
        currentColorB.a = currentDimension == Dimension.DimensionB ? 0.8f : 0.1f;
        
        materialObstaclesA.SetColor(Shader.PropertyToID("_BaseColor"), currentColorA);
        materialObstaclesB.SetColor(Shader.PropertyToID("_BaseColor"), currentColorB);
    }

    private void UpdateSkybox()
    {
        if (currentDimension == Dimension.DimensionA)
        {
            RenderSettings.skybox = skybox_Blue;
            baseRoad.SetColor("_EmissionColor", Color.blue);

        }
        else
        {
            RenderSettings.skybox = skybox_Red;
            baseRoad.SetColor("_EmissionColor", Color.red);
        }
    }

    private void UpdateHdri()
    {
        
    }

    private void UpdateShaders()
    {
        
    }
}
