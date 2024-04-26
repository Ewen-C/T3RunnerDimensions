using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minX = -5f;  // Limite minimale sur l'axe x
    public float maxX = 5f;   // Limite maximale sur l'axe x
    public float acceleration = 10f; // Vitesse d'accélération 
    public float deceleration = 10f; // Vitesse de décélération
    
    [SerializeField] private GameObject player;
    [SerializeField] private InputManager inputManager;

    private Rigidbody rb;
    private float currentSpeed;
    private float moveDirection;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        inputManager.OnMoveDirectionChanged += HandleMoveDirectionChanged;
        inputManager.OnDoubleTap += HandleDoubleTap;
    }
    
    private void HandleMoveDirectionChanged(float direction)
    {
        moveDirection = direction;
    }
    
    private void HandleDoubleTap()
    {
        player.GetComponent<PlayerCharacter>().SwitchDimension();
    }
    
    private void Update()
    {
        if (moveDirection != 0f)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed * moveDirection, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        float targetX = Mathf.Lerp(rb.position.x, rb.position.x + currentSpeed * Time.deltaTime, 0.5f);
        targetX = Mathf.Clamp(targetX, minX, maxX);
        Vector3 targetPosition = new Vector3(targetX, rb.position.y, rb.position.z);
        rb.MovePosition(targetPosition);
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
