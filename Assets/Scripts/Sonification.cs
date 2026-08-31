using UnityEngine;
using FMODUnity;

public class Sonification : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter emitter;

    public void Play()
    {
        emitter.Play();
    }

    public void Stop()
    {
        emitter.Stop();
    }

    public void UpdateParams(float azimuth, float elevation)
    {
        emitter.SetParameter("AzimuthError", azimuth);
        emitter.SetParameter("ElevationError", elevation);
    }
}
