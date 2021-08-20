using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUiManager : MonoBehaviour {

    private bool menu = false;
    private GameObject[] settingMenuItems;
    private GameObject[] mainMenuItems;

    // Start is called before the first frame update
    void Start() {
        settingMenuItems = GameObject.FindGameObjectsWithTag("SettingMenu");
        mainMenuItems = GameObject.FindGameObjectsWithTag("MainMenu");

        foreach(GameObject menuItem in settingMenuItems) {
            menuItem.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void StartGame () {
        MainManager.Instance.SaveNewDificulty();
        SceneManager.LoadScene(1);
    }

    public void OpenMenu () {

        foreach(GameObject menuItem in mainMenuItems) {
            menuItem.SetActive(menu); 
        }

        foreach(GameObject menuItem in settingMenuItems) {
            menuItem.SetActive(!menu);
        }

        // saves settings when toggling to and from menu
        MainManager.Instance.SaveNewCrosshair();

        menu = !menu;
    }

    public void SetDificulty (TMP_Dropdown change) {
        MainManager.Instance.dificulty = change.value + 1;
    }
}
