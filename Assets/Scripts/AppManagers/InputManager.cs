using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld, Camera.main.transform.forward);
                
            if (touch.phase == TouchPhase.Began)
            {
                if (hitInformation.collider != null) {
                    GameObject touchedObject = hitInformation.transform.gameObject;
                    touchedObject.GetComponent<CubeObj>().OnMouseDown();
                }  
            }
        }
    }
}