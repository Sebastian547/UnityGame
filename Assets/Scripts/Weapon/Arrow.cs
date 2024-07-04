using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    int damage;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(750 * -gameObject.transform.right);

        StartCoroutine(RemoveArrow());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null && collision.collider.tag == "Enemy")
        {
            collision.collider.GetComponent<IActor>().TakeDamage(1*damage);
            Destroy(gameObject);
        }

    }

    IEnumerator RemoveArrow()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
}