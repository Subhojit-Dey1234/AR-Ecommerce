using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductListCanvasDisplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject productListCanvas;
    [SerializeField]
    private Vector3 desiredScale = Vector3.one;
    private Vector3 featureScale = Vector3.one;
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

            Debug.Log("Hit"+ hit.transform.name);
            if (hit.transform == gameObject.transform)
            {
                productListCanvas.SetActive(true);
                isSelected = true;
                productListCanvas.transform.localScale = Vector3.Lerp(productListCanvas.transform.localScale, desiredScale, lerpSpeed * Time.deltaTime);
            }

            if(hit.transform.name == "FeaturesDot"){
                hit.transform.GetChild(0).gameObject.SetActive(true);
                // Debug.Log(hit.transform.localScale);
                // hit.transform.GetChild(0).gameObject.transform.localScale = Vector3.Lerp(hit.transform.localScale,new Vector3(1f,1f,1f),lerpSpeed*Time.deltaTime);
            }
        }
       
        
    }

    public void DeselectProduct()
    {
        isSelected = false;
        productListCanvas.SetActive(false);
        productListCanvas.transform.localScale = new Vector3(0, 0, 0);
    }
}
