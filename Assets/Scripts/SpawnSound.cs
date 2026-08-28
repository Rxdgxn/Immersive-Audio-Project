using UnityEngine;
using FMODUnity;

public class SpawnSound : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter emitter;

    public void PlaySound()
    {
        emitter.Play();
    }
}
