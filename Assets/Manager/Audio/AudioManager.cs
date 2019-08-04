using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffectType
{
    Postive,
    Negative,
    Status,
    Summon,
    Ability,
    Cancel,
    Click,
    Destroy,
}

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource m_BGM;
    public AudioSource m_SFX;

    public AudioCollection allClips;

    private bool lastUpdateClick = false;
    private bool click = false;

    private void Update()
    {
        lastUpdateClick = click;
        click = Input.GetMouseButtonDown(0);
        if (click && !lastUpdateClick)
        {
            Play(SoundEffectType.Click);
        }
    }

    public void Play(SoundEffectType type)
    {
        m_SFX.clip = allClips.GetAudioClip(type);
        m_SFX.Play();
    }

}
