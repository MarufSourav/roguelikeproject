using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TrainingBots : MonoBehaviour
{
    public int frags;
    [SerializeField] TextMeshProUGUI countdowntext;
    public TextMeshProUGUI StartEndText;
    public TextMeshProUGUI PlayerPoints;
    public GameObject botPrefab;
    GameObject spawnlocation;    
    public Image seButton;    
    public bool Started = false;
    int randomVal;
    string randomValS;
    float ct = 60f;    
    public void StartEndTraining()
    {
        if (!Started) 
        {            
            Started = true;
            frags = 0;
            Debug.Log(frags);
            seButton.color = Color.black;
            StartEndText.text = "END";
            for (int i = 0; i < 1; i++) 
            {
                SpawnBots();
            }
        }            
        else        
        {
            Started = false;
            StartEndText.text = "START";
            seButton.color = Color.white;
        }          
    }
    void SpawnBots() 
    {
        randomVal = UnityEngine.Random.Range(0, 19);
        randomValS = randomVal.ToString();  
        Debug.Log("Random Value: " + randomValS);
        spawnlocation = GameObject.Find(randomValS);
        Instantiate(botPrefab, spawnlocation.transform.position, spawnlocation.transform.rotation);
    }
    private void Update()
    {
        if (Started) 
        {
            ct -= 1 * Time.deltaTime;
            PlayerPoints.text = frags.ToString();            
        }            
        else 
            ct = 60f;
        countdowntext.text = ct.ToString("0.0");
        if (ct <= 0f) 
            StartEndTraining();
    }
}
