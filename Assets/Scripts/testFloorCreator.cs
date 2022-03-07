using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testFloorCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        testFloor floor = JsonConvert.DeserializeObject<testFloor>("Room1.json");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
