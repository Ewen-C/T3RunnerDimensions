using UnityEngine;
using Lean.Touch;

public class InputManager : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public float minX = -5f;  // Limite minimale sur l'axe x
    public float maxX = 5f;   // Limite maximale sur l'axe x
    private float moveDirection = 0f; // 0 = pas de mouvement, 1 = droite, -1 = gauche
    
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
        // Assurez-vous que le toucher est dans la partie inférieure de l'écran
        if (finger.ScreenPosition.y < Screen.height / 4)
        {
            // Déterminez si le toucher est à gauche ou à droite de l'écran
            moveDirection = finger.ScreenPosition.x > Screen.width / 2 ? 1f : -1f;
        }
    }

    public void HandleFingerUp(LeanFinger finger)
    {
        // Arrêter le mouvement lorsque le doigt est relevé
        moveDirection = 0f;
    }

    public void HandleFingerUpdate(LeanFinger finger)
    {
        if (moveDirection != 0f)
        {
            // Calculez la nouvelle position x cible
            Vector3 targetPosition = rb.position + new Vector3(moveDirection, 0, 0) * moveSpeed * Time.deltaTime;

            // Maintenez la position y et z inchangées
            targetPosition.y = rb.position.y;
            targetPosition.z = rb.position.z;
            
            // Utilisez Mathf.Clamp pour garder la position x dans les limites définies
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

            // Appliquez le mouvement
            rb.MovePosition(targetPosition); ;
        }
    }
    
    public void HandleFingerTap(LeanFinger finger)
    {
        // Assurez-vous que le toucher est dans la partie inférieure de l'écran
        if (finger.ScreenPosition.y > Screen.height / 4)
        {
            // Double Tap
            if (finger.TapCount == 2)
            {
                if (player != null) 
                {
                    player.GetComponent<PlayerCharacter>().SwitchDimension();
                }
            }
        }
    }
}