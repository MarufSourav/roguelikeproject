using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TrainingBots : MonoBehaviour
{
    public PlayerState ps;
    public int frags;
    public bool botActive = false;
    public TextMeshProUGUI countdowntext;
    public TextMeshProUGUI StartEndText;
    public TextMeshProUGUI PlayerPoints;
    public GameObject botPrefab;
    GameObject spawnlocation;    
    public Image seButton;
    public bool Started = false;
    int randomVal;
    string randomValS;
    float ct = 60f;
    public float rotateSpeed;
    public void StartEndTraining()
    {
        if (!Started) 
        {            
            Started = !Started;
            ps.AmountToFrag = 0;
            frags = 0;
            seButton.color = Color.black;
            StartEndText.text = "END";                       
        }            
        else        
        {
            Started = !Started;
            StartEndText.text = "START TRAINING";
            seButton.color = Color.white;            
        }          
    }
    void SpawnBots() 
    {
        randomVal = Random.Range(0, 19);
        randomValS = randomVal.ToString();
        spawnlocation = GameObject.Find(randomValS);
        Instantiate(botPrefab, spawnlocation.transform.position, spawnlocation.transform.rotation);
        botActive = true;
    }
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
        if (Started)
        {
            if (!botActive)
                SpawnBots();
            ct -= 1 * Time.deltaTime;
            PlayerPoints.text = frags.ToString();
        }
        else 
        {            
            Destroy(GameObject.FindWithTag("BotTR"));
            botActive = false;
            ct = 60f;            
        }            
        countdowntext.text = ct.ToString("0");
        if (ct <= 0f) 
            StartEndTraining();            
    }
}
