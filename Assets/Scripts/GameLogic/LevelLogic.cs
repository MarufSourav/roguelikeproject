using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLogic : MonoBehaviour
{
    //public int enemyAmount;
    public PlayerState ps;
    public GameObject rangeEnemy;
    public GameObject meleeEnemy;
    GameObject spawnLocation;
    EnemyTypeSpawn enemyType;    
    public GameObject end;
    public float rotateSpeed;
    public int justSpawnedLocation;
    public float timer;
    IEnumerator SpawnBots()
    {    
        yield return new WaitForSeconds(timer);
        if(justSpawnedLocation > 8)
            justSpawnedLocation = 1;
        spawnLocation = GameObject.Find(justSpawnedLocation.ToString());        
        justSpawnedLocation++;
        
        enemyType = spawnLocation.GetComponent<EnemyTypeSpawn>();
        if (ps.AmountToFrag <= 7) 
        {
            Debug.Log("BotSpawned");
            if (enemyType.meleeEnemy)
                Instantiate(meleeEnemy, spawnLocation.transform.position, spawnLocation.transform.rotation);
            else
                Instantiate(rangeEnemy, spawnLocation.transform.position, spawnLocation.transform.rotation);
            ps.AmountToFrag++;
        }
    }
    private void Start()
    {
        justSpawnedLocation = 1;
        ps.AmountToFrag = 0;
        timer = 5f;
        end.SetActive(false);
    }
    private void Update()
    {
        StartCoroutine(SpawnBots());
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
        if (ps.AmountToFrag > 0)
            end.SetActive(false);
        else
            end.SetActive(true);
    }
}
