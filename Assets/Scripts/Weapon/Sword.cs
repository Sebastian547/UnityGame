using System.Collections;

using UnityEngine;

public class Sword : Weapon
{
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
        bc2d.enabled = true;
        yield return new WaitForSecondsRealtime(Cooldown/2f);
        Show = false; 
        bc2d.enabled = false;
        coroutineRunning =false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy")
        {
            collision.GetComponent<IActor>().TakeDamage(Damage);
        }
    }
    
}