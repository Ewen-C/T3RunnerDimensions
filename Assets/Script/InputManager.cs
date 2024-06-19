using UnityEngine;
using Lean.Touch;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public delegate void MovementHandler(float direction);
    public event MovementHandler OnMoveDirectionChanged;

    public delegate void DimensionChangeHandler();
    public event DimensionChangeHandler OnDimensionChange;
    
    public Image leftControlArea;
    public Image rightControlArea;
    public Image changeDimControlArea;
    
    private float moveDirection; // 0 = pas de mouvement, 1 = droite, -1 = gauche
    
    protected virtual void OnEnable()
    {
        LeanTouch.OnFingerDown += HandleFingerDown;
        LeanTouch.OnFingerUp += HandleFingerUp;
    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerDown -= HandleFingerDown;
        LeanTouch.OnFingerUp -= HandleFingerUp;
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        Vector2 touchPosition = finger.ScreenPosition;
        
        if (IsPointerOverUIObject(leftControlArea, touchPosition))
        {
            moveDirection = -1f; // Déplacer à gauche
        }
        else if (IsPointerOverUIObject(rightControlArea, touchPosition))
        {
            moveDirection = 1f; // Déplacer à droite
        }
        else if (IsPointerOverUIObject(changeDimControlArea, touchPosition))
        {
            OnDimensionChange?.Invoke();
        }
        
        OnMoveDirectionChanged?.Invoke(moveDirection);
    }

    private void HandleFingerUp(LeanFinger finger)
    {
        moveDirection = 0f;
        OnMoveDirectionChanged?.Invoke(moveDirection);
    }
    
    private bool IsPointerOverUIObject(Image img, Vector2 screenPosition)
    {
        RectTransform rectTransform = img.rectTransform;
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPosition, null);
    }
}