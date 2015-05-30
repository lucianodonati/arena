using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(string _sound)
    {
        audioSource.clip = getRandomClip(_sound);
        if (audioSource.clip == null)
            Debug.LogError("Trying to play: " + _sound + " ,but is not assigned.");
        else
            audioSource.Play();
    }

    private AudioClip getRandomClip(string _sound)
    {
        AudioClip theClip = null;
        foreach (Sound theSound in sounds)
        {
            if (theSound.name.Equals(_sound))
                theClip = theSound.clips[Random.Range(0, theSound.clips.Count)];
        }
        return theClip;
    }
}