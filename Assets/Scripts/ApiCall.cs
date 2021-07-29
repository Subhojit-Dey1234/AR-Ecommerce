using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using System;

public class ApiCall : MonoBehaviour
{
    public GameObject prefab;
    public Texture2D texture2D;
    public Sprite defaultIcon;
    float xPos = -2.57f;
    float dist = 2.67f;
    [Serializable]
    public class Data
    {
        public int id;
        public string title;
        public string price;
        public string image;
        public Sprite icon;
    }

    Data[] totalData;
    void Start()
    {
        StartCoroutine(GetRequest("https://fakestoreapi.com/products"));
        Debug.Log("Api Scripts Working");
    }
    IEnumerator GetGameData(){
        
        for(int i = 0; i< 3 ; i++ ){
            Debug.Log(totalData[i].id);
            var design = Instantiate(prefab);
            design.transform.parent = GameObject.Find("Parent").transform;
            design.transform.localPosition = new Vector3(xPos,0.01f,-0.5699999f);
            design.transform.localScale = new Vector3(0.72f,0.72f,0.72f);
            design.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().text = totalData[i].title;
            design.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = totalData[i].icon;
            design.transform.GetChild(1).transform.localPosition = new Vector3(0f,0f,-1.9f);
            Debug.Log(totalData[i].icon);
            xPos = xPos + dist;
            yield return totalData[i];
        }
    }

    IEnumerator GetGameIcon(){
        Debug.Log("Icon");
        for(int i = 0; i< 3 ; i++ ){
            Debug.Log(totalData[i].image);
            UnityWebRequest w =  UnityWebRequestTexture.GetTexture(totalData[i].image);
            yield return w.SendWebRequest();
            if(w.error != null){
                totalData[i].icon = defaultIcon;
            }
            else{
                if(w.isDone){
                    Texture2D tx = DownloadHandlerTexture.GetContent(w);
                    var sprite = Sprite.Create (tx, new Rect (0f, 0f, tx.width, tx.height),new Vector2(0.5f,0.5f));


                    Debug.Log(totalData[i].icon);
                    totalData[i].icon = sprite;


                    Debug.Log(totalData[i].icon);
                }
            }
        }


        StartCoroutine(GetGameData());
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " +   webRequest.downloadHandler.text);
                    totalData = JsonHelper.GetArray<Data>(webRequest.downloadHandler.text);
                    
                    StartCoroutine(GetGameIcon());
                    break;
            }
        }
    }
}
