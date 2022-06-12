using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Text FPSText;
    float pollingTime = 1f;
    float time;
    int frameCount;
    public Text inputValue;
    public Slider sliderValue;
    public GameObject mainMenu;
    public GameObject Player;
    public GameObject Camera;
    bool menuActive = false;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainMenu.SetActive(false);
        menuActive = false;
    }
    private void Update()
    {
        inputValue.text = sliderValue.value.ToString();
        time += Time.deltaTime;
        frameCount++;
        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            FPSText.text = frameCount.ToString() + " FPS";
            time -= pollingTime;
            frameCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            menuPanel();
    }
    public void backToTraining() { SceneManager.LoadScene(0); Time.timeScale = 1f; }
    public void menuPanel() 
    {
        if (!menuActive)
        {
            if(FindObjectOfType<RifleStateManager>() != null)
                FindObjectOfType<RifleStateManager>().enabled = false;
            FindObjectOfType<PlayerMovement>().enabled = false;
            FindObjectOfType<MouseLook>().enabled = false;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mainMenu.SetActive(true);
            menuActive = !menuActive;
        }
        else
        {
            if (FindObjectOfType<RifleStateManager>() != null)
                FindObjectOfType<RifleStateManager>().enabled = true;
            FindObjectOfType<PlayerMovement>().enabled = true;
            FindObjectOfType<MouseLook>().enabled = true;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mainMenu.SetActive(false);
            menuActive = !menuActive;
        }
    }
    
    public void exit(){
        Application.Quit();
    }
}
