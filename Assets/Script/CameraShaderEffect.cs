using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShaderEffect : MonoBehaviour
{
    public Material effectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (effectMaterial != null)
            Graphics.Blit(src, dest, effectMaterial);
        else
            Graphics.Blit(src, dest);
    }
}