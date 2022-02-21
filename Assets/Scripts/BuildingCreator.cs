using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BuildingCreator : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject floor;

    private void Start()
    {
        Room room = roomParser();
        create(room);

    }

    public Room roomParser()
    {
        var RoomString = File.ReadAllText("Room1.json");
        Room room = JsonUtility.FromJson<Room>(RoomString);
        //var doors = Room.SelectToken("numbersOfDoors").Value<JObject>();
        //int nbDoors = doors.SelectToken("value").Value<int>();
        //var windows = Room.SelectToken("numberOfWindows").Value<JObject>();
        //int nbWindows = windows.SelectToken("value").Value<int>();
        //var location = Room.SelectToken("location").Value<JObject>();
        //var locationValue = location.SelectToken("value").Value<JObject>();
        //int[][] coordinates = locationValue.SelectToken("coordinates").Value<int[][]>();
        //Room room = new Room(nbDoors, nbWindows, coordinates);
        return room;
    }

    public void create(Room room)
    {
        int i;
        int[][][] coordinates = room.getCoordinates();
        for (i=0; i<4; i++)
        {
            Debug.Log("coordinates :" + coordinates);
            Debug.Log("coucou");
        }
        Debug.Log(room.getNumberOfDoors());
    }


}
