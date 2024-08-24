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
    
    // [SerializeField] private List<Animator> animsObstacles;
    [SerializeField] private Animator testAnim;
    [SerializeField] private float crossfadeDuration = 0.2f;
    
    [SerializeField] private Material skyboxBlue;
    [SerializeField] private Material skyboxRed;
    [SerializeField] private Material baseRoad;

    [SerializeField] private float vfxVitesseSpawnSpeed = 250;
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
        UpdateObtacles();
        UpdateSkybox();
        UpdateVfx();
    }

    private void UpdateObtacles()
    {
        Debug.Log(currentDimension);
        
        // if (currentDimension == Dimension.DimensionA)
        // {
        //     animPrefabObstacleBlue.Play("appear");
        //     animPrefabObstacleRed.Play("dissolve");
        // }
        // else
        // {
        //     animPrefabObstacleBlue.Play("dissolve");
        //     animPrefabObstacleRed.Play("appear");
        // }
        
        testAnim.CrossFade("dissolve", crossfadeDuration);
        
        if (currentDimension == Dimension.DimensionA)
        {
            testAnim.CrossFade("dissolve", crossfadeDuration);
        }
        else
        {
            testAnim.CrossFade("appear", crossfadeDuration);
        }
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
        vfxVitesseBlue.SetFloat("vitesse", currentDimension == Dimension.DimensionA ? vfxVitesseSpawnSpeed : 0);
        vfxVitesseRed.SetFloat("vitesse", currentDimension == Dimension.DimensionB ? vfxVitesseSpawnSpeed : 0);
    }
}
