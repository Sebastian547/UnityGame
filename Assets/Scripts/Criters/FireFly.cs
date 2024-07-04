
using UnityEngine;

public class FireFly : MonoBehaviour
{


    Vector3 wektorRuchu = Vector3.zero;
    Rigidbody2D rb;
    int step;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        step = 15;
    }
    private void LateUpdate()
    {
        if( step > 0)
        {
            step--;
            Move();
        }
        else
        {
            rb.velocity = Vector3.zero;
            wektorRuchu = Vector3.zero;
            wektorRuchu += new Vector3(Random.Range(-4, 5), Random.Range(-4, 5), Random.Range(-4, 5));

            if (wektorRuchu.x >= 0)
            {
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                
            }
            else
            {
                transform.localRotation  = Quaternion.Euler(0f, -180f, 0f);
                
            }
            
            step = 25;
        }
    }


    private void Move()
    {
        

        rb.AddForce(wektorRuchu);
    }
}
