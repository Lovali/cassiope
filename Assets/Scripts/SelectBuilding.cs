using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class SelectBuilding : MonoBehaviour
{
    public umi3d.edk.UMI3DModel model;
    [SerializeField] umi3d.edk.UMI3DNode floor_br_node;
    [SerializeField] umi3d.edk.UMI3DNode wall1_br_node;
    [SerializeField] umi3d.edk.UMI3DNode wall2_br_node;
    [SerializeField] umi3d.edk.UMI3DNode wall3_br_node;
    [SerializeField] umi3d.edk.UMI3DNode wall4_br_node;
    [SerializeField] umi3d.edk.UMI3DNode ceil_br_node;
    [SerializeField] string ipAddress;

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
        transaction.AddIfNotNull(floor_br_node.objectActive.SetValue(false));
        transaction.AddIfNotNull(wall1_br_node.objectActive.SetValue(false));
        transaction.AddIfNotNull(wall2_br_node.objectActive.SetValue(false));
        transaction.AddIfNotNull(wall3_br_node.objectActive.SetValue(false));
        transaction.AddIfNotNull(wall4_br_node.objectActive.SetValue(false));
        transaction.AddIfNotNull(ceil_br_node.objectActive.SetValue(false));
        StartCoroutine(getBuildings());
        transaction.Dispatch();
        Debug.Log("Open");
    }
}
