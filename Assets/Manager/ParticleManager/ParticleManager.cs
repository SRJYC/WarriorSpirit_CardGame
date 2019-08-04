using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Other/ParticleManager")]
public class ParticleManager : SingletonScriptableObject<ParticleManager>
{
    public enum ParticleType
    {
        destroy,
        applyStatus,
        hit,
    }
    public ParticleType reference;
    public List<GameObject> particlePrefab;

    public void PlayEffect(ParticleType type, GameObject target)
    {
        GameObject particle = Instantiate(particlePrefab[(int)type]);
        particle.transform.position = target.transform.position;
    }
}
