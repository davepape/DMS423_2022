/* cookie-clicker script that sends messages to a web server each time the object is clicked, and puts the returned data into a text UI object */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using JSONObject = Defective.JSON.JSONObject;
using TMPro;

public class webclick : MonoBehaviour
{
    [SerializeField] private string url = "http://example.com";
    [SerializeField] private TextMeshProUGUI textGui;

    void Start()
    {
        textGui.text = "";
    }

    void OnMouseDown()
    {
        StartCoroutine(DoScaling());
        StartCoroutine(SendClick());
    }

    IEnumerator DoScaling()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = Vector3.one;
    }

    IEnumerator SendClick()
    {
        WWWForm form = new WWWForm();
        form.AddField("click","1");
        UnityWebRequest req = UnityWebRequest.Post(url, form);
        yield return req.SendWebRequest();
        if (req.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log($"Error ({url}): {req.error}");
        else
            {
            JSONObject data = new JSONObject(req.downloadHandler.text);
            int clicks = data["clicks"].intValue;
            print(data);
            textGui.text = $"{clicks} clicks";
            }
    }
}
