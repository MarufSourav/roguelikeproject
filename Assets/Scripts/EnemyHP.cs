using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyHP : MonoBehaviour
{
    public float health = 100f;
    public Image healthBar;
    public TrainingBots tb; 
    public void HP(float damage)
    {
        health -= damage;
        FindObjectOfType<AudioManager>().Play("HitMarkerSound");
        healthBar.fillAmount = health / 100f;
        if (health <= 0f)
            Die();        
    }
    private void Die()
    {
        FindObjectOfType<AudioManager>().Play("FragSoundEffect");
        tb = GetComponent<TrainingBots>();
        tb.frags++;
        Destroy(gameObject);
    }
   
}
