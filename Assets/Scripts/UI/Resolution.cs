using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{

    float referenceResX = 1080, referenceResY = 1920;
    float currentResX, currentResY;
    private Vector2 resPos;

    public void Start() // public because will be called if any object is added to the canvas
    {
        GetCanvasResolution();
        SetResolution();
    }

    private void SetResolution()
    {
        Loop(gameObject, referenceResX, referenceResY);
    }
    public void SetResolution(GameObject obj)
    {
        Loop(obj, referenceResX, referenceResY);
    }

    private void Loop(GameObject currentObj, float oldResX, float oldResY)
    {
        foreach (Transform child in currentObj.transform)
        {
            if (child.transform.childCount > 0){
                Loop(child.gameObject, oldResX, oldResY);
            }
            RectTransform rt = child.gameObject.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(currentResX * rt.anchoredPosition.x / oldResX, currentResY * rt.anchoredPosition.y / oldResY);
            
            
            float newResX = currentResX * rt.sizeDelta.x / oldResX;
            float newResY;
            if(rt.sizeDelta.y == oldResY) newResY = currentResY * rt.sizeDelta.y / oldResY;
            else newResY = currentResX * rt.sizeDelta.y / oldResX;
            

            rt.sizeDelta = new Vector2(newResX,newResY);

            HorizontalOrVerticalLayoutGroup LG = child.gameObject.GetComponent<HorizontalOrVerticalLayoutGroup>();
            TextMeshProUGUI text = child.gameObject.GetComponent<TextMeshProUGUI>();
            
            if(LG != null) LG.spacing = currentResX * LG.spacing / oldResX;
            if(text != null) text.fontSize = currentResX * text.fontSize / oldResX;
            
        }
    }

    private void GetCanvasResolution()
    {
        currentResX = gameObject.GetComponent<RectTransform>().rect.width;
        currentResY = gameObject.GetComponent<RectTransform>().rect.height;
    }

}