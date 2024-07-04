using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class BossProjectile : MonoBehaviour
{
    Rigidbody2D rb;

    
    GameObject player;

    [SerializeField]
    int speed = 1;
    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(750 * -gameObject.transform.right);

        StartCoroutine(PlayerChase());
        StartCoroutine(RemoveArrow());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null && collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<IActor>().TakeDamage(5);
            Destroy(gameObject);
        }

    }
    IEnumerator PlayerChase()
    {
        while (true)
        {
            FollowPlayer();
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.AddForce(direction * speed);
    }
    IEnumerator RemoveArrow()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }


}
