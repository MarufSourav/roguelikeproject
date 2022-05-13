using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectOnHit : MonoBehaviour{
    public GameObject gunMuzzle;
    public GameObject hitmarker;
    public PlayerState ps;
    public TrainingBots StartEndTraining;
    public WeaponBehaviour IA;    
    public void Effect() 
    {
        RaycastHit hit;
        Vector3 shootDirection = gunMuzzle.transform.forward;
        shootDirection.x += Random.Range(-ps.spreadFactor, ps.spreadFactor);
        shootDirection.y += Random.Range(-ps.spreadFactor, ps.spreadFactor);
        if (Physics.Raycast(gunMuzzle.transform.position, shootDirection, out hit)){            
            EnemyDamageZone target = hit.transform.GetComponent<EnemyDamageZone>();
            if (hit.transform.name == "Head")
            {
                hitmarker.gameObject.SetActive(true);
                Invoke("hitmarkerActive", .2f);
                target.TakeDamage(ps.damage * 4);
            }
            else if (hit.transform.name == "Body")
            {
                hitmarker.gameObject.SetActive(true);
                Invoke("hitmarkerActive", .2f);
                target.TakeDamage(ps.damage);
            }
            else if (hit.transform.name == "LegRight" || hit.transform.name == "LegLeft")
            {
                hitmarker.gameObject.SetActive(true);
                Invoke("hitmarkerActive", .2f);
                target.TakeDamage(ps.damage * 0.5f);
            }
            else if (hit.transform.name == "Start/End Button")
                StartEndTraining.StartEndTraining();
            else if (hit.transform.name == "InfiniteAmmo Button")
            {
                IA.infiniteAmmo = !IA.infiniteAmmo;
                Image IAI = hit.transform.GetComponent<Image>();
                if (IA.infiniteAmmo) 
                    IAI.color = Color.black;
                else
                    IAI.color = Color.white;                
            }                
        }
    }
    void hitmarkerActive() 
    {
        hitmarker.gameObject.SetActive(false);
    }
}
