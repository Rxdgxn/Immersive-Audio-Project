using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private StudioEventEmitter emitter;
    
    void Start()
    {
        emitter.Play();
    }

    void Update()
    {
        
    }
}
