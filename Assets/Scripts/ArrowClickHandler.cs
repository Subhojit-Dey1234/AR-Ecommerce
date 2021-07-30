using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowClickHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject productListPanel;


    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount == 0)
            return;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
            {
                Debug.Log("Ray hit right arrow" + hit.transform.name);
                if (hit.transform.name == "RightArrow")
                {
                    
                   if( productListPanel.GetComponent<ProductListAPIManager>().productindex < 13)
                    {
                        productListPanel.GetComponent<ProductListAPIManager>().productindex += 3;
                        productListPanel.GetComponent<ProductListAPIManager>().ArrowClicked();
                    }
                    
                }
                if (hit.transform.name == "LeftArrow")
                {
                    Debug.Log("Ray hit left arrow");
                    if (productListPanel.GetComponent<ProductListAPIManager>().productindex >=3)
                    {
                        productListPanel.GetComponent<ProductListAPIManager>().productindex -= 3;
                        productListPanel.GetComponent<ProductListAPIManager>().ArrowClicked();
                    }

                }
            }
        }
    }
}
