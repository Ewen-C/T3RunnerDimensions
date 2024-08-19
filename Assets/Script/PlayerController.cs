using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 0.1f; // Accélération ajoutée à chaque frame lors du mouvement
    public float maxSpeed = 5f; // Vitesse maximale
    public float smoothTime = 0.3f; // Temps pour atteindre la vitesse maximale
    public float minX = -5f;  // Limite minimale sur l axe x
    public float maxX = 5f;   // Limite maximale sur l axe x
    public float maxLeanAngle = 30f; // Angle maximal inclinaison
    private bool gameOver = false;
    
    [SerializeField] private GameObject player;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private AnimationCurve curve;

    private Rigidbody rb;
    private PlayerCharacter character;
    private float currentSpeed; // Vitesse actuelle du personnage
    private float targetSpeed; // Vitesse cible basée sur l entrée du joueur
    private float velocityX; // Utilisé pour le smoothing de la vitesse
    
    [SerializeField] private DimensionManager dimensionManager;
    
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        character = player.GetComponent<PlayerCharacter>();
        inputManager.OnMoveDirectionChanged += HandleMoveDirectionChanged;
        inputManager.OnDimensionChange += HandleDimensionChange;
    }

    public void UpdateGameOver(bool newGameOver)
    {
        gameOver = newGameOver;

        if (gameOver)
        {
            inputManager.OnMoveDirectionChanged -= HandleMoveDirectionChanged;
            inputManager.OnDimensionChange -= HandleDimensionChange;
        }
    }
    
    private void HandleMoveDirectionChanged(float direction)
    {
        targetSpeed = direction * maxSpeed;
    }
    
    private void HandleDimensionChange()
    {
        dimensionManager.SwitchDimension();
    }
    
    private void Update()
    {
        //Debug
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetSpeed = -maxSpeed;
        }
        else  if (Input.GetKey(KeyCode.RightArrow))
        {
            targetSpeed = maxSpeed;
        }
        
        
        
        // Transition en douceur de la vitesse actuelle vers la vitesse cible
        currentSpeed = targetSpeed != 0f ? Mathf.SmoothDamp(currentSpeed, targetSpeed, ref velocityX, smoothTime) :
            // Ralentissement progressif si aucune entrée n est détectée
            Mathf.Lerp(currentSpeed, 0, Time.deltaTime * acceleration);
        
        // Utilisation de Translate pour le mouvement latéral
        Vector3 movement = new Vector3(currentSpeed * Time.deltaTime, 0, 0);
        Transform rbTransform;
        (rbTransform = rb.transform).Translate(movement, Space.World);

        // Maintien de la position X dans les limites
        Vector3 position = rbTransform.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        rb.transform.position = position;
        
        LeanController();
    }
    
    private void LeanController()       // Application de la rotation pour l inclinaison
    {
        float lean = curve.Evaluate(currentSpeed / maxSpeed) * maxLeanAngle;
        rb.transform.rotation = Quaternion.Euler(0, 0, -lean);
    }
}
