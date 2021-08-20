using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CroshairUiSettings : MonoBehaviour {

    private Slider crosshairSpace;
    private Slider crosshairWidth;
    private Slider crosshairLength;
    private Toggle centerDot;

    

    private CrosshairController crosshair;
    // Start is called before the first frame update
    private void Start() {
        crosshair = GetComponentInChildren<CrosshairController>();
        Toggle centerDot = GetComponentInChildren<Toggle>();
        // Debug.Log(sliders.Length);
        if (MainManager.Instance != null) {
            foreach (Slider slider in GetComponentsInChildren<Slider>()) {
                switch (slider.gameObject.name) {
                    case "CHSpace":
                        // Debug.Log("slider set");
                        crosshairSpace = slider;
                        crosshairSpace.value = MainManager.Instance.crosshairSettings[0];
                        break;
                    case "CHLength":
                        crosshairLength = slider;
                        crosshairLength.value = MainManager.Instance.crosshairSettings[1];
                        break;
                    case "CHWidth":
                        crosshairWidth = slider;
                        crosshairWidth.value = MainManager.Instance.crosshairSettings[2];
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void OnCrosshairSpaceChanged () {
        MainManager.Instance.crosshairSettings[0] = crosshairSpace.value;
        crosshair.OnCrosshairChanged();
    }
    public void OnCrosshairLengthChanged () {
        MainManager.Instance.crosshairSettings[1] = crosshairLength.value;
        crosshair.OnCrosshairChanged();
    }

    public void OnCrosshairWidthChanged () {
        MainManager.Instance.crosshairSettings[2] = crosshairWidth.value;
        crosshair.OnCrosshairChanged();
    }

    public void OnCrosshairDotToggled () {
        MainManager.Instance.crosshairDotToggle = centerDot.isOn;
        crosshair.OnCrosshairChanged();
    }
}
