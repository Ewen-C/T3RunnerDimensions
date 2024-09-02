using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header ("Music")]
    [field: SerializeField] public EventReference music {get; private set;}

    [field: Header ("SFX")]
    [field: SerializeField] public EventReference deathSound {get; private set;}
    [field: SerializeField] public EventReference collectSound {get; private set;}
    [field: SerializeField] public EventReference switchSound {get; private set;}
    [field: SerializeField] public EventReference selectSound {get; private set;}

    public static FMODEvents instance {get; private set;}
    // Start is called before the first frame update
    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogError("Plus d'un FMOD events dans la scène");
        }
        instance = this;
    }
}
