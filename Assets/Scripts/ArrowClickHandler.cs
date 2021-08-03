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

        int count = 0;
        
        if (Input.touchCount == 0)
            return;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Input.GetTouch(0).phase == TouchPhase.Began

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
            {
                count += 1;
                Debug.Log("Ray hit right arrow" + hit.transform.name + count);
                
                if (hit.transform.name == "RightArrow")
                {

                 if(count == 1)   
                {
                   if( productListPanel.GetComponent<ProductListAPIManager>().productindex < 13)
                    {
                        productListPanel.GetComponent<ProductListAPIManager>().productindex += 3;
                        productListPanel.GetComponent<ProductListAPIManager>().ArrowClicked();
                        return;
                    }
                }
                }
                if (hit.transform.name == "LeftArrow")
                {
                    Debug.Log("Ray hit left arrow");
                    if(count == 1)
                    {
                    if (productListPanel.GetComponent<ProductListAPIManager>().productindex >=3)
                    {
                        productListPanel.GetComponent<ProductListAPIManager>().productindex -= 3;
                        productListPanel.GetComponent<ProductListAPIManager>().ArrowClicked();
                        return;
                    }
                    }

                }
            }
        }
    }
}
