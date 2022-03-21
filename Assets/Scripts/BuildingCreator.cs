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
        float heightWall = (float)height / 2;
        Debug.Log("height1: " + heightWall);

        //Putting an absolute value to be sure the wall have a positive scale
        float width1 = Mathf.Abs((float)coordinates[1][0] - (float)coordinates[0][0]);
        float x1 = width1 / 2;
        float z1 = ((float)coordinates[0][1] + (float)coordinates[1][1]) / 2;
        GameObject wall1 = Instantiate(wall, new Vector3(x1,heightWall, z1), Quaternion.identity);
        Debug.Log("width1: " + width1);
        Debug.Log("x1: " + x1);
        Debug.Log("z1: " + z1);
        wall1.transform.localScale = new Vector3(width1, height, 0.1f);

        float width2 = Mathf.Abs((float)coordinates[3][0] - (float)coordinates[2][0]);
        float x2 = width2 / 2;
        float z2 = ((float)coordinates[2][1] + (float)coordinates[3][1]) / 2;
        GameObject wall2 = Instantiate(wall, new Vector3(x2, heightWall, z2), Quaternion.identity);
        Debug.Log("width2: " + width2);
        Debug.Log("x2: " + x2);
        Debug.Log("z2: " + z2);
        wall2.transform.localScale = new Vector3(width2, height, 0.1f);

        float width3 = Mathf.Abs((float)coordinates[2][1] - (float)coordinates[1][1]);
        float x3 = ((float)coordinates[1][0] + (float)coordinates[2][0]) / 2;
        float z3 = (float)coordinates[2][1] - (((float)coordinates[2][1] - (float)coordinates[1][1]) / 2);
        GameObject wall3 = Instantiate(wall, new Vector3(x3, heightWall, z3), Quaternion.identity);
        Debug.Log("width3: " + width3);
        Debug.Log("x3: " + x3);
        Debug.Log("z3: " + z3);
        wall3.transform.localScale = new Vector3(width3, height, 0.1f);
        //Here wwe must implement another method in case the rooms aren't square
        wall3.transform.Rotate(new Vector3(0, 90, 0));
        //wall3.transform.localRotation = new Vector3.Vector3(0, 90, 0);

        float width4 = Mathf.Abs((float)coordinates[3][1] - (float)coordinates[0][1]);
        float x4 = ((float)coordinates[3][0] + (float)coordinates[0][0]) / 2;
        float z4 = (float)coordinates[3][1] - (((float)coordinates[3][1] - (float)coordinates[0][1]) / 2);
        GameObject wall4 = Instantiate(wall, new Vector3(x4, heightWall, z4), Quaternion.identity);
        Debug.Log("width4: " + width4);
        Debug.Log("x4: " + x4);
        Debug.Log("z4: " + z4);
        wall4.transform.localScale = new Vector3(width4, height, 0.1f);
        wall4.transform.Rotate(new Vector3(0, 90, 0));

    }
}
