using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class testFloorCreator : MonoBehaviour
{
    string myPath = "Floor1.json";



    // Start is called before the first frame update
    void Start()
    {
        //testFloor floor1 = JsonConvert.DeserializeObject<testFloor>("Floor1.json");
        testFloor floor1 = JsonConvert.DeserializeObject<testFloor>(File.ReadAllText(myPath));
        Debug.Log("floor id: " + floor1.id);
        Debug.Log("floor type: " + floor1.type);
        Debug.Log("number of rooms list: " + floor1.numberOfRoom);
        Debug.Log("nb of room value: " + floor1.numberOfRoom[1]);
    }

}
