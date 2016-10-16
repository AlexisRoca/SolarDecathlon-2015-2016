using UnityEngine;
using System.Collections;

public class EnvironmentalSound : MonoBehaviour {

    private AudioSource source;
    private EnvironmentalSoundTrigger currentSound = null;

    private int fadingTime = 1;
    private int nbIncrement = 10;

    void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        source.loop = true;
    }

    IEnumerator FadeOut()
    {
        float delta = (1 / (float)nbIncrement) * fadingTime;
        for (int i = 0; i < nbIncrement; i++)
        {
            source.volume -= delta;
            yield return new WaitForSeconds(delta);
        }
        yield return new WaitForSeconds(fadingTime);
    }

    IEnumerator FadeIn()
    {
        float delta = (1 / (float)nbIncrement) * fadingTime;
        Debug.Log(delta);
        for (int i = 0; i < nbIncrement; i++)
        {
            source.volume += delta;
            yield return new WaitForSeconds(delta);
        }
    }

    public void PlaySound(EnvironmentalSoundTrigger clip)
    {
        StartCoroutine(PlaySoundCo(clip));
    }

    IEnumerator PlaySoundCo(EnvironmentalSoundTrigger clip)
    {
        if (clip != currentSound)
        {
            if (source.isPlaying)
            {
                StartCoroutine(FadeOut());
                yield return new WaitForSeconds(fadingTime);
            }

            currentSound = clip;

            source.clip = clip.clip;
            source.volume = 0;
            source.Play();
            StartCoroutine(FadeIn());
        }
        
    }
}
