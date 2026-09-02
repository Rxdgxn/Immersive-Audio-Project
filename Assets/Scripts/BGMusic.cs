using UnityEngine;
using FMODUnity;

public class BGMusic : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter emitter;
    void Start()
    {
        emitter.Play();
    }

}
