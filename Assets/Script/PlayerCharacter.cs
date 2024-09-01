using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int dimensionALayer;
    [SerializeField] private int dimensionBLayer;

    [SerializeField] private Material playerMaterial;
    [SerializeField] private Color playerColorBlue;
    [SerializeField] private Color playerColorRed;
    [SerializeField] private float playerIntensityFactor = 25f;

    [SerializeField] private Material playerBoostMaterial;
    [SerializeField] private Color playerBoostColorBlue;
    [SerializeField] private Color playerBoostColorRed;
    [SerializeField] private float playerBoostIntensityFactor = 8f;

    [SerializeField] private bool debugMode;

    [SerializeField] private Animator animatorMesh;
    [SerializeField] private Animator animatorOutline;
    [SerializeField] private float crossfadeDuration;


    private void OnTriggerEnter(Collider other)
    {
        if (debugMode) return;
        // Debug.Log("Collide (debugMode) " + debugMode + " : " + other.tag);

        if (other.CompareTag("ObstacleDimension") || other.CompareTag("ObstacleFixe"))
        {
            // Debug.Log("HIT " + other.tag);
            PlayerManager.gameOver = true;
            GetComponentInParent<PlayerController>().UpdateGameOver(true);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.deathSound, transform.position);
        }
    }

    public void UpdateDimension(DimensionManager.Dimension newDimension)
    {
        gameObject.layer = (newDimension == DimensionManager.Dimension.DimensionA) ? dimensionALayer : dimensionBLayer;

        if (newDimension == DimensionManager.Dimension.DimensionA)
        {
            playerMaterial.SetColor("_EmissionColor", playerColorBlue * playerIntensityFactor);
            playerBoostMaterial.color = playerBoostColorBlue * playerBoostIntensityFactor;
        }
        else
        {
            playerMaterial.SetColor("_EmissionColor", playerColorRed * playerIntensityFactor);
            playerBoostMaterial.color = playerBoostColorRed * playerBoostIntensityFactor;
        }
    }

    public void PlayBoostAnimations(bool isAppearing)
    {
        Debug.Log(isAppearing);
        animatorMesh.CrossFade(isAppearing ? "up" : "down", crossfadeDuration);
        animatorOutline.CrossFade(isAppearing ? "up_effect" : "down_effect", crossfadeDuration);
    }
}