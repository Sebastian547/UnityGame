using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrap : Trap
{

    ParticleSystem particleSystemIn;

    bool working = false;
    bool isTimer = false;
    void Start()
    {
        particleSystemIn = GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        if (!isTimer)
        {
            StartCoroutine(ParticleAnimatior());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && working == true)
        {
            collision.GetComponent<IActor>().TakeDamage(Damage);
        }
    }



    private IEnumerator ParticleAnimatior()
    {

        isTimer = true;

        yield return new WaitForSeconds(Cooldown);


        if (particleSystemIn != null)
        {
            if (working)
            {
                particleSystemIn.Stop();
            }
            else
            {
                particleSystemIn.Play();
            }
            working = !working;
        }

        isTimer = false;
    }


}