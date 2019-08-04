using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/SoundPack")]
public class SoundPack : ScriptableObject
{
    public bool random;
    public List<AudioClip> clips;

    public AudioClip GetClip()
    {
        int count = clips.Count;
        if (count > 0)
        {
            int index = 0;
            if (random)
                index = Random.Range(0, count);

            return clips[index];
        }
        else
        {
            Debug.LogError(this.name + " doesn't have any clips");
        }
        return null;
    }
}
