using Unity.VisualScripting;
using UnityEngine;


public class Weapon : MonoBehaviour, IAction
{
    
    int damage      = 1;
    int knockback   = 1;
    float cooldown  = 1;
    bool show = false;


    private void Start()
    {
        
    }

    public void Action()
    {
        Atakuj();
    }

    public virtual void Atakuj()
    {

    }
    
    private void LateUpdate()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = show;
    }

    public int Damage
    {
        get { return damage;  }  
        set { damage = value; }
    }

    public int KnockBack
    {
        get { return knockback;  }
        set { knockback = value; }
    }
    public float Cooldown
    {
        get { return   cooldown;}
        set { cooldown = value; }
    }
    public bool Show
    {
        get { return show; }
        set { show = value; }
    }

    public void InitiateDifficulty()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty");

        switch (difficulty)
        {
            case 0:
                Damage = 10;
                break;
            case 1:
                Damage = 5;
                break;
            case 2:
                Damage = 1;
                break;
        }
    }
}
