using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using Newtonsoft.Json.Linq;

public class JSONFloorParser {

    static void Main(string[] args)
    {
        var FloorString = File.ReadAllText(args[1]);
        var Floor = JObject.Parse(FloorString);
        var roomsOnFloor = Floor.SelectToken("roomsOnFloor").Value<JObject>();
        int numberOfRooms = Floor.SelectToken("numberOfRooms").Value<int>();
        
        Building building = new Building(nbFloorsAboveGround);
    }
}