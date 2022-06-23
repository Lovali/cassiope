using System.Security;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Net.Http;
//using System.Runtime.Intrinsics.X86;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json.Linq;

public class CreatorAPI : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject door;
    [SerializeField] GameObject window;
    [SerializeField] GameObject IOT;
    [SerializeField] GameObject blackRoom;
    [SerializeField] GameObject selectBuildingButton;
    [SerializeField] string ipAddress;
    [SerializeField] string building;
    public string jsonURL;

    private void Start()
    {
        //StartCoroutine(getFloor("Floor3"));
        blackRoomCreator();
    }

    //---------------------------------------------------------START OF THE GAME-------------------------------------------------------------------------//
    void blackRoomCreator(){
        //GameObject floor_br = Instantiate(floor, new Vector3(0,0,0), Quaternion.identity);
        //floor_br.transform.parent = gameObject.transform;
        //floor_br.transform.localScale = new Vector3(1, 1, 1);
        GameObject br = Instantiate(blackRoom, new Vector3(0,-10,0), Quaternion.identity);
        br.transform.parent = gameObject.transform;
        //br.transform.localScale = new Vector3(1, 1, 1);
        //GameObject button = Instantiate(selectBuildingButton, new Vector3(0, -8, 0), Quaternion.identity);
        //button.transform.parent = gameObject.transform;
        //button.transform.localScale = new Vector3(5, 1, 1);
    }


    //--------------------------------------------------------------- DATA API --------------------------------------------------------------------------//
    public IEnumerator getFloor(string floorName){
        // Sending API request
        // http://192.168.98.159:1026/ngsi-ld/v1/entities/Etoile%3AFloor3
            WWW _www = new WWW("http://" + ipAddress + ":1026/ngsi-ld/v1/entities/" + building + ":"+ floorName);

            yield return _www;

            if(_www.error == null){
                UnityEngine.Debug.Log("Successfully entered in Floor");
                var Floor = JObject.Parse(_www.text);
                var nbOfRooms = Floor.SelectToken("numberOfRooms").Value<JObject>();
                int numberOfRooms = nbOfRooms.SelectToken("value").Value<int>();
                var roomsOnFloor = Floor.SelectToken("roomsOnFloor").Value<JObject>();
                object[] arrayRoomsOnFloor = roomsOnFloor.SelectToken("object").Value<JArray>().ToObject<object[]>();
                string[] objRoomsOnFloor = new string[numberOfRooms];
                int k = 0;
                foreach (object room in arrayRoomsOnFloor)
                {
                    objRoomsOnFloor[k] = Convert.ToString(room);
                    k++;
                }
                var location = Floor.SelectToken("location").Value<JObject>();
                var locationValue = location.SelectToken("value").Value<JObject>();
                var heightObject = Floor.SelectToken("height").Value<JObject>();
                var height = heightObject.SelectToken("value").Value<int>();
                object[][] JArrayCoordinates = locationValue.SelectToken("coordinates").Value<JArray>().ToObject<object[][][]>();
                double[][] coordinates = new double[5][];
                coordinates[0] = new double[3];
                coordinates[1] = new double[3];
                coordinates[2] = new double[3];
                coordinates[3] = new double[3];
                coordinates[4] = new double[3];
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
                CreatingFloor(floor.getCoordinates(), floor.getRoomsOnFloor(), floor.getHeight(), floor.getNumberOfRooms());
                IOT iot = IOTParser("Etoile_Floor3_IOT1.json");
                CreatingIOT(iot.getCoordinates(), iot.getHeight());
            } else {
                UnityEngine.Debug.Log("Something went wrong");
            }
    }

    IEnumerator getRoom(string roomName){
        // Sending API request
        // http://192.168.98.159:1026/ngsi-ld/v1/entities/Etoile%3AFloor3
            WWW _www = new WWW("http://" + ipAddress + ":1026/ngsi-ld/v1/entities/"+ roomName);

            yield return _www;

            if(_www.error == null){
                //coordinates = processJsonRoomCoordinates(_www.text);
                var Room = JObject.Parse(_www.text);
                string roomId = Room.SelectToken("id").Value<string>();
                var doors = Room.SelectToken("numbersOfDoors").Value<JObject>();
                int nbDoors = doors.SelectToken("value").Value<int>();
                var windows = Room.SelectToken("numberOfWindows").Value<JObject>();
                int nbWindows = windows.SelectToken("value").Value<int>();
                var location = Room.SelectToken("location").Value<JObject>();
                var locationValue = location.SelectToken("value").Value<JObject>();
                var heightObject = Room.SelectToken("height").Value<JObject>();
                int height = heightObject.SelectToken("value").Value<int>();
                object[][][] JArrayCoordinates = locationValue.SelectToken("coordinates").Value<JArray>().ToObject<object[][][]>();
                double[][] coordinates = new double[5][];
                //setting the size of the tables inside coordinates (x,y,z for each)
                coordinates[0] = new double[3];
                coordinates[1] = new double[3];
                coordinates[2] = new double[3];
                coordinates[3] = new double[3];
                coordinates[4] = new double[3];
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
                Room room = new Room(nbDoors, nbWindows, coordinates, height);
                room.setName(roomId);
                CreatingRoom(room.getCoordinates(), room.getHeight(), room.getNumberOfDoors(), room.getNumberOfWindows(), room.getName());
            } else {
                UnityEngine.Debug.Log("Something went wrong");
            }
    }

    IEnumerator getWindow(string windowName)
    {
    // Sending API request
    // http://192.168.98.159:1026/ngsi-ld/v1/entities/windowName
        WWW _www = new WWW("http://" + ipAddress + ":1026/ngsi-ld/v1/entities/"+ windowName);

        yield return _www;

        if(_www.error == null){
            var Window = JObject.Parse(_www.text);
            var location = Window.SelectToken("location").Value<JObject>();
            var locationValue = location.SelectToken("value").Value<JObject>();
            var heightObject = Window.SelectToken("height").Value<JObject>();
            double height = heightObject.SelectToken("value").Value<double>();
            object[][] JArrayCoordinates = locationValue.SelectToken("coordinates").Value<JArray>().ToObject<object[][]>();
            double[][] coordinates = new double[2][];
            coordinates[0] = new double[3];
            coordinates[1] = new double[3];
            int i = 0;
            int j = 0;
            foreach (object[] firstTab in JArrayCoordinates)
            {
                foreach (object x in firstTab)
                {
                    coordinates[i][j] = Convert.ToDouble(x);
                    j++;
                }
                i++;
                j = 0;
            }
            Window window = new Window(coordinates, height);
            CreatingWindow(window.getCoordinates(), window.getHeight());
        } else {
            UnityEngine.Debug.Log("Something went wrong");
        }
    }
    
    IEnumerator getDoor(string doorName)
    {
    // Sending API request
    // http://192.168.98.159:1026/ngsi-ld/v1/entities/windowName
        WWW _www = new WWW("http://" + ipAddress + ":1026/ngsi-ld/v1/entities/"+ doorName);

        yield return _www;

        if(_www.error == null){
            var Door = JObject.Parse(_www.text);
            var location = Door.SelectToken("location").Value<JObject>();
            var locationValue = location.SelectToken("value").Value<JObject>();
            var heightObject = Door.SelectToken("height").Value<JObject>();
            double height = heightObject.SelectToken("value").Value<double>();
            object[][] JArrayCoordinates = locationValue.SelectToken("coordinates").Value<JArray>().ToObject<object[][]>();
            double[][] coordinates = new double[2][];
            coordinates[0] = new double[3];
            coordinates[1] = new double[3];
            int i = 0;
            int j = 0;
            foreach (object[] firstTab in JArrayCoordinates)
            {
                foreach (object x in firstTab)
                {
                    coordinates[i][j] = Convert.ToDouble(x);
                    j++;
                }
                i++;
                j = 0;
            }
            Door door = new Door(coordinates, height);
            CreatingDoor(door.getCoordinates(), door.getHeight());
        } else {
            UnityEngine.Debug.Log("Something went wrong");
        }
    }

    //--------------------------------------------------------------- PARSERS --------------------------------------------------------------------------//

    public Building BuildingParser()
    {
        var BuildingString = File.ReadAllText("Building.json");
        var Building = JObject.Parse(BuildingString);
        var floorsAboveGround = Building.SelectToken("floorsAboveGround").Value<JObject>();
        int nbFloorsAboveGround = floorsAboveGround.SelectToken("value").Value<int>();
        Building building = new Building(nbFloorsAboveGround);
        return building;
    }

    public IOT IOTParser(String nom_fichier)
    {
        var IOTString = File.ReadAllText(nom_fichier);
        var IOT = JObject.Parse(IOTString);
        var location = IOT.SelectToken("location").Value<JObject>();
        var locationValue = location.SelectToken("value").Value<JObject>();
        var heightObject = IOT.SelectToken("height").Value<JObject>();
        double height = heightObject.SelectToken("value").Value<double>();
        object[][] JArrayCoordinates = locationValue.SelectToken("coordinates").Value<JArray>().ToObject<object[][]>();
        double[] coordinates = new double[3];
        int j = 0;
            foreach (object[] secondTab in JArrayCoordinates)
            {
                foreach (object x in secondTab)
                {
                    coordinates[j] = Convert.ToDouble(x);
                    j++;
                }
            }
        IOT IoT = new IOT(coordinates, height);
        return IoT;
    }

    //------------------------------------------------------------------ CREATORS ---------------------------------------------------------------------//


    public void CreatingFloor(double[][] coordinates, string[] objRoomsOnFloor,int height, int numberOfRooms)
    {
        float width = Mathf.Abs((float)coordinates[1][0] - (float)coordinates[0][0]) / 10;
        float l = Mathf.Abs((float)coordinates[0][1] - (float)coordinates[2][1]) / 10;
        float x = ((float)coordinates[1][0] - (float)coordinates[0][0]) / 2;
        float z = ((float)coordinates[0][1] + (float)coordinates[2][1]) / 2;
        GameObject floor1 = Instantiate(floor, new Vector3(x, 0, z), Quaternion.identity);
        floor1.transform.parent = gameObject.transform;
        floor1.transform.localScale = new Vector3(width, 0.1f, l);
        
        for (int i=0; i<numberOfRooms; i++)
        {
            string str = objRoomsOnFloor[i];
            StartCoroutine(getRoom(str));
        }
    }

    public void CreatingRoom(double[][] coordinates, int height, int nbDoors, int nbWindows, string roomName)
    {
        //height = la hauteur du mur (ici deux unités)
        //heightWall = la hauteur a laquelle on va placer le centre du mur
        float heightWall = ((float)height / 2) + (float)coordinates[0][2];

        //Putting an absolute value to be sure the wall have a positive scale
        float width1 = Mathf.Abs((float)coordinates[1][0] - (float)coordinates[0][0]);
        float x1 = (float)coordinates[0][0] + (width1 / 2);
        float z1 = ((float)coordinates[0][1] + (float)coordinates[1][1]) / 2;
        GameObject wall1 = Instantiate(wall, new Vector3(x1,heightWall, z1), Quaternion.identity);
        wall1.transform.parent = gameObject.transform;

        wall1.transform.localScale = new Vector3(width1, height, 0.1f);

        float width2 = Mathf.Abs((float)coordinates[3][0] - (float)coordinates[2][0]);
        float x2 = (float)coordinates[3][0] + (width2 / 2);
        float z2 = ((float)coordinates[2][1] + (float)coordinates[3][1]) / 2;
        GameObject wall2 = Instantiate(wall, new Vector3(x2, heightWall, z2), Quaternion.identity);
        wall2.transform.parent = gameObject.transform;

        wall2.transform.localScale = new Vector3(width2, height, 0.1f);

        float width3 = Mathf.Abs((float)coordinates[2][1] - (float)coordinates[1][1]);
        float x3 = ((float)coordinates[1][0] + (float)coordinates[2][0]) / 2;
        float z3 = (float)coordinates[2][1] - (((float)coordinates[2][1] - (float)coordinates[1][1]) / 2);
        GameObject wall3 = Instantiate(wall, new Vector3(x3, heightWall, z3), Quaternion.identity);
        wall3.transform.parent = gameObject.transform;

        wall3.transform.localScale = new Vector3(width3, height, 0.1f);
        //Here we must implement another method in case the rooms aren't square
        wall3.transform.Rotate(new Vector3(0, 90, 0));

        float width4 = Mathf.Abs((float)coordinates[3][1] - (float)coordinates[0][1]);
        float x4 = ((float)coordinates[3][0] + (float)coordinates[0][0]) / 2;
        float z4 = (float)coordinates[3][1] - (((float)coordinates[3][1] - (float)coordinates[0][1]) / 2);
        GameObject wall4 = Instantiate(wall, new Vector3(x4, heightWall, z4), Quaternion.identity);
        wall4.transform.parent = gameObject.transform;

        wall4.transform.localScale = new Vector3(width4, height, 0.1f);
        wall4.transform.Rotate(new Vector3(0, 90, 0));


        for(int i=1; i<nbDoors+1; i++)
        {
            string doorName = roomName + ":Door" + i;
            StartCoroutine(getDoor(doorName));
        }

        for(int i=1; i<nbWindows+1; i++)
        {
            string windowName = roomName + ":Window" + i;
            StartCoroutine(getWindow(windowName));
        }

    }

    public void CreatingDoor(double[][] coordinates, double height)
    {
        float heightDoor = ((float)height / 2) + (float)coordinates[0][2];
        if (coordinates[0][1] == coordinates[1][1])
        {
            float width1 = Mathf.Abs((float)coordinates[1][0] - (float)coordinates[0][0]);
            float x1 = Mathf.Min((float)coordinates[0][0],(float)coordinates[1][0]) + (width1 / 2);
            GameObject door1 = Instantiate(door, new Vector3(x1, heightDoor, (float)coordinates[0][1]), Quaternion.identity);
            door1.transform.parent = gameObject.transform;
            door1.transform.localScale = new Vector3(width1, (float)height, 0.2f);
        } else
        {
            float width2 = Mathf.Abs((float)coordinates[1][1] - (float)coordinates[0][1]);
            float z2 = Mathf.Max((float)coordinates[1][1],(float)coordinates[0][1]) - (Mathf.Abs((float)coordinates[1][1] - (float)coordinates[0][1]) / 2);
            GameObject door1 = Instantiate(door, new Vector3((float)coordinates[0][0], heightDoor, z2), Quaternion.identity);
            door1.transform.parent = gameObject.transform;
            door1.transform.localScale = new Vector3(width2, (float)height, 0.2f);
            door1.transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    public void CreatingWindow(double[][] coordinates, double height)
    {
        float widthX = Mathf.Abs((float)coordinates[0][0] - (float)coordinates[1][0]);
        //si les deux coordonnes sont egales, ie leur difference vaut zéro, la fenetre est sur un mur orienté selon y
        if (widthX == 0)
        {
            float widthY = Mathf.Abs((float)coordinates[0][1] - (float)coordinates[1][1]);
            float y = Mathf.Max((float)coordinates[1][1], (float)coordinates[0][1]) - (widthY / 2);
            float heightAboveGround = (float)coordinates[0][2] + ((float)height / 2);

            GameObject window1 = Instantiate(window, new Vector3((float)coordinates[0][0], heightAboveGround, y), Quaternion.identity);
            window1.transform.parent = gameObject.transform;
            window1.transform.localScale = new Vector3(widthY, (float)height, 0.2f);
            window1.transform.Rotate(new Vector3(0, 90, 0));

        }

        else
        {
            float x = Mathf.Max((float)coordinates[1][0],(float)coordinates[0][0]) - (widthX / 2);
            float heightAboveGround = (float)coordinates[0][2] + ((float)height / 2);
            GameObject window1 = Instantiate(window, new Vector3(x, heightAboveGround, (float)coordinates[0][1]), Quaternion.identity);
            window1.transform.parent = gameObject.transform;
            window1.transform.localScale = new Vector3(widthX, (float)height, 0.2f);

        }
    }

    public void CreatingIOT(double[] coordinates, double height)
    {
        GameObject iot = Instantiate(IOT, new Vector3((float)coordinates[0], (float)coordinates[2], (float)coordinates[1]), Quaternion.identity);
        iot.transform.parent = gameObject.transform;
        iot.transform.localScale = new Vector3((float)height, (float)height, (float)height);
    }
}
