#region File Header

/*******************************************************************************
 * Author: Matthew "Riktor" Baker
 * Filename: SoundManager.cs
 * Date Created: 4/14/2015 8:28PM EST
 * 
 * Description: A simple sound manager single for playing one shot sounds.
 * 
 * Changelog:   - Modified: Matthew "Riktor" Baker - 4/16/2015 9:01 PM - Added Comments
 *******************************************************************************/

#endregion

#region Using Directives

using UnityEngine;

#endregion

public class SoundManager : MonoBehaviour
{
    #region Public Enumeration

    /// <summary>
    /// Represents all of the available sounds to play.
    /// </summary>
    public enum SoundClip
    {
        None,
        WelcomeToSummonersRift,
        EnemySlain,
        AllySlain,
        Ace,
        Victory,
        Defeat,
        exitchampionselect,
        TargetPing,
        TauntSound,
        PlayerTurn,
        FailClick,
    }

    #endregion

    #region Private Variables

    #region SerializeFields

    /// <summary>
    /// The Primary Audio Source
    /// </summary>
    [SerializeField] private AudioSource audioSource = null;
    
    /// <summary>
    /// A Special Audio source that is designed to not be interrupted.
    /// </summary>
    [SerializeField] private AudioSource priorityAudioSource = null;

    #endregion
    
    /// <summary>
    /// A record of the last sound played if we need to play a sound in an update loop, we can keep it from repeating over and over.
    /// </summary>
    private SoundClip lastSoundPlayed = SoundClip.None;

    /// <summary>
    /// The instance of the sound manager
    /// </summary>
    private static SoundManager instance;
    
    #endregion

    #region Native Unity Functionality

    /// <summary>
    /// Played before the first frame, we use it to assign our instance and setup our GameObject singleton.
    /// </summary>
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Used to retrieve the singleton instance of the SoundManager
    /// </summary>
    /// <returns> the instance of the SoundManager </returns>
    public static SoundManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("SoundManager is null... it is missing from the heirarchy most likely.");
        }
        return instance;
    }

    /// <summary>
    /// Plays a sound with priority.
    /// </summary>
    /// <param name="soundClip"> chosen from our dfefault list of SoundClips. </param>
    public void PlayPrioritySound(SoundClip soundClip)
    {
        priorityAudioSource.Stop();
        priorityAudioSource.clip = Resources.Load<AudioClip>("Sound/" + soundClip.ToString());
        priorityAudioSource.Play();
    }

    /// <summary>
    /// Plays a sound with priority.
    /// </summary>
    /// <param name="soundClip"> The AudioClip you wish to play. </param>
    public void PlayPrioritySound(UnityEngine.AudioClip soundClip)
    {
        if (soundClip != null)
        {
            priorityAudioSource.Stop();
            priorityAudioSource.clip = soundClip;
            priorityAudioSource.Play();
        }
    }

    /// <summary>
    /// Plays a sound.
    /// </summary>
    /// <param name="soundClip"> chosen from our dfefault list of SoundClips. </param>
    public void PlaySound(SoundClip soundClip)
    {
        audioSource.Stop();
        audioSource.clip = null;

        audioSource.clip = Resources.Load<AudioClip>("Sound/" + soundClip.ToString());
        audioSource.Play();
    }

    /// <summary>
    /// Plays a sound one time.
    /// </summary>
    /// <param name="soundClip"> chosen from our dfefault list of SoundClips. </param>
    public void PlaySoundOnce(SoundClip soundClip)
    {
        if (lastSoundPlayed != soundClip)
        {
            audioSource.clip = Resources.Load<AudioClip>("Sound/" + soundClip.ToString());
            audioSource.Play();
            lastSoundPlayed = soundClip;
        }       
    }

    /// <summary>
    /// Plays a sound.
    /// </summary>
    /// <param name="soundClip"> The AudioClip you wish to play. </param>
    public void PlaySound(UnityEngine.AudioClip soundClip)
    {
        if (soundClip != null )
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }

    /// <summary>
    /// Gets the name of the last played sound
    /// </summary>
    /// <returns>the name of teh last played sound</returns>
    public string LastSoundPlayed()
    {
        return lastSoundPlayed.ToString();
    }

    /// <summary>
    /// Gets the current sound clips name.
    /// </summary>
    /// <returns> the current sound clips name </returns>
    public string CurrentClipName()
    {
        return audioSource.clip.name;
    }

    /// <summary>
    /// Checks if a clip is playing through the normal audio source.
    /// </summary>
    /// <returns> true if clip is playing, otherwise false. </returns>
    public bool IsClipPlaying()
    {
        return audioSource.isPlaying;
    }

    /// <summary>
    /// Check if a clip is playig through the priority audio source.
    /// </summary>
    /// <returns> true if clip is playing, otherwise false. </returns>
    public bool IsPriorityClipPlaying()
    {
        return priorityAudioSource.isPlaying;
    }

    #endregion
}