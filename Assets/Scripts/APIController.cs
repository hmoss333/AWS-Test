using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class APIController : MonoBehaviour
{
    public static APIController instance;


    private string url = "https://493joif4kf.execute-api.us-east-2.amazonaws.com/items";

    public DBObject currentObject { get; private set; }
    [SerializeField] DBObjectContainer dbObjects;

    Coroutine currentRoutine;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", 0);
        form.AddField("name", "test");
    }

    public void GetEntries()
    {
        if (currentRoutine == null)
            currentRoutine = StartCoroutine(GetAllEntriesRoutine());
    }

    public void GetEntry(string id)
    {
        if (currentRoutine == null)
            currentRoutine = StartCoroutine(GetEntryRoutine(id));
    }

    public void PutEntry(string id, string name)
    {
        if (currentRoutine == null)
            currentRoutine = StartCoroutine(PutEntryRoutine(id, name));
    }

    public void DeleteEntry(string id)
    {
        if (currentRoutine == null)
            currentRoutine = StartCoroutine(DeleteEntryRoutine(id));
    }

    IEnumerator GetAllEntriesRoutine()
    {
        var request = UnityWebRequest.Get(url);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        using (request)
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log($"Unable to locate database at url: {url}");
                Debug.Log(request.error);
            }
            else
            {
                string jsonWrap = "{\"objectCollection\":" + request.downloadHandler.text + "}";
                DBObjectContainer data = JsonUtility.FromJson<DBObjectContainer>(jsonWrap);
                dbObjects = data;
            }
        }

        currentRoutine = null;
    }

    IEnumerator GetEntryRoutine(string id)
    {
        string uri = $"{url}/{id}";

        var request = UnityWebRequest.Get(uri);
        using (request)
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.downloadHandler.text == String.Empty)
            {
                Debug.Log($"Unable to locate database object with ID: {id}");
                Debug.Log(request.error);
                UIController.instance.UpdateLogText($"Unable to locate database object with ID: {id}");
            }
            else
            {
                print(request.downloadHandler.text);
                DBObject data = JsonUtility.FromJson<DBObject>(request.downloadHandler.text);
                currentObject = data;

                UIController.instance.UpdateSearchResuts(currentObject.id, currentObject.name);
                UIController.instance.UpdateLogText($"ID: {data.id}, Name: {data.name}");
            }
        }

        currentRoutine = null;
    }

    IEnumerator PutEntryRoutine(string id, string name)
    {
        DBObject tempObject = new DBObject();
        tempObject.id = id;
        tempObject.name = name;

        string result = JsonUtility.ToJson(tempObject);
        print(result);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(result);

        var request = UnityWebRequest.Put(url, bodyRaw);
        request.SetRequestHeader("Content-Type", "application/json");
        using (request)
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log($"Unable to locate database at url: {url}");
                Debug.Log(request.error);

                UIController.instance.UpdateLogText($"Unable to locate database at url: {url}");
            }
            else
            {
                Debug.Log($"Added database object with ID: {id}");
                UIController.instance.UpdateLogText($"Added ID: {id}, Name: {name}");
            }
        }

        currentRoutine = null;
    }

    IEnumerator DeleteEntryRoutine(string id)
    {
        string uri = $"{url}/{id}";

        var request = UnityWebRequest.Delete(uri);
        using (request)
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log($"Unable to locate database object with ID: {id}");
                Debug.Log(request.error);
                UIController.instance.UpdateLogText($"Unable to locate database object with ID: {id}");
            }
            else
            {
                Debug.Log($"Deleted database object with ID: {id}");
                UIController.instance.UpdateLogText($"Deleted database object with ID: {id}");
            }
        }

        currentRoutine = null;
    }
}

[Serializable]
public class DBObject
{
    public string id;
    public string name;
}

[Serializable]
public class DBObjectContainer
{
    public DBObject[] objectCollection;
}
