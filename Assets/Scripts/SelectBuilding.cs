using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class SelectBuilding : MonoBehaviour
{
    public umi3d.edk.UMI3DModel model;
    [SerializeField] GameObject black_room;
    [SerializeField] string ipAddress;
    CreatorAPI creator = new CreatorAPI();

    IEnumerator getBuildings()
    {
        WWW _www = new WWW("http://" + ipAddress + ":1026/ngsi-ld/v1/entities/?type=urn:mytypes:building");
        yield return _www;

        if (_www.error == null)
        {
            var List = JArray.Parse(_www.text);
            JObject[] buildingList = List.ToObject<JObject[]>();
            List<string> buildingIds = new List<string>();
            string[] buildings = new string[buildingList.Length];
            for (int i = 0; i < buildings.Length; i++)
            {
                //buildings[i] = buildingList[i].SelectToken("id");
                //UnityEngine.Debug.Log(buildingList[i].SelectToken("id").Value<string>());
                string str = buildingList[i].SelectToken("id").Value<string>();
                string temp = str.Substring(9);
                buildingIds.Add(temp);
                buildings[i] = temp;
                UnityEngine.Debug.Log(buildings[i]);
            }
        }
        else
        {
            UnityEngine.Debug.Log("Something went wrong");
        }
    }

    public void Select()
    {
        umi3d.edk.Transaction transaction = new umi3d.edk.Transaction();
        transaction.reliable = true;
        transaction.AddIfNotNull(model.objectActive.SetValue(false));
        black_room.SetActive(false);
        StartCoroutine(creator.getFloor("Floor3"));
        transaction.Dispatch();
        Debug.Log("Open");
    }
}
