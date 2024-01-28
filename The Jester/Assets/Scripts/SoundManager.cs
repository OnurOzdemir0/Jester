using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    [SerializeField] private AudioClip[] toparlicamClips;
    [SerializeField] private AudioClip kusuraBakmayinClip; // Play when one health is left
    [SerializeField] private AudioClip[] kingLaughClips;
    [SerializeField] private AudioClip[] kingMadClips;

    [SerializeField] private AudioClip kingLaughDeathClip;
    [SerializeField] private AudioClip cardPullClip;
    [SerializeField] private AudioClip clockTick;

    private AudioSource audioSource;

    void Awake()
    {
        if (soundManager != null && soundManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            soundManager = this;
            DontDestroyOnLoad(this.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayRandomToparlicamSound()
    {
        PlayRandomSound(toparlicamClips);
    }

    public void PlayKusuraBakmayinSound()
    {
        audioSource.PlayOneShot(kusuraBakmayinClip);
    }

    public void PlayRandomKingLaughSound()
    {
        PlayRandomSound(kingLaughClips);
    }

    public void PlayRandomKingMadSound()
    {
        PlayRandomSound(kingMadClips);
    }

    public void PlayKingLaughDeathSound()
    {
        audioSource.PlayOneShot(kingLaughDeathClip);
    }

    public void PlayCardPullSound()
    {
        audioSource.PlayOneShot(cardPullClip);
    }
    public void PlayClockTick()
    {
        audioSource.PlayOneShot(clockTick);
    }

    

    private void PlayRandomSound(AudioClip[] clips)
    {
        if (clips.Length == 0) return;

        int index = Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[index]);
    }
}
