using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelLogic : MonoBehaviour
{
    public PlayerState ps;
    public GameObject end;
    public float rotateSpeed;
    public int roundEnemy;
    public int whatIsRound;
    public TextMeshProUGUI roundNumber;
    private void Start()
    {
        end.SetActive(false);
        whatIsRound = 1;
        roundNumber.text = whatIsRound.ToString();
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
            FindObjectOfType<HordeSpawnLogic>().stopSpawner();
            roundNumber.color = Color.black;
            whatIsRound++;
            roundEnemy = (int)(roundEnemy * 1.25f);
            ps.AmountToFrag = roundEnemy;
            if (whatIsRound == 12)
            {
                end.SetActive(true);
                FindObjectOfType<AudioManager>().Play("EndIndicatorAnnouncementSound");
            }
            FindObjectOfType<ItemSpawner>().SpawnItem();
            Invoke("startSpawner", 10f);
        }        
    }
    void startSpawner() { 
        FindObjectOfType<HordeSpawnLogic>().startSpawner();
        roundNumber.text = whatIsRound.ToString(); roundNumber.color = Color.white;
    }
}
