using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField]
    GameObject arrow;

    Rigidbody2D rb;
    bool coroutineRunning = false;
    BoxCollider2D bc2d;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        bc2d.enabled = false;
        InitiateDifficulty(); 
    }
    public override void Atakuj()
    {
        if (!coroutineRunning)
        {
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence()
    {
        coroutineRunning = true;
        Show = true;
        GameObject arrowIn = Instantiate(arrow,gameObject.transform.position,gameObject.transform.rotation);
        arrowIn.GetComponent<Arrow>().Damage = Damage;

        yield return new WaitForSecondsRealtime(Cooldown / 2f);
        Show = false;
        coroutineRunning = false;
    }
}
