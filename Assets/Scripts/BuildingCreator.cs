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
    [SerializeField] GameObject door;
    [SerializeField] GameObject window;
    [SerializeField] GameObject IOT;

    private void Start()
    {
        /*Building building = BuildingParser();
        int n = building.getnbFloorsAboveGround();
        for(int i=1; i<n+1; i++)
        {
            String name = "Floor" + i + ".json";
            Floor floor = FloorParser(name);
            CreatingFloor(floor.getCoordinates(), floor.getRoomsOnFloor(),floor.getHeight(), floor.getNumberOfRooms());
        }*/
        Floor floor = FloorParser("Etoile_Floor3.json");
        CreatingFloor(floor.getCoordinates(), floor.getRoomsOnFloor(), floor.getHeight(), floor.getNumberOfRooms());
        IOT iot = IOTParser("Etoile_Floor3_IOT1.json");
        CreatingIOT(iot.getCoordinates(), iot.getHeight());

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

    public Floor FloorParser(String nom_fichier)
    {
        var FloorString = File.ReadAllText(nom_fichier);
        var Floor = JObject.Parse(FloorString);
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
        return floor;
    }

    public Room RoomParser(String nom_fichier)
    {
        var RoomString = File.ReadAllText(nom_fichier);
        var Room = JObject.Parse(RoomString);
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
        return room;
    }


    public Door DoorParser(String nom_fichier)
    {
        var DoorString = File.ReadAllText(nom_fichier);
        var Door = JObject.Parse(DoorString);
        var location = Door.SelectToken("location").Value<JObject>();
        var locationValue = location.SelectToken("value").Value<JObject>();
        var heightObject = Door.SelectToken("height").Value<JObject>();
        double height = heightObject.SelectToken("value").Value<double>();
        object[][][] JArrayCoordinates = locationValue.SelectToken("coordinates").Value<JArray>().ToObject<object[][][]>();
        double[][] coordinates = new double[2][];
        coordinates[0] = new double[3];
        coordinates[1] = new double[3];
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
        Door door = new Door(coordinates, height);
        return door;
    }

    public Window WindowParser(String nom_fichier)
    {
        var WindowString = File.ReadAllText(nom_fichier);
        var Window = JObject.Parse(WindowString);
        var location = Window.SelectToken("location").Value<JObject>();
        var locationValue = location.SelectToken("value").Value<JObject>();
        var heightObject = Window.SelectToken("height").Value<JObject>();
        double height = heightObject.SelectToken("value").Value<double>();
        object[][][] JArrayCoordinates = locationValue.SelectToken("coordinates").Value<JArray>().ToObject<object[][][]>();
        double[][] coordinates = new double[2][];
        coordinates[0] = new double[3];
        coordinates[1] = new double[3];
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
        Window window = new Window(coordinates, height);
        return window;
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
        Debug.Log("In creating floor method");
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
            int str_length = str.Length;
            Debug.Log("length of the name string : " + str_length);
            string temp = str.Substring(str_length - 8);
            Debug.Log("temporary string : " + temp);
            string name = "Etoile_Floor3_" + temp + ".json";
            Debug.Log("name string : " + name);
            if (str_length == 14)
            {
                Debug.Log("plop");
                temp = str.Substring(str_length - 7);
                Debug.Log("temporary string : " + temp);
                name = "Etoile_Floor3_" + temp + ".json";
                Debug.Log("name string : " + name);
            }
            Room room = RoomParser(name);
            room.setName(temp);
            Debug.Log("name of the room : " + room.getName());
            CreatingRoom(room.getCoordinates(), room.getHeight(), room.getNumberOfDoors(), room.getNumberOfWindows(), room.getName());
        }
    }

    public void CreatingRoom(double[][] coordinates, int height, int nbDoors, int nbWindows, string roomName)
    {
        //height = la hauteur du mur (ici deux unités)
        //heightWall = la hauteur a laquelle on va placer le centre du mur
        float heightWall = ((float)height / 2) + (float)coordinates[0][2];
        //Debug.Log("heightWall: " + heightWall);

        //Putting an absolute value to be sure the wall have a positive scale
        float width1 = Mathf.Abs((float)coordinates[1][0] - (float)coordinates[0][0]);
        float x1 = (float)coordinates[0][0] + (width1 / 2);
        float z1 = ((float)coordinates[0][1] + (float)coordinates[1][1]) / 2;
        GameObject wall1 = Instantiate(wall, new Vector3(x1,heightWall, z1), Quaternion.identity);
        wall1.transform.parent = gameObject.transform;
        //Debug.Log("width1: " + width1);
        //Debug.Log("x1: " + x1);
        //Debug.Log("z1: " + z1);
        wall1.transform.localScale = new Vector3(width1, height, 0.1f);

        float width2 = Mathf.Abs((float)coordinates[3][0] - (float)coordinates[2][0]);
        float x2 = (float)coordinates[3][0] + (width2 / 2);
        float z2 = ((float)coordinates[2][1] + (float)coordinates[3][1]) / 2;
        GameObject wall2 = Instantiate(wall, new Vector3(x2, heightWall, z2), Quaternion.identity);
        wall2.transform.parent = gameObject.transform;
        //Debug.Log("width2: " + width2);
        //Debug.Log("x2: " + x2);
        //Debug.Log("z2: " + z2);
        wall2.transform.localScale = new Vector3(width2, height, 0.1f);

        float width3 = Mathf.Abs((float)coordinates[2][1] - (float)coordinates[1][1]);
        float x3 = ((float)coordinates[1][0] + (float)coordinates[2][0]) / 2;
        float z3 = (float)coordinates[2][1] - (((float)coordinates[2][1] - (float)coordinates[1][1]) / 2);
        GameObject wall3 = Instantiate(wall, new Vector3(x3, heightWall, z3), Quaternion.identity);
        wall3.transform.parent = gameObject.transform;
        //Debug.Log("width3: " + width3);
        //Debug.Log("x3: " + x3);
        //Debug.Log("z3: " + z3);
        wall3.transform.localScale = new Vector3(width3, height, 0.1f);
        //Here we must implement another method in case the rooms aren't square
        wall3.transform.Rotate(new Vector3(0, 90, 0));
        //wall3.transform.localRotation = new Vector3.Vector3(0, 90, 0);

        float width4 = Mathf.Abs((float)coordinates[3][1] - (float)coordinates[0][1]);
        float x4 = ((float)coordinates[3][0] + (float)coordinates[0][0]) / 2;
        float z4 = (float)coordinates[3][1] - (((float)coordinates[3][1] - (float)coordinates[0][1]) / 2);
        GameObject wall4 = Instantiate(wall, new Vector3(x4, heightWall, z4), Quaternion.identity);
        wall4.transform.parent = gameObject.transform;
        //Debug.Log("width4: " + width4);
        //Debug.Log("x4: " + x4);
        //Debug.Log("z4: " + z4);
        wall4.transform.localScale = new Vector3(width4, height, 0.1f);
        wall4.transform.Rotate(new Vector3(0, 90, 0));


        for(int i=1; i<nbDoors+1; i++)
        {
            string doorName = "Etoile_Floor3_" + roomName + "_Door" + i + ".json";
            Debug.Log("door name : " + doorName);
            Door door = DoorParser(doorName);
            CreatingDoor(door.getCoordinates(), door.getHeight());
        }

        for(int i=1; i<nbWindows+1; i++)
        {
            string windowName = "Etoile_Floor3_" + roomName + "_Window" + i + ".json";
            Debug.Log("window name : " + windowName);
            Window window = WindowParser(windowName);
            CreatingWindow(window.getCoordinates(), window.getHeight());
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
            //Debug.Log("coord z: " + (float)coordinates[0][2]);
            //Debug.Log("height: " + height);
            //Debug.Log("height above ground: " + heightAboveGround);
            //Debug.Log("widthY: " + widthY);
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
