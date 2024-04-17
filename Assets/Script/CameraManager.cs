using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform playerTransform; // Référence au transform du joueur
    public float smoothSpeed = 0.125f; // Vitesse de suivi de la caméra
    public float minX, maxX; // Limites horizontales de déplacement de la caméra
    
    void LateUpdate()
    {
        // Obtenir la position actuelle de la caméra
        Vector3 currentPosition = transform.position;

        if (playerTransform.position.x > minX && playerTransform.position.x < maxX)
        {
            // Calculer la nouvelle position cible de la caméra centrée sur le joueur
            Vector3 targetPosition = new Vector3(playerTransform.position.x, currentPosition.y, currentPosition.z);

            // Déplacer la caméra de manière fluide vers la nouvelle position cible
            Vector3 smoothedPosition = Vector3.Lerp(currentPosition, targetPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
