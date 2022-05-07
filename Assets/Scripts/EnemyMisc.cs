using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyMisc : MonoBehaviour
{

    public float health = 100f;
    public Image healthBar;
    //public Rigidbody rb;
    public void TakeDamage (float damage)
    {        
        health -= damage;
        healthBar.fillAmount = health / 100f;
        if (health <= 0f)
        {
            Die();            
        }
    }
    private void Die() 
    {
        health = 100f;
        healthBar.fillAmount = 1f;
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.None;        
    }   
}