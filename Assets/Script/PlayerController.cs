using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 0.1f; // Accélération ajoutée à chaque frame lors du mouvement
    public float maxSpeed = 5f; // Vitesse maximale
    public float smoothTime = 0.3f; // Temps pour atteindre la vitesse maximale
    public float minX = -5f;  // Limite minimale sur l'axe x
    public float maxX = 5f;   // Limite maximale sur l'axe x
    public float maxLeanAngle = 30f; // Angle maximal de penchement
    
    [SerializeField] private GameObject player;
    [SerializeField] private InputManager inputManager;

    private Rigidbody rb;
    private float currentSpeed = 0f; // Vitesse actuelle du personnage
    private float targetSpeed = 0f; // Vitesse cible basée sur l'entrée du joueur
    private float velocityX = 0f; // Utilisé pour le smoothing de la vitesse


    [SerializeField] private AnimationCurve curve;
    
    
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        inputManager.OnMoveDirectionChanged += HandleMoveDirectionChanged;
        inputManager.OnDoubleTap += HandleDoubleTap;
    }
    
    private void HandleMoveDirectionChanged(float direction)
    {
        targetSpeed  = direction * maxSpeed;
    }
    
    private void HandleDoubleTap()
    {
        player.GetComponent<PlayerCharacter>().SwitchDimension();
    }
    
    private void Update()
    {
        if (targetSpeed != 0f)
        {
            // Smoothly transition the current speed towards the target speed
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref velocityX, smoothTime);
        }
        else
        {
            // Gradually slow down if no input is detected
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * acceleration);
        }
        
        // Vector3 targetPosition = rb.position + new Vector3(currentSpeed, 0, 0) * Time.deltaTime;
        // targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        // rb.MovePosition(targetPosition);
        //
        // float lean = (currentSpeed / maxSpeed) * maxLeanAngle;
        // player.transform.rotation = Quaternion.Euler(0, 0, -lean);
        
        // Utilisation de Translate pour le mouvement latéral
        Vector3 movement = new Vector3(currentSpeed * Time.deltaTime, 0, 0);
        rb.transform.Translate(movement, Space.World);

        // Maintien de la position X dans les limites
        Vector3 position = rb.transform.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        rb.transform.position = position;
        
        LeanController();
    }
    
    private void LeanController()       // Application de la rotation pour le penchement
    {
        // float lean = (currentSpeed / maxSpeed) * maxLeanAngle;
        float lean = curve.Evaluate(currentSpeed / maxSpeed) * maxLeanAngle;
        rb.transform.rotation = Quaternion.Euler(0, 0, -lean);
    }

    private void OnDestroy()
    {
        if (inputManager != null)
        {
            inputManager.OnMoveDirectionChanged -= HandleMoveDirectionChanged;
            inputManager.OnDoubleTap -= HandleDoubleTap;
        }
    }
}
