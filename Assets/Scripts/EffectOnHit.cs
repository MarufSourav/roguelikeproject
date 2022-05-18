using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectOnHit : MonoBehaviour{
    public Transform gunMuzzle;
    public Transform BulletSpawnPoint;    
    public ParticleSystem ImpactParticleSystem;
    public TrailRenderer BulletTrail;
    public GameObject hitmarker;
    public PlayerState ps;
    public TrainingBots StartEndTraining;
    public WeaponBehaviour IA;
    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit) 
    {
        float time = 0f;
        Vector3 startposition = Trail.transform.position;
        while (time < 1) 
        {
            Trail.transform.position = Vector3.Lerp(startposition, hit.point, time);
            time += Time.deltaTime / Trail.time;
            yield return null;
        }
        Trail.transform.position = hit.point;
        Instantiate(ImpactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(Trail.gameObject, Trail.time);
    }
    public void Effect() 
    {
        RaycastHit hit;
        Vector3 shootDirection = gunMuzzle.transform.forward;
        shootDirection.x += Random.Range(-ps.spreadFactor, ps.spreadFactor);
        shootDirection.y += Random.Range(-ps.spreadFactor, ps.spreadFactor);
        if (Physics.Raycast(gunMuzzle.transform.position, shootDirection, out hit)){
            TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
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
