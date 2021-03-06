using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyHP : MonoBehaviour
{
    public float health = 100f;
    public Image healthBar;
    public GameObject botGO;
    public PlayerState ps;
    private void Start(){
        botGO.SetActive(true);
    }
    public void HP(float damage)
    {
        health -= damage;
        FindObjectOfType<AudioManager>().Play("HitMarkerSound");
        healthBar.fillAmount = health / 100f;
        if (health <= 0f)
            Die();        
    }
    public void Die()
    {
        ps.AmountToFrag--;
        FindObjectOfType<AudioManager>().Play("FragSoundEffect");
        if (FindObjectOfType<TrainingBots>() != null) 
        {
            FindObjectOfType<TrainingBots>().frags++;
            FindObjectOfType<TrainingBots>().botActive = false;
        }
        Destroy(gameObject);
    }
}
