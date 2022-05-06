using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMisc : MonoBehaviour
{
    public float health = 100f;    
    public Rigidbody rb;    
    public void TakeDamage (float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();            
        }
    }
    private void Die() 
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;        
    }   
}