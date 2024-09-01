using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    public static GaugeManager Instance { get; private set; }
    public Slider gaugeSlider;
    public Image gaugeFillImage;
    
    public int pointsPerCollectableWhite = 7;
    public int pointsPerCollectableYellow = 5;
    public float decreaseRateYellow = 9f;
    
    private const int maxWhitePoints = 99;
    private bool isYellowPhase = false;
    
    private Color whitePhaseColor = Color.white;
    private Color yellowPhaseColor = Color.yellow;
    
    public bool IsYellowPhase => isYellowPhase;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isYellowPhase)
        {
            // Diminue les points continuellement en phase jaune
            gaugeSlider.value -= decreaseRateYellow * Time.deltaTime;
            if (gaugeSlider.value <= 0)
            {
                SwitchToWhitePhase();
            }
        }
    }

    public void AddPoints(int value)
    {
        if (!isYellowPhase)
        {
            // Ajoute des points en phase blanche
            gaugeSlider.value += value;
            if (gaugeSlider.value >= maxWhitePoints)
            {
                SwitchToYellowPhase();
            }
        }
        else
        {
            // Ajoute moins de points en phase jaune
            gaugeSlider.value += value;
            if (gaugeSlider.value > maxWhitePoints)
            {
                gaugeSlider.value = maxWhitePoints;
            }
        }
    }
    
    private void SwitchToYellowPhase()
    {
        isYellowPhase = true;
        gaugeFillImage.color = yellowPhaseColor;
    }

    private void SwitchToWhitePhase()
    {
        isYellowPhase = false;
        gaugeSlider.value = 0;
        gaugeFillImage.color = whitePhaseColor;
    }
}