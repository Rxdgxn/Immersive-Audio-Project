using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private EventReference testEvent;
    
    void Start()
    {
        var inst = RuntimeManager.CreateInstance(testEvent);
        inst.set3DAttributes(transform.To3DAttributes());
        inst.start();
        inst.release();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
