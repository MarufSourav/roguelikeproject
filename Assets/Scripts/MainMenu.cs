using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;   
    public GameObject panel3;
    public GameObject panel4;
    void Start()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
    }
    public void newGame() 
    {
        SceneManager.LoadScene(1);
    }
}
