using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFollowMouse : MonoBehaviour
{
    private Vector3 MousePosition
    {
        get
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector2 mouseWorldPosition = MousePosition; 
        
        // get direction you want to point at
        Vector2 direction = (mouseWorldPosition - (Vector2) transform.position).normalized;

        // set vector of transform directly
        transform.right = direction;
    }
}
