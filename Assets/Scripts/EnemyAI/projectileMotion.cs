using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class projectileMotion : MonoBehaviour
{
    public float projectileRange;
    public float projectileSpeed;
    private Vector3 projectileMove;
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log("Hit Player");
            destroyProjectile();
            SceneManager.LoadScene(0);
        }
        else if (other.gameObject.tag == "Wall"|| other.gameObject.tag == "Ground")
        {
            Debug.Log("Hit Obsticle");
            destroyProjectile();
        }
    }
    private void Start()
    {
        Invoke("destroyProjectile", projectileRange);        
        transform.LookAt(GameObject.Find("Player").transform);
        projectileMove.x = 0;
        projectileMove.y = 0;
        projectileMove.z = projectileSpeed;
    }
    
    private void FixedUpdate(){transform.Translate(projectileMove);}
    void destroyProjectile() {Destroy(gameObject);}
}
