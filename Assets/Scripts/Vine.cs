using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Vine : MonoBehaviour
{
    float x;
    float y;

    int max = 5;
    int current = 5;

    [SerializeField]
    int howFarSee = 50;

    [SerializeField]
    GameObject vine;

    [SerializeField]
    Grid grid;

    Rigidbody2D rb;

    [SerializeField]
    GameObject player;

    void Start()
    {
        
        
        gameObject.name = current.ToString();
        x = transform.position.x;
        y = transform.position.y;

        
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        if (current == max && gameObject.transform.parent.name != max.ToString()) 
        {
            max = Random.Range(0,max);    /// Random Lenght of vines
            current = max;

            body.GetComponent<FixedJoint2D>().enabled = true; 
            body.GetComponent<SpringJoint2D>().enabled = false;
        }
        else
        {

            body.GetComponent<FixedJoint2D>().enabled = false;
            body.GetComponent<SpringJoint2D>().enabled = true;
            body.GetComponent<SpringJoint2D>().connectedAnchor = new Vector2(0, 0);
        }


        if (current > 0)
        {
            NewVine(x, y, (current-1) );
        }

        rb = body;
        StartCoroutine(PlayerCheck());
    }


    IEnumerator PlayerCheck()
    {
        while (true)
        {

            float distance = (player.transform.position - transform.position).magnitude;



            if (distance < howFarSee)
            {
                
                
                rb.Sleep();
                
                
            }
            else
            {
                
                rb.WakeUp();
            }
          

            yield return new WaitForSeconds(1f);
        }
    }



    GameObject NewVine(float x,float y, int currentV)
{
       

        RaycastHit2D ray = Physics2D.Raycast(new Vector2(x, y - grid.cellSize.y/2), Vector2.down, 1f);

        if (ray.collider != null)
        { Debug.Log(ray.collider.name); }

        if (ray.collider ==null)
        {
            
            if (current > 0)
            {

                GameObject newVine = Instantiate(vine, new Vector3(x, y-grid.cellSize.y, 0), Quaternion.identity);  

                newVine.GetComponent<Vine>().current = currentV; 
                
                newVine.transform.parent = gameObject.transform;

                newVine.GetComponent<SpringJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();

                
                return gameObject;

            }
        }
        
        return null;
    }


    void jointScript()
    {

    }
}
