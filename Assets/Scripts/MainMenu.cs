using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Text inputValue;
    public Slider sliderValue;
    public GameObject mainMenu;
    public GameObject Player;
    public GameObject Camera;
    bool menuActive = false;
    private void Start()
    {
        Player.GetComponent<MouseLook>().enabled = true;
        Player.GetComponent<PlayerMovement>().enabled = true;
        Player.GetComponent<WeaponBehaviour>().enabled = true;
        Camera.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainMenu.SetActive(false);
        menuActive = false;
    }
    private void Update()
    {
        inputValue.text = sliderValue.value.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel();             
        }
    }
    public void menuPanel() 
    {
        if (!menuActive)
        {
            Player.GetComponent<MouseLook>().enabled = false;
            Player.GetComponent<PlayerMovement>().enabled = false;
            Player.GetComponent<WeaponBehaviour>().enabled = false;
            Camera.GetComponent<MouseLook>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mainMenu.SetActive(true);
            menuActive = !menuActive;
        }
        else
        {
            Player.GetComponent<MouseLook>().enabled = true;
            Player.GetComponent<PlayerMovement>().enabled = true;
            Player.GetComponent<WeaponBehaviour>().enabled = true;
            Camera.GetComponent<MouseLook>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mainMenu.SetActive(false);
            menuActive = !menuActive;
        }
    }
    public void exit() 
    {
        Application.Quit();
    }
}
