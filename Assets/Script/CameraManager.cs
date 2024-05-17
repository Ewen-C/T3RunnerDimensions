using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public PlayerController playerController; // Référence au script PlayerController
    public float shiftAmount = 0.5f; // Quantité de décalage latéral de la caméra
    public float shiftSpeed = 2f; // Vitesse de décalage de la caméra

    private Vector3 originalPosition;
    private float targetXPosition;

    void Start()
    {
        originalPosition = transform.position; // Sauvegarde la position originale de la caméra
    }

    void Update()
    {
        if (playerController != null)
        {
            // Calcule la position cible de la caméra basée sur la direction du mouvement du joueur
            targetXPosition = originalPosition.x + (playerController.GetCurrentSpeed() / playerController.maxSpeed) * shiftAmount;
            
            // Lisse le mouvement de la caméra vers la position cible
            Vector3 currentPosition = transform.position;
            currentPosition.x = Mathf.Lerp(currentPosition.x, targetXPosition, Time.deltaTime * shiftSpeed);
            transform.position = currentPosition;
        }
    }

    public void ResetCameraPosition()
    {
        // Appelé pour réinitialiser la position de la caméra, si nécessaire
        transform.position = originalPosition;
    }
}