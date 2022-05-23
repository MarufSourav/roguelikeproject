using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class projectileMotion : MonoBehaviour
{
    public float projectileRange;
    public float projectileSpeed;
    private Vector3 projectileMove;
    public PlayerState ps;
    public bool parriedProjectile;
    void parryIndicator() 
    {
        FindObjectOfType<AudioManager>().Play("ParrySound");
        if (ps.ammoOnParry)
            ps.magAmmo = 20;        
        parriedProjectile = true;
        projectileMove.x = 0;
        projectileMove.y = 0;
        projectileMove.z = -projectileSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player") 
        {
            if (ps.parry)
            {
                parryIndicator();
                other.GetComponent<PlayerMovement>().ReCalibrateDash();
            }
            else
            {
                destroyProjectile();
                if(!ps.invulnerable)
                    SceneManager.LoadScene(0);
            }           
        }
        else if (other.gameObject.tag == "Wall"|| other.gameObject.tag == "Ground")
            destroyProjectile();

        if (other.gameObject.tag == "BotTR" && parriedProjectile) 
        {
            FindObjectOfType<AudioManager>().Play("HitMarkerSound");
            other.gameObject.GetComponentInParent<EnemyHP>().Die();            
            destroyProjectile();
        }
    }
    private void Start()
    {
        parriedProjectile = false;
        Invoke("destroyProjectile", projectileRange);
        transform.LookAt(GameObject.Find("Player").transform);
        projectileMove.x = 0;
        projectileMove.y = 0;
        projectileMove.z = projectileSpeed;
    }
    
    private void FixedUpdate(){transform.Translate(projectileMove);}
    void destroyProjectile() {Destroy(gameObject);}
}
