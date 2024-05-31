using UnityEngine;
using Lean.Touch;

public class InputManager : MonoBehaviour
{
    public delegate void MovementHandler(float direction);
    public event MovementHandler OnMoveDirectionChanged;

    public delegate void DoubleTapHandler();
    public event DoubleTapHandler OnDoubleTap;
    
    
    private float moveDirection; // 0 = pas de mouvement, 1 = droite, -1 = gauche
    
    protected virtual void OnEnable()
    {
        LeanTouch.OnFingerDown += HandleFingerDown;
        LeanTouch.OnFingerUp += HandleFingerUp;
        LeanTouch.OnFingerTap += HandleFingerTap;
    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerDown -= HandleFingerDown;
        LeanTouch.OnFingerUp -= HandleFingerUp;
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        if (finger.ScreenPosition.y < Screen.height / 4.0f && finger.ScreenPosition.x < Screen.width / 3.0f)
        {
            moveDirection = -1f;
        } else if (finger.ScreenPosition.y < Screen.height / 4.0f && finger.ScreenPosition.x > (Screen.width / 3.0f) * 2)
        {
            moveDirection = 1f;
        }
        
        OnMoveDirectionChanged?.Invoke(moveDirection);
    }

    private void HandleFingerUp(LeanFinger finger)
    {
        moveDirection = 0f;
        OnMoveDirectionChanged?.Invoke(moveDirection);
    }

    private void HandleFingerTap(LeanFinger finger)
    { 
        if (finger.ScreenPosition.y < Screen.height / 4.0f && finger.ScreenPosition.x > (Screen.width / 3.0f) && 
            finger.ScreenPosition.x < (Screen.width / 3.0f) * 2)
        {
            if (finger.TapCount == 2)
            {
                OnDoubleTap?.Invoke();
            }
        }
    }
}