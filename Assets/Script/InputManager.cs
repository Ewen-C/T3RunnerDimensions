using UnityEngine;
using Lean.Touch;

public class InputManager : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public float minX = -5f;  // Limite minimale sur l'axe x
    public float maxX = 5f;   // Limite maximale sur l'axe x
    public float acceleration = 10f; // Vitesse d'accélération 
    public float deceleration = 10f; // Vitesse de décélération
    
    private float moveDirection = 0f; // 0 = pas de mouvement, 1 = droite, -1 = gauche
    private float currentSpeed = 0f;
    
    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    protected virtual void OnEnable()
    {
        LeanTouch.OnFingerDown += HandleFingerDown;
        LeanTouch.OnFingerUp += HandleFingerUp;
        LeanTouch.OnFingerUpdate += HandleFingerUpdate;
        LeanTouch.OnFingerTap += HandleFingerTap;
    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerDown -= HandleFingerDown;
        LeanTouch.OnFingerUp -= HandleFingerUp;
        LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }

    public void HandleFingerDown(LeanFinger finger)
    {
        if (finger.ScreenPosition.y < Screen.height / 4 && finger.ScreenPosition.x < Screen.width / 3)
        {
            moveDirection = -1f;
        } else if (finger.ScreenPosition.y < Screen.height / 4 && finger.ScreenPosition.x > (Screen.width / 3) * 2)
        {
            moveDirection = 1f;
        }
        
        // // Convertir la position de l'écran en position dans le monde
        // Vector3 fingerPosition = Camera.main.ScreenToWorldPoint(new Vector3(finger.ScreenPosition.x, finger.ScreenPosition.y, Camera.main.transform.position.z - rb.transform.position.z));
        //
        // // Déterminez si le toucher est à gauche ou à droite du personnage
        // moveDirection = -fingerPosition.x > rb.position.x ? 1f : -1f;
    }

    public void HandleFingerUp(LeanFinger finger)
    {
        // Arrêter le mouvement lorsque le doigt est relevé
        moveDirection = 0f;
        // Diminue la vitesse progressivement jusqu'à 0
        //currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
    }

    public void HandleFingerUpdate(LeanFinger finger)
    {
        if (moveDirection != 0f)
        {
            // Augmente la vitesse progressivement jusqu'à moveSpeed
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed * moveDirection, acceleration * Time.deltaTime);
        }
        else
        {
            // Diminue la vitesse progressivement jusqu'à 0
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        // Calculez la nouvelle position x cible avec interpolation linéaire
        float targetX = Mathf.Lerp(rb.position.x, rb.position.x + currentSpeed * Time.deltaTime, 0.5f);
        targetX = Mathf.Clamp(targetX, minX, maxX);

        Vector3 targetPosition = new Vector3(targetX, rb.position.y, rb.position.z);

        // Appliquez le mouvement
        rb.MovePosition(targetPosition);
    }
    
    public void HandleFingerTap(LeanFinger finger)
    { 
        if (finger.ScreenPosition.y < Screen.height / 4 && finger.ScreenPosition.x > (Screen.width / 3) && 
            finger.ScreenPosition.x < (Screen.width / 3) * 2)
        {
            // Double Tap
            if (finger.TapCount == 2) { if (player != null) 
                { player.GetComponent<PlayerCharacter>().SwitchDimension(); }
            }
        }
    }
}