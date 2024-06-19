using UnityEngine;
using Lean.Touch;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public delegate void MovementHandler(float direction);
    public event MovementHandler OnMoveDirectionChanged;

    public delegate void DimensionChangeHandler();
    public event DimensionChangeHandler OnDimensionChange;
    
    //public Image leftControlArea;
    //public Image rightControlArea;
    public Image changeDimControlArea;
    
    public float sensitivity = 0.1f;  // Ajustez ce paramètre pour calibrer la réactivité de l'inclinaison
    public float deadZone = 5f;  // Inclinaison minimale nécessaire pour commencer le mouvement
    
    private float moveDirection; // 0 = pas de mouvement, 1 = droite, -1 = gauche
    
    protected virtual void OnEnable()
    {
        LeanTouch.OnFingerDown += HandleFingerDown;
        //LeanTouch.OnFingerUp += HandleFingerUp;
        //LeanTouch.OnFingerSwipe += HandleFingerSwipe;
    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerDown -= HandleFingerDown;
        //LeanTouch.OnFingerUp -= HandleFingerUp;
        //LeanTouch.OnFingerSwipe -= HandleFingerSwipe;
    }
    
    void Start()
    {
        Input.gyro.enabled = true;
    }
    
    void Update()
    {
        float tilt = Input.gyro.attitude.eulerAngles.z;
        
        Debug.Log("Angle Inclinaison : " + tilt);
        
        // Ajuster l'angle pour centrer autour de 90 degrés
        tilt -= 90;  // Centrer autour de 0° avec 90° comme point neutre
        if (tilt < -180) tilt += 360;  // Corriger la discontinuité à -180/180

        // Appliquer deadzone et sensibilité
        if (Mathf.Abs(tilt) < deadZone) {
            moveDirection = 0;
        } else {
            // Calculer la direction et la sensibilité
            moveDirection = Mathf.Sign(tilt) * Mathf.Clamp01((Mathf.Abs(tilt) - deadZone) * sensitivity);
        }

        OnMoveDirectionChanged?.Invoke(-moveDirection);
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        // Vector2 touchPosition = finger.ScreenPosition;
        //
        // if (IsPointerOverUIObject(leftControlArea, touchPosition))
        // {
        //     moveDirection = -1f; // Déplacer à gauche
        // }
        // else if (IsPointerOverUIObject(rightControlArea, touchPosition))
        // {
        //     moveDirection = 1f; // Déplacer à droite
        // }
        //
        // OnMoveDirectionChanged?.Invoke(moveDirection);
        
        Vector2 touchPosition = finger.ScreenPosition;
        
        if (IsPointerOverUIObject(changeDimControlArea, touchPosition))
        {
            OnDimensionChange?.Invoke();
        }
    }

    // private void HandleFingerUp(LeanFinger finger)
    // {
    //     moveDirection = 0f;
    //     OnMoveDirectionChanged?.Invoke(moveDirection);
    // }
    
    // private void HandleFingerSwipe(LeanFinger finger)
    // {
    //     // Detect vertical swipe up
    //     if (finger.SwipeScreenDelta.y > Mathf.Abs(finger.SwipeScreenDelta.x) && finger.SwipeScreenDelta.y > 50)
    //     {
    //         OnDimensionChange?.Invoke();
    //     }
    // }
    
    private bool IsPointerOverUIObject(Image img, Vector2 screenPosition)
    {
        RectTransform rectTransform = img.rectTransform;
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPosition, null);
    }
}