using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductListCanvasDisplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject productListCanvas;
    [SerializeField]
    private Vector3 desiredScale = Vector3.one;
    [SerializeField]
    private float lerpSpeed = 10f;
    [HideInInspector]
    public bool isSelected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
        {
            if (hit.transform == gameObject.transform)
            {
                productListCanvas.SetActive(true);
                isSelected = true;
                productListCanvas.transform.localScale = Vector3.Lerp(productListCanvas.transform.localScale, desiredScale, lerpSpeed * Time.deltaTime);
                

            }
        }
        /*if(isSelected == true)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.position.x - productListCanvas.transform.position.x,
                                0f,
                                Camera.main.transform.position.z - productListCanvas.transform.position.z));
            productListCanvas.transform.rotation = newRotation;
        }*/
        
    }

    public void DeselectProduct()
    {
        isSelected = false;
        productListCanvas.SetActive(false);
        productListCanvas.transform.localScale = new Vector3(0, 0, 0);
    }
}
