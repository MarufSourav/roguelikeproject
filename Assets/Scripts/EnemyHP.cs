using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHP : MonoBehaviour
{
    public float health = 100f;
    public Image healthBar;    
    public void HP(float damage)
    {
        health -= damage;
        FindObjectOfType<AudioManager>().Play("HitMarkerSound");
        healthBar.fillAmount = health / 100f;
        if (health <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {        
        Invoke("Regen", 2f);
        FindObjectOfType<AudioManager>().Play("FragSoundEffect");
    }
    private void Regen() 
    {
        health = 100f;
        healthBar.fillAmount = 1f;
    }
}
