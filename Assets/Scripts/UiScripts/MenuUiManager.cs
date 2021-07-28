using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUiManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI dificulty;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void StartGame () {
        SceneManager.LoadScene(1);
    }

    public void SetDificulty (TMP_Dropdown change) {
        MainManager.Instance.dificulty = change.value + 1;
    }
}
