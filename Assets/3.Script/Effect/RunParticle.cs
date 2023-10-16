using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParticle : MonoBehaviour
{
    private bool spawnParticle = false;
    private ParticleSystem ps;
    private ParticleSystemRenderer psr;

    [Header("Materials")]
    [SerializeField] Material m_run;
    [SerializeField] Material m_roll;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        psr = GetComponent<ParticleSystemRenderer>();
    }

    public void Particle_On(int direction, bool roll)
    {
        if (!spawnParticle)
        {
            spawnParticle = true;

            if (direction == 0)
            {
                psr.flip = new Vector3(1, 0, 0);
            }
            else if (direction == 1)
            {
                psr.flip = new Vector3(0, 0, 0);
            }

            if (roll)
            {
                psr.material = m_roll;
            }
            else
            {
                psr.material = m_run;
            }

            ps.Play();
        }
    }

    public void Particle_Off()
    {
        if (spawnParticle)
        {
            spawnParticle = false;
            ps.Stop();
        }
    }
}
