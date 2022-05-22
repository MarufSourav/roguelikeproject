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
        Debug.Log("Successful Parry");
        ps.parriedProjectile = true;
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player") 
        {
            if (ps.parry)
            {
                parryIndicator();
                parriedProjectile=true;
                ps.parriedProjectile = true;
                projectileMove.x = 0;
                projectileMove.y = 0;
                projectileMove.z = -projectileSpeed;
            }
            else
            {
                destroyProjectile();
            }           
        }
        else if (other.gameObject.tag == "Wall"|| other.gameObject.tag == "Ground")
            destroyProjectile();

        if (other.gameObject.tag == "BotTR" && parriedProjectile) 
        {
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
