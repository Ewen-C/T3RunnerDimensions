using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DimensionManager : MonoBehaviour
{
    // Dimension A -> blue, Dimension B -> Red / Pink
    public enum Dimension { DimensionA, DimensionB }
    private Dimension currentDimension = Dimension.DimensionA;

    [SerializeField] private MeshRenderer meshPrefabObstacleBlue;
    [SerializeField] private MeshRenderer meshPrefabObstacleRed;
    
    [SerializeField] private Material skyboxBlue;
    [SerializeField] private Material skyboxRed;
    [SerializeField] private Material baseRoad;

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
            baseRoad.SetColor("_EmissionColor", Color.blue);
            RenderSettings.fogColor = Color.cyan;

        }
        else
        {
            RenderSettings.skybox = skyboxRed;
            baseRoad.SetColor("_EmissionColor", Color.red);
            RenderSettings.fogColor = Color.magenta;
        }
    }

    private void UpdateVfx()
    {
        vfxVitesseBlue.enabled = currentDimension == Dimension.DimensionA;
        vfxVitesseRed.enabled = currentDimension == Dimension.DimensionB;
    }
}
