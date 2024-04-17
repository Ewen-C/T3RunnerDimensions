using UnityEngine;
using Lean.Touch;

public class InputManager : MonoBehaviour
{
    public GameObject player;
    
    public Rigidbody rb;
    public float smoothSpeed = 5f;
    
    // Définissez les numéros de layer pour chaque dimension
    public int dimensionALayer;
    public int dimensionBLayer;
    
    private bool isDragging = false;
    private Vector3 targetPosition;
    
    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    protected virtual void OnEnable()
    {
        // Hook into the events we need
        LeanTouch.OnFingerDown += HandleFingerDown;
        LeanTouch.OnFingerUpdate += HandleFingerSet;
        LeanTouch.OnFingerUp += HandleFingerUp;
        LeanTouch.OnFingerTap += HandleFingerTap;
    }

    protected virtual void OnDisable()
    {
        // Unhook the events
        LeanTouch.OnFingerDown -= HandleFingerDown;
        LeanTouch.OnFingerUpdate -= HandleFingerSet;
        LeanTouch.OnFingerUp -= HandleFingerUp;
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }

    public void HandleFingerDown(LeanFinger finger)
    {
        isDragging = true;
    }

    public void HandleFingerSet(LeanFinger finger)
    {
        if (isDragging)
        {
            // Convert the finger's position to world space
            Vector3 fingerWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(finger.ScreenPosition.x, 0, Camera.main.transform.position.z - rb.transform.position.z));

            // Keep the y and z positions the same as the character
            fingerWorldPosition.y = rb.position.y;
            fingerWorldPosition.z = rb.position.z;

            // Interpolate the position for smooth movement
            targetPosition = new Vector3(-fingerWorldPosition.x, rb.position.y, rb.position.z);
            rb.MovePosition(Vector3.Lerp(rb.position, targetPosition, smoothSpeed * Time.deltaTime));
        }
    }

    public void HandleFingerUp(LeanFinger finger)
    {
        isDragging = false;
    }
    
    public void HandleFingerTap(LeanFinger finger)
    {
        if (player != null) 
        {
            player.GetComponent<PlayerCharacter>().SwitchDimension();
        }
    }
}