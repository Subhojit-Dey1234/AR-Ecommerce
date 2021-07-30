using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProductListAPIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] productList;
    [SerializeField]
    Sprite defaultIcon;
    public int productindex = 0;
    [System.Serializable]
    public class Data
    {
        public int id;
        public string title;
        public string price;
        public string image;
        public Sprite icon;
    }

    Data[] totalData;
    private void Awake()
    {
        StartCoroutine(GetRequest("https://fakestoreapi.com/products"));
        Debug.Log("Api Scripts Working");
    }
    public void ArrowClicked()
    {
        StartCoroutine(GetRequest("https://fakestoreapi.com/products"));
        Debug.Log(productindex);
    }
    IEnumerator GetGameData()
    {

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(totalData[i].title); 
            productList[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = totalData[i+productindex].title;
            productList[i].transform.GetChild(1).GetComponent<Image>().sprite = totalData[i+productindex].icon;
            Debug.Log(totalData[i].icon);
            yield return totalData[i];
        }
    }

    IEnumerator GetGameIcon()
    {
        Debug.Log("Icon");
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(totalData[i].image);
            UnityWebRequest w = UnityWebRequestTexture.GetTexture(totalData[i].image);
            yield return w.SendWebRequest();
            if (w.error != null)
            {
                totalData[i].icon = defaultIcon;
            }
            else
            {
                if (w.isDone)
                {
                    Texture2D tx = DownloadHandlerTexture.GetContent(w);
                    var sprite = Sprite.Create(tx, new Rect(0f, 0f, tx.width, tx.height), new Vector2(0.5f, 0.5f));


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
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    totalData = JsonHelper.GetArray<Data>(webRequest.downloadHandler.text);
                    Debug.Log(totalData);
                    StartCoroutine(GetGameIcon());
                    break;
            }
        }
    }
}
