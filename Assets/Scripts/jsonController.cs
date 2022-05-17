using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jsonController : MonoBehaviour
{
    public string jsonURL;

    void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        WWW _www = new WWW(jsonURL);
        yield return _www;

        if (_www.error == null)
        {
            processJsonData(_www.text);
        }
        else
        {
            UnityEngine.Debug.Log("Something went wrong");
        }
    }

    private void processJsonData(string _url)
    {
        // Floor floor = JsonUtility.FromJson<Floor>(_url);
        jsonRoom room = JsonUtility.FromJson<jsonRoom>(_url);
        // jsonDataClass jsnData = JsonUtility.FromJson<jsonDataClass>(_url);
        // UnityEngine.Debug.Log(jsnData.playerName);
        // UnityEngine.Debug.Log(jsnData.balls[0].name);
        // UnityEngine.Debug.Log(floor.getHeight());
        UnityEngine.Debug.Log(room.id);
        UnityEngine.Debug.Log(room.type);
        UnityEngine.Debug.Log(room.Location.value.coordinates);
    }
}