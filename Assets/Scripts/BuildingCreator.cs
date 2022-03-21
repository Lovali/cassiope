using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json.Linq;

public class BuildingCreator : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject floor;

    private void Start()
    {
        Building building = BuildingParser();
        int n = building.getnbFloorsAboveGround();
        //for (int i=0; i<n; i++)
        //{
            //Floor floor = FloorParser();
        //}
        Room room = RoomParser();
        CreatingRoom(room.getCoordinates(), room.getHeight());
    }

    public Room RoomParser()
    {
        var RoomString = File.ReadAllText("Room1.json");
        var Room = JObject.Parse(RoomString);
        var doors = Room.SelectToken("numbersOfDoors").Value<JObject>();
        int nbDoors = doors.SelectToken("value").Value<int>();
        var windows = Room.SelectToken("numberOfWindows").Value<JObject>();
        int nbWindows = windows.SelectToken("value").Value<int>();
        var location = Room.SelectToken("location").Value<JObject>();
        var locationValue = location.SelectToken("value").Value<JObject>();
        int height = locationValue.SelectToken("height").Value<int>();
        object[][] JArrayCoordinates = locationValue.SelectToken("coordinates").Value<JArray>().ToObject<object[][][]>();
        double[][] coordinates = new double[4][];
        //setting the size of the tables inside coordinates (x,y,z for each)
        coordinates[0] = new double[3];
        coordinates[1] = new double[3];
        coordinates[2] = new double[3];
        coordinates[3] = new double[3];
        int i = 0;
        int j = 0;
        foreach (object[][] firstTab in JArrayCoordinates)
        {
            foreach (object[] secondTab in firstTab)
            {
                foreach (object x in secondTab)
                {
                    coordinates[i][j] = Convert.ToDouble(x);
                    //Debug.Log("i : " + i);
                    //Debug.Log("j : " + j);
                    //Debug.Log(coordinates[i][j]);
                    j++;
                }
                i++;
                j = 0;
            }
        }
        Room room = new Room(nbDoors, nbWindows, coordinates, height);
        return room;
    }

    public Floor FloorParser()
    {
        var FloorString = File.ReadAllText("Floor1.json");
        var Floor = JObject.Parse(FloorString);
        var nbOfRooms = Floor.SelectToken("numberOfRooms").Value<JObject>();
        int numberOfRooms = nbOfRooms.SelectToken("value").Value<int>();
        var roomsOnFloor = Floor.SelectToken("roomsOnFloor").Value<JObject>();
        int[] objRoomsOnFloor = roomsOnFloor.SelectToken("object").Value<int[]>();
        var location = Floor.SelectToken("location").Value<JObject>();
        var locationValue = location.SelectToken("value").Value<JObject>();
        int height = locationValue.SelectToken("height").Value<int>();
        object[][] JArrayCoordinates = locationValue.SelectToken("coordinates").Value<JArray>().ToObject<object[][][]>();
        double[][] coordinates = new double[4][];
        coordinates[0] = new double[3];
        coordinates[1] = new double[3];
        coordinates[2] = new double[3];
        coordinates[3] = new double[3];
        int i = 0;
        int j = 0;
        foreach (object[][] firstTab in JArrayCoordinates)
        {
            foreach (object[] secondTab in firstTab)
            {
                foreach (object x in secondTab)
                {
                    coordinates[i][j] = Convert.ToDouble(x);
                    j++;
                }
                i++;
                j = 0;
            }
        }
        Floor floor = new Floor(numberOfRooms, objRoomsOnFloor, coordinates, height);
        return floor;
    }

    public Building BuildingParser()
    {
        var BuildingString = File.ReadAllText("Building.json");
        var Building = JObject.Parse(BuildingString);
        var floorsAboveGround = Building.SelectToken("floorsAboveGround").Value<JObject>();
        int nbFloorsAboveGround = floorsAboveGround.SelectToken("value").Value<int>();
        Building building = new Building(nbFloorsAboveGround);
        return building;
    }

    public void CreatingFloor()
    {

    }

    public void CreatingRoom(double[][] coordinates, int height)
    {
        float height1 = (float)height / 2;
        //Putting an absolute value to be sure the wall have a positive scale
        float width1 = Mathf.Abs((float)coordinates[1][0] - (float)coordinates[0][0]);
        float x1 = width1 / 2;
        float z1 = ((float)coordinates[0][1] + (float)coordinates[1][1]) / 2;
        GameObject wall1 = Instantiate(wall, new Vector3(x1,height1, z1), Quaternion.identity);
        Debug.Log("width1: " + width1);
        Debug.Log("height1: " + height1);
        Debug.Log("x1: " + x1);
        wall1.transform.localScale = new Vector3(width1, height, 1.0f);

    }
}
