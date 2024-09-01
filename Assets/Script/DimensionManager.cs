using UnityEngine;
using UnityEngine.VFX;

public class DimensionManager : MonoBehaviour
{
    // Dimension A -> Blue / Cyan, Dimension B -> Red / Magenta
    public enum Dimension { DimensionA, DimensionB }
    private Dimension currentDimension = Dimension.DimensionA;

    [SerializeField] private MeshRenderer meshPrefabObstacleBlue;
    [SerializeField] private MeshRenderer meshPrefabObstacleRed;
    
    [SerializeField] private Material skyboxBlue;
    [SerializeField] private Material skyboxRed;
    
    [SerializeField] private Material neonMaterial;
    [SerializeField] private Color neonColorBlue;
    [SerializeField] private Color neonColorRed;
    [SerializeField] private float neonIntensityFactor = 25f;
    

    [SerializeField] private VisualEffect vfxVitesseBlue;
    [SerializeField] private VisualEffect vfxVitesseRed;

    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private ActivityManager activityManager;
    
    void Start()
    {
        ApplyDimensionChanges(); // S'adapte à la dimension de départ
    }

    public void SwitchDimension()
    {
        currentDimension = (currentDimension == Dimension.DimensionA) ? Dimension.DimensionB : Dimension.DimensionA;
        ApplyDimensionChanges();
        activityManager.UpdateObtacles(currentDimension);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.switchSound, transform.position);
    }

    private void ApplyDimensionChanges()
    {
        playerCharacter.UpdateDimension(currentDimension);
        UpdateSkybox();
        UpdateVfx();
    }

    private void UpdateSkybox()
    {
        if (currentDimension == Dimension.DimensionA)
        {
            RenderSettings.skybox = skyboxBlue;
            RenderSettings.fogColor = Color.cyan;
            neonMaterial.SetColor("_EmissionColor", neonColorBlue * neonIntensityFactor);
            

        }
        else
        {
            RenderSettings.skybox = skyboxRed;
            RenderSettings.fogColor = Color.magenta;
            neonMaterial.SetColor("_EmissionColor", neonColorRed * neonIntensityFactor);
        }
    }

    private void UpdateVfx()
    {
        vfxVitesseBlue.enabled = currentDimension == Dimension.DimensionA;
        vfxVitesseRed.enabled = currentDimension == Dimension.DimensionB;
    }
}
