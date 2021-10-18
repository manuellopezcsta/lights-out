using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSwitch : MonoBehaviour
{

    public bool isOn = false;
    private Behaviour halo;

    // Start is called before the first frame update
    void Start()
    {
        halo = (Behaviour)GetComponent("Halo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change()
    {
        // Get the current position of the plane.
        Vector3 currentPos = transform.Find("Plane").position;
        float offset = 0.8f;
        if(isOn)
        {
            isOn = false;            
            //currentPos = new Vector3(currentPos.x, currentPos.y, currentPos.z + 0.5f);
            transform.Find("Plane").position = new Vector3(currentPos.x, currentPos.y, currentPos.z + offset);
            //transform.localEulerAngles = new Vector3(0, 45, 0);
            // Apago el halo
            //halo.enabled = true;            
                           
        }else{
            isOn = true;
            //currentPos = new Vector3(currentPos.x, currentPos.y, currentPos.z - 0.5f);
            transform.Find("Plane").position = new Vector3(currentPos.x, currentPos.y, currentPos.z - offset);
            //transform.localEulerAngles = Vector3.zero;
            //halo.enabled = false;
        }
    }
}
