using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Vector3 targetVal = Vector3.zero;
    GameObject parent;
    float SPEED = 6f;
    Vector3 desiredScale;
    void Start()
    {
        Debug.Log("Animation Manager Started");
        parent = GameObject.Find("Parent");

        Debug.Log("Parent",parent);
        desiredScale = new Vector3(0.8f,0.8f,0.8f);
    }
    void Update()
    {
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out RaycastHit hit)){
            Debug.Log("Hit");
            if(hit.transform.CompareTag("CubeObject")){
                Debug.Log(hit.transform.tag);
                parent.transform.localScale = Vector3.Lerp(parent.transform.localScale,desiredScale,SPEED*Time.deltaTime);

                parent.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position);
                // parent.transform.LookAt(Camera.main.transform);
                targetVal = gameObject.transform.localEulerAngles;
                targetVal.x = 0;
                targetVal.z = 0;
                parent.transform.localEulerAngles = targetVal;
            }
        }
    }
}
