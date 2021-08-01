using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRotationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject productParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
        {
            if (hit.transform != gameObject.transform && productParent.GetComponent<ProductListCanvasDisplayManager>().isSelected ==true)
            {
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.position.x - gameObject.transform.position.x,
                               0f,
                               Camera.main.transform.position.z - gameObject.transform.position.z));
                gameObject.transform.rotation = newRotation;


            }
        }
    }
}
