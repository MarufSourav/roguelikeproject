using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLogic : MonoBehaviour
{
    //public int enemyAmount;
    public PlayerState ps;
    public GameObject end;
    public float rotateSpeed;
    public int roundEnemy;
    public int whatIsRound;
    private void Start()
    {
        end.SetActive(false);
        whatIsRound = 1;
        roundEnemy = 5;
        ps.AmountToFrag = roundEnemy;
        end.SetActive(false);
        FindObjectOfType<HordeSpawnLogic>().startSpawner();
    }
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
        if (ps.AmountToFrag == 0) 
        {
            whatIsRound++;
            Debug.Log("Finished Round");
            roundEnemy = (int)(roundEnemy * 1.25f);
            ps.AmountToFrag = roundEnemy;
            if (whatIsRound >= 20)
                end.SetActive(true);
            else 
            {
                if (whatIsRound % 1 == 5)
                    FindObjectOfType<ItemSpawner>().SpawnAbility();
            }
            FindObjectOfType<ItemSpawner>().SpawnEnhance();            
            Invoke("startSpawner", 10f);
        }        
    }
    void startSpawner() { FindObjectOfType<HordeSpawnLogic>().startSpawner(); }
}
