using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
        [SerializeField]
        int damage = 1;
        [SerializeField]
        int knockback = 1;
        [SerializeField]
        float cooldown = 1;

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public int KnockBack
        {
            get { return knockback; }
            set { knockback = value; }
        }
        public float Cooldown
        {
            get { return cooldown; }
            set { cooldown = value; }
        }
    }
