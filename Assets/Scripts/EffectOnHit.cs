using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EffectOnHit : MonoBehaviour{
    public Transform gunMuzzle;
    public Transform PBSP;
    public Transform RBSP;
    Transform BulletSpawnPoint;    
    public TrailRenderer BulletTrail;
    public PlayerState ps;
    public WeaponBehaviour IA;
    GameObject hitMarker;
    private void Start() {
        hitMarker = GameObject.Find("HitMarker");
        hitMarker.SetActive(false);
    }
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
        Destroy(Trail.gameObject, Trail.time);
    }
    public void Effect() 
    {
        if (ps.gunType == "Pistol")
            BulletSpawnPoint = PBSP;
        else if (ps.gunType == "Rifle")
            BulletSpawnPoint = RBSP;
        RaycastHit hit;
        Vector3 shootDirection = gunMuzzle.transform.forward;
        shootDirection.x += Random.Range(-GetComponent<WeaponBehaviour>().spreadFactor, GetComponent<WeaponBehaviour>().spreadFactor);
        shootDirection.y += Random.Range(-GetComponent<WeaponBehaviour>().spreadFactor, GetComponent<WeaponBehaviour>().spreadFactor);
        if (Physics.Raycast(gunMuzzle.transform.position, shootDirection, out hit)){
            TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            EnemyHP target = hit.transform.GetComponentInParent<EnemyHP>();
            EnemyAi EAI = hit.transform.GetComponentInParent<EnemyAi>();
            if (hit.transform.name == "Head")
            {
                hitMarker.SetActive(true);
                Invoke("hitmarkerActive", .2f);
                target.HP(ps.damage * 4);
                EAI.enemyShot = true;
            }
            else if (hit.transform.name == "Body")
            {
                hitMarker.SetActive(true);
                Invoke("hitmarkerActive", .2f);
                target.HP(ps.damage);
                EAI.enemyShot = true;
            }
            else if (hit.transform.name == "LegRight" || hit.transform.name == "LegLeft")
            {
                hitMarker.SetActive(true);
                Invoke("hitmarkerActive", .2f);
                target.HP(ps.damage * 0.5f);
                EAI.enemyShot = true;
            }
            else if (hit.transform.name == "Start/End Button")
                FindObjectOfType<TrainingBots>().StartEndTraining();
            else if (hit.transform.name == "InfiniteAmmo Button")
            {
                IA.infiniteAmmo = !IA.infiniteAmmo;
                Image IAI = hit.transform.GetComponent<Image>();
                if (IA.infiniteAmmo)
                    IAI.color = Color.black;
                else
                    IAI.color = Color.white;
            }
            else if (hit.transform.name == "StartGame Button")
            {
                FindObjectOfType<RandomLevelSpawner>().randomLevel();
            }
        }
    }
    void hitmarkerActive() 
    {
        hitMarker.SetActive(false);
    }
}
