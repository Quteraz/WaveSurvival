using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUiManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI wave;
    [SerializeField] TextMeshProUGUI currency;
    [SerializeField] TextMeshProUGUI ammo;

    [SerializeField] SpawnManager spawnManager;
    [SerializeField] PlayerController playerController;

    private float time = 0f;

    // Start is called before the first frame update
    void Start() {
        playerController.OnHealthChange += ReduceHealth;
    }

    // Update is called once per frame
    void Update() {
        UpdateTime();
    }

    private void UpdateTime () {
        time += Time.deltaTime;
        int timeMin = (int) time / 60;
        int timeSec = (int) time % 60;
        timer.text = "Time: " + timeMin + ":" + timeSec;
    }

    private void ReduceHealth (int newHealth) {
        health.text = "Health: " + newHealth;
    }
}
