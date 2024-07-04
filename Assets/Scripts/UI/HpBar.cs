using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    private Image hpBarImage;

    private int maxHP = 100; 
    private int currentHP;
   

    public void UpdateHP(int health)
    {
        currentHP = health;
        float fillAmount = (float)currentHP / (float) maxHP;
        hpBarImage.fillAmount = fillAmount;
        
    }
}
