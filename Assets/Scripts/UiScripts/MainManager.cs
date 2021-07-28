using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {
    public static MainManager Instance;


    // Data that is saved between scenes
    public int dificulty;

    private void Awake() {
        
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
