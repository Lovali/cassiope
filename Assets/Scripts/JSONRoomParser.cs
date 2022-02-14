using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using Newtonsoft.Json.Linq;

public class JSONRoomParser
{
    static void Parser(string[] args)
    {
        var RoomString = File.ReadAllText("Room1.json");
        var Room = JObject.Parse(RoomString);
        var doors = Room.SelectToken("numbersOfDoors").Value<JObject>();
        int nbDoors = doors.SelectToken("value").Value<int>();
        var windows = Room.SelectToken("numberOfWindows").Value<JObject>();
        int nbWindows = windows.SelectToken("value").Value<int>();
        var location = Room.SelectToken("location").Value<JObject>();
        var locationValue = location.SelectToken("value").Value<JObject>();
        int[][] coordinates = locationValue.SelectToken("coordinates").Value<int[][]>();
        Room room = new Room(nbDoors, nbWindows, coordinates);
    }
}