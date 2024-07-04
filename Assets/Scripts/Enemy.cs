using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IActor
{

    [SerializeField]
    int health = 5;

    [SerializeField]
    int damage = 1;

    [SerializeField]
    int speed = 1;

    [SerializeField]
    int howFarSee = 2;

    [SerializeField]
    GameObject player;

    Rigidbody2D rb;

    [SerializeField]
    float printValue;


    public void TakeDamage(int Dmg)
    {
        health -= Dmg;
        if (health <= 0) 
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PlayerChase());
    }


    IEnumerator PlayerChase()
    {
        while (true)
        {

            float distance = (player.transform.position - transform.position).magnitude;

            printValue = distance;

            if (distance < howFarSee)
            {
                FollowPlayer();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<IActor>().TakeDamage(damage);
        }
    }
}