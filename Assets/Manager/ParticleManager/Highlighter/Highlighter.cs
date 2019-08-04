using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{

    public ParticleSystem m_Particle;

    public void Highlight(GameObject target)
    {
        gameObject.SetActive(true);

        transform.SetParent(target.transform);
        transform.position = target.transform.position;


        m_Particle.Play();
    }

    public void Stop()
    {
        gameObject.SetActive(false);

        transform.SetParent(null);

        m_Particle.Stop();
    }
}
