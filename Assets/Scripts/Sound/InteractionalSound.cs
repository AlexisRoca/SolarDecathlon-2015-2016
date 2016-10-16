using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class InteractionalSound : MonoBehaviour {

    public List<AudioClip> clips;

    private AudioSource source;
    private int currentClipIndex = 0;

    void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    protected void OnMVRWandButtonPressed(VRSelection iSelection)
    {
        source.clip = clips[currentClipIndex];
        source.Play();
        SwitchClip();
    }

     void SwitchClip()
    {
        currentClipIndex++;
        if(currentClipIndex >= clips.Count)
        {
            currentClipIndex = 0;
        }
    }
}
