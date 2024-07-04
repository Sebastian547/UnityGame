using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IActor
{
    [SerializeField]
    GameObject hpBar;

    [SerializeField]
    int health = 100;

    [SerializeField]
    int damage = 1;

    [SerializeField]
    int speed = 1;

    [SerializeField]
    List <GameObject> arrow;

    [SerializeField]
    GameObject player;

    Rigidbody2D rb;

    [SerializeField]
    float printValue;

    public event System.Action gameWin;
    public void TakeDamage(int Damage)
    {
        health = health - Damage;
        hpBar.GetComponent<HpBar>().UpdateHP(health);
        if (health <= 0)
        {
            Destroy(gameObject);
            gameWin.Invoke();
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PlayerChaseCoroutine());
        StartCoroutine(SpecialAttackCoroutine());
    }
    

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.AddForce(direction * speed);
    }

    private void SpecialAttack()
    {
        int i = Random.Range(0, arrow.Count);
        Instantiate(arrow[i], gameObject.transform.position, gameObject.transform.rotation);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<IActor>().TakeDamage(damage);
        }
    }
    IEnumerator SpecialAttackCoroutine()
    {
        while (true)
        {

            SpecialAttack();

            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator PlayerChaseCoroutine()
    {
        while (true)
        {

            FollowPlayer();

            yield return new WaitForSeconds(1f);
        }
    }
}

