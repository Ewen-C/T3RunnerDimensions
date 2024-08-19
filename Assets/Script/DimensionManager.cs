using UnityEngine;
using UnityEngine.VFX;

public class DimensionManager : MonoBehaviour
{
    // Dimension A -> blue, Dimension B -> Red / Pink
    public enum Dimension { DimensionA, DimensionB }
    private Dimension currentDimension = Dimension.DimensionA;
    
    [SerializeField] private Material materialObstaclesBlue;
    [SerializeField] private Material materialObstaclesRed;
    
    [SerializeField] private Material skyboxBlue;
    [SerializeField] private Material skyboxRed;
    [SerializeField] private Material baseRoad;

    [SerializeField] private float vfxVitesseSpeed = 250;
    [SerializeField] private VisualEffect vfxVitesseBlue;
    [SerializeField] private VisualEffect vfxVitesseRed;

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
        UpdateVfx();
        UpdateShaders();
    }

    private void UpdateObtaclesMaterial()
    {
        Color currentColorA = materialObstaclesBlue.GetColor(Shader.PropertyToID("_BaseColor"));
        Color currentColorB = materialObstaclesRed.GetColor(Shader.PropertyToID("_BaseColor"));

        currentColorA.a = currentDimension == Dimension.DimensionA ? 0.8f : 0.1f;
        currentColorB.a = currentDimension == Dimension.DimensionB ? 0.8f : 0.1f;
        
        materialObstaclesBlue.SetColor(Shader.PropertyToID("_BaseColor"), currentColorA);
        materialObstaclesRed.SetColor(Shader.PropertyToID("_BaseColor"), currentColorB);
    }

    private void UpdateSkybox()
    {
        if (currentDimension == Dimension.DimensionA)
        {
            RenderSettings.skybox = skyboxBlue;
            baseRoad.SetColor("_EmissionColor", Color.blue);

        }
        else
        {
            RenderSettings.skybox = skyboxRed;
            baseRoad.SetColor("_EmissionColor", Color.red);
        }
    }

    private void UpdateVfx()
    {
        vfxVitesseBlue.SetFloat("vitesse", currentDimension == Dimension.DimensionA ? vfxVitesseSpeed : 0);
        vfxVitesseRed.SetFloat("vitesse", currentDimension == Dimension.DimensionB ? vfxVitesseSpeed : 0);
    }

    private void UpdateShaders()
    {
        
    }
}
