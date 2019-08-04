using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/Collection")]
public class AudioCollection : ScriptableObject
{
    public SoundEffectType reference;
    public List<SoundPack> audios;

    public AudioClip GetAudioClip(SoundEffectType type)
    {
        return audios[(int)type].GetClip();
    }
}
