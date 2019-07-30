using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Other/ParticleManager")]
public class ParticleManager : SingletonScriptableObject<ParticleManager>
{
    public enum ParticleType
    {
        burst,
    }
    public List<GameObject> particlePrefab;

    public void PlayEffect(ParticleType type, GameObject target)
    {
        GameObject particle = Instantiate(particlePrefab[(int)type]);
        particle.transform.position = target.transform.position;

        CoroutineManager.Instance.StartCoroutine(Play(particle));
    }

    IEnumerator Play(GameObject particle)
    {
        ParticleSystem system = particle.GetComponentInChildren<ParticleSystem>();

        while(!system.isStopped)
        {
            yield return new WaitForSeconds(1.0f);
        }

        Destroy(particle);
    }
}
