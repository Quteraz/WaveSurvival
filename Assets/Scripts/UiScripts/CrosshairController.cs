using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CrosshairController : MonoBehaviour {

    private Image[] crosshairList;
    void Start() {
        
        crosshairList = GetComponentsInChildren<Image>();

        if (MainManager.Instance != null) {
            OnCrosshairChanged();
        }
    }
    
    public void OnCrosshairChanged () {
        float crosshairSpace = MainManager.Instance.crosshairSettings[0];
        float crosshairLength = MainManager.Instance.crosshairSettings[1];
        float crosshairWidth = MainManager.Instance.crosshairSettings[2];
        foreach (Image item in crosshairList) {

            switch (item.name) {
                case "CrosshairDot":
                    item.gameObject.SetActive(MainManager.Instance.crosshairDotToggle);
                    break;
                case "CrosshairLeft":
                    SetTransform(item, -crosshairSpace,0, crosshairLength, crosshairWidth);
                    break;
                case "CrosshairRight":
                    SetTransform(item, crosshairSpace,0, crosshairLength, crosshairWidth);
                    break;
                case "CrosshairBottom":
                    SetTransform(item,0, -crosshairSpace, crosshairWidth, crosshairLength);
                    break;
                case "CrosshairTop":
                    SetTransform(item,0, crosshairSpace, crosshairWidth, crosshairLength);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetTransform (Image item, float cSpacex, float cSpacey, float cLength, float cWidth) {
        Debug.Log(cSpacex);
        Vector3 size = new Vector3 (cLength, cWidth, 0);
        Vector3 space = new Vector3 (cSpacex,cSpacey,0);
        item.gameObject.transform.localScale = size;
        item.gameObject.transform.localPosition = space;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
