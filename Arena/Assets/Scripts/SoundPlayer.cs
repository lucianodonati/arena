using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [System.Serializable]
    public struct Sound
    {
        public string name;
        public List<AudioClip> clips;
    }

    private AudioSource audioSource;

    public List<Sound> sounds;

    // Use this for initialization
    private void Start()
    {
        //audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySound(string _sound)
    {
        AudioClip theClip = getRandomClip(_sound);
        if (theClip != null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = theClip;
            if (audioSource.clip == null)
                Debug.LogError("Trying to play: " + _sound + " ,but is not assigned.");
            else
                audioSource.Play();
        }
    }

    private AudioClip getRandomClip(string _sound)
    {
        AudioClip theClip = null;
        foreach (Sound theSound in sounds)
        {
            if (theSound.name.Equals(_sound))
                theClip = theSound.clips[Random.Range(0, theSound.clips.Count - 1)];
        }
        return theClip;
    }
}