using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour {
    public static MainManager Instance;

    // Data that is saved between scenes
    public int dificulty;
    public float[] crosshairSettings = new float[] {15, 7, 2}; // [space, length, width]
    public bool crosshairDotToggle = true;

    private void Awake() {
        
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load persistent data
        LoadCrosshair();
        LoadDificulty();
    }

    [System.Serializable]
    class SaveCrosshair {
        public bool dot;
        public float width;
        public float length;
        public float space;

    }

    [System.Serializable]
    class SaveHighScore {
        public float time;
    }

    [System.Serializable]
    class SaveDificulty {
        public int dificulty;
    }

    public void SaveNewCrosshair () {
        SaveCrosshair data = new SaveCrosshair();

        data.dot = crosshairDotToggle;
        data.space = crosshairSettings[0];
        data.length = crosshairSettings[1];
        data.width = crosshairSettings[2];

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/crosshair.json", json);
    }

    private void LoadCrosshair () {
        string path = Application.persistentDataPath + "/crosshair.json";

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveCrosshair data = JsonUtility.FromJson<SaveCrosshair>(json);

            crosshairDotToggle = data.dot;
            crosshairSettings[0] = data.space;
            crosshairSettings[1] = data.length;
            crosshairSettings[2] = data.width;
        }

    }

    public void SaveNewDificulty () {
        SaveDificulty data = new SaveDificulty();

        data.dificulty = dificulty;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/dificulty.json", json);
    }

    private void LoadDificulty () {
        string path = Application.persistentDataPath + "/dificulty.json";

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveDificulty data = JsonUtility.FromJson<SaveDificulty>(json);

            dificulty = data.dificulty;
        }
    }
}
