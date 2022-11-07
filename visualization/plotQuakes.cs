/* Example of reading earthquake data from the USGS data feed.
   This script requests GEOJson formatted data, then parses it and prints some of the information.
   It requires the JSONObject class for Unity, which can be downloaded from https://github.com/mtschoen/JSONObject
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using JSONObject = Defective.JSON.JSONObject;
using TMPro;

public class plotQuakes : MonoBehaviour
{
    [SerializeField] private string url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/4.5_day.geojson";
    [SerializeField] private GameObject prefab;
    [SerializeField] private float radius = 1.0f;
    [SerializeField] private TextMeshProUGUI textGui;

    void Start()
        {
        StartCoroutine(GetData(url));
        }
    
    IEnumerator GetData(string url)
        {
        Debug.Log($"sending request {url}");
        UnityWebRequest req = UnityWebRequest.Get(url);
        yield return req.SendWebRequest();
        if (req.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log($"Error ({url}): {req.error}");
        else
            {
            JSONObject data = new JSONObject(req.downloadHandler.text);
            for (int i=0; i < data["metadata"]["count"].longValue; i++)
                {
                float lat = data["features"][i]["geometry"]["coordinates"][1].floatValue;
                float lon = data["features"][i]["geometry"]["coordinates"][0].floatValue;
                float mag = data["features"][i]["properties"]["mag"].floatValue;
                Debug.Log($"lat: {lat}  lon: {lon}   mag: {mag}");
                GameObject marker = Instantiate(prefab, transform);
                lon *= Mathf.Deg2Rad;
                lat *= Mathf.Deg2Rad;
                Vector3 pos = new Vector3(Mathf.Cos(lon)*Mathf.Cos(lat)*radius, Mathf.Sin(lat)*radius, Mathf.Sin(lon)*Mathf.Cos(lat)*radius);
                marker.transform.localPosition = pos;
                float size = mag/10.0f;
                marker.transform.localScale = new Vector3(size,size,size);
                //marker.AddComponent<quakeData>();
                quakeData markerData = marker.GetComponent<quakeData>();
                if (markerData)
                    {
                    markerData.title = data["features"][i]["properties"]["title"].stringValue;
                    markerData.textGui = textGui;
                    }
                }
            }
        }
}
