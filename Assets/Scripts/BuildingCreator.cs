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
        /*Room room1 = RoomParser("Etoile_Floor3_RoomASC.json");
        CreatingRoom(room1.getCoordinates(), room1.getHeight(), room1.getNumberOfDoors(), room1.getNumberOfWindows());
        Room room2 = RoomParser("Etoile_Floor3_RoomH300.json");
        CreatingRoom(room2.getCoordinates(), room2.getHeight(), room2.getNumberOfDoors(), room2.getNumberOfWindows());
        Room room3 = RoomParser("Etoile_Floor3_RoomH301.json");
        CreatingRoom(room3.getCoordinates(), room3.getHeight(), room3.getNumberOfDoors(), room3.getNumberOfWindows());
        Room room4 = RoomParser("Etoile_Floor3_RoomH310.json");
        CreatingRoom(room4.getCoordinates(), room4.getHeight(), room4.getNumberOfDoors(), room4.getNumberOfWindows());
        Room room5 = RoomParser("Etoile_Floor3_RoomH311.json");
        CreatingRoom(room5.getCoordinates(), room5.getHeight(), room5.getNumberOfDoors(), room5.getNumberOfWindows());
        Room room6 = RoomParser("Etoile_Floor3_RoomH312.json");
        CreatingRoom(room6.getCoordinates(), room6.getHeight(), room6.getNumberOfDoors(), room6.getNumberOfWindows());
        Room room7 = RoomParser("Etoile_Floor3_RoomH313.json");
        CreatingRoom(room7.getCoordinates(), room7.getHeight(), room7.getNumberOfDoors(), room7.getNumberOfWindows());
        Room room8 = RoomParser("Etoile_Floor3_RoomH314.json");
        CreatingRoom(room8.getCoordinates(), room8.getHeight(), room8.getNumberOfDoors(), room8.getNumberOfWindows());
        Room room9 = RoomParser("Etoile_Floor3_RoomH315.json");
        CreatingRoom(room9.getCoordinates(), room9.getHeight(), room9.getNumberOfDoors(), room9.getNumberOfWindows());
        Room room10 = RoomParser("Etoile_Floor3_RoomH316.json");
        CreatingRoom(room10.getCoordinates(), room10.getHeight(), room10.getNumberOfDoors(), room10.getNumberOfWindows());
        Room room11 = RoomParser("Etoile_Floor3_RoomH317.json");
        CreatingRoom(room11.getCoordinates(), room11.getHeight(), room11.getNumberOfDoors(), room11.getNumberOfWindows());
        Room room12 = RoomParser("Etoile_Floor3_RoomH320.json");
        CreatingRoom(room12.getCoordinates(), room12.getHeight(), room12.getNumberOfDoors(), room12.getNumberOfWindows());
        Room room13 = RoomParser("Etoile_Floor3_RoomH323.json");
        CreatingRoom(room13.getCoordinates(), room13.getHeight(), room13.getNumberOfDoors(), room13.getNumberOfWindows());
        Room room14 = RoomParser("Etoile_Floor3_RoomH324.json");
        CreatingRoom(room14.getCoordinates(), room14.getHeight(), room14.getNumberOfDoors(), room14.getNumberOfWindows());
        Room room15 = RoomParser("Etoile_Floor3_RoomH325.json");
        CreatingRoom(room15.getCoordinates(), room15.getHeight(), room15.getNumberOfDoors(), room15.getNumberOfWindows());
        Room room16 = RoomParser("Etoile_Floor3_RoomH326.json");
        CreatingRoom(room16.getCoordinates(), room16.getHeight(), room16.getNumberOfDoors(), room16.getNumberOfWindows());
        Room room17 = RoomParser("Etoile_Floor3_RoomH327.json");
        CreatingRoom(room17.getCoordinates(), room17.getHeight(), room17.getNumberOfDoors(), room17.getNumberOfWindows());
        Door door1 = DoorParser("Etoile_Floor3_RoomH300_Door1.json");
        CreatingDoor(door1.getCoordinates(), door1.getHeight());
        Door door2 = DoorParser("Etoile_Floor3_RoomH301_Door1.json");
        CreatingDoor(door2.getCoordinates(), door2.getHeight());
        Door door3 = DoorParser("Etoile_Floor3_RoomH310_Door1.json");
        CreatingDoor(door3.getCoordinates(), door3.getHeight());
        Door door4 = DoorParser("Etoile_Floor3_RoomH311_Door1.json");
        CreatingDoor(door4.getCoordinates(), door4.getHeight());
        Door door5 = DoorParser("Etoile_Floor3_RoomH312_Door1.json");
        CreatingDoor(door5.getCoordinates(), door5.getHeight());
        Door door6 = DoorParser("Etoile_Floor3_RoomH313_Door1.json");
        CreatingDoor(door6.getCoordinates(), door6.getHeight());
        Door door7 = DoorParser("Etoile_Floor3_RoomH314_Door1.json");
        CreatingDoor(door7.getCoordinates(), door7.getHeight());
        Door door8 = DoorParser("Etoile_Floor3_RoomH315_Door1.json");
        CreatingDoor(door8.getCoordinates(), door8.getHeight());
        Door door9 = DoorParser("Etoile_Floor3_RoomH316_Door1.json");
        CreatingDoor(door9.getCoordinates(), door9.getHeight());
        Door door10 = DoorParser("Etoile_Floor3_RoomH317_Door1.json");
        CreatingDoor(door10.getCoordinates(), door10.getHeight());
        Door door11 = DoorParser("Etoile_Floor3_RoomH320_Door1.json");
        CreatingDoor(door11.getCoordinates(), door11.getHeight());
        Door door12 = DoorParser("Etoile_Floor3_RoomH323_Door1.json");
        CreatingDoor(door12.getCoordinates(), door12.getHeight());
        Door door13 = DoorParser("Etoile_Floor3_RoomH324_Door1.json");
        CreatingDoor(door13.getCoordinates(), door13.getHeight());
        Door door14 = DoorParser("Etoile_Floor3_RoomH325_Door1.json");
        CreatingDoor(door14.getCoordinates(), door14.getHeight());
        Door door15 = DoorParser("Etoile_Floor3_RoomH326_Door1.json");
        CreatingDoor(door15.getCoordinates(), door15.getHeight());
        Door door16 = DoorParser("Etoile_Floor3_RoomH327_Door1.json");
        CreatingDoor(door16.getCoordinates(), door16.getHeight());
        Door door17 = DoorParser("Etoile_Floor3_RoomH327_Door2.json");
        CreatingDoor(door17.getCoordinates(), door17.getHeight());
        Window window1 = WindowParser("Etoile_Floor3_RoomH320_Window1.json");
        CreatingWindow(window1.getCoordinates(), window1.getHeight());
        Window window2 = WindowParser("Etoile_Floor3_RoomH320_Window2.json");
        CreatingWindow(window2.getCoordinates(), window2.getHeight());
        Window window3 = WindowParser("Etoile_Floor3_RoomH320_Window3.json");
        CreatingWindow(window3.getCoordinates(), window3.getHeight());
        Window window4 = WindowParser("Etoile_Floor3_RoomH320_Window4.json");
        CreatingWindow(window4.getCoordinates(), window4.getHeight());
        Window window5 = WindowParser("Etoile_Floor3_RoomH320_Window5.json");
        CreatingWindow(window5.getCoordinates(), window5.getHeight());
        Window window6 = WindowParser("Etoile_Floor3_RoomH320_Window6.json");
        CreatingWindow(window6.getCoordinates(), window6.getHeight());
        Window window7 = WindowParser("Etoile_Floor3_RoomH320_Window7.json");
        CreatingWindow(window7.getCoordinates(), window7.getHeight());
        Window window8 = WindowParser("Etoile_Floor3_RoomH320_Window8.json");
        CreatingWindow(window8.getCoordinates(), window8.getHeight());
        Window window9 = WindowParser("Etoile_Floor3_RoomH320_Window9.json");
        CreatingWindow(window9.getCoordinates(), window9.getHeight());
        Window window10 = WindowParser("Etoile_Floor3_RoomH320_Window10.json");
        CreatingWindow(window10.getCoordinates(), window10.getHeight());
        Window window11 = WindowParser("Etoile_Floor3_RoomH320_Window11.json");
        CreatingWindow(window11.getCoordinates(), window11.getHeight());*/

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


    //------------------------------------------------------------------ CREATORS ---------------------------------------------------------------------//


    public void CreatingFloor(double[][] coordinates, string[] objRoomsOnFloor,int height, int numberOfRooms)
    {
        Debug.Log("In creating floor method");
        float width = Mathf.Abs((float)coordinates[1][0] - (float)coordinates[0][0]) / 10;
        float l = Mathf.Abs((float)coordinates[0][1] - (float)coordinates[2][1]) / 10;
        float x = ((float)coordinates[1][0] - (float)coordinates[0][0]) / 2;
        float z = ((float)coordinates[0][1] + (float)coordinates[2][1]) / 2;
        GameObject floor1 = Instantiate(floor, new Vector3(x, 0, z), Quaternion.identity);
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
            CreatingRoom(room.getCoordinates(), room.getHeight(), room.getNumberOfDoors(), room.getNumberOfWindows());
        }
    }

    public void CreatingRoom(double[][] coordinates, int height, int nbDoors, int nbWindows)
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
        //Debug.Log("width1: " + width1);
        //Debug.Log("x1: " + x1);
        //Debug.Log("z1: " + z1);
        wall1.transform.localScale = new Vector3(width1, height, 0.1f);

        float width2 = Mathf.Abs((float)coordinates[3][0] - (float)coordinates[2][0]);
        float x2 = (float)coordinates[3][0] + (width2 / 2);
        float z2 = ((float)coordinates[2][1] + (float)coordinates[3][1]) / 2;
        GameObject wall2 = Instantiate(wall, new Vector3(x2, heightWall, z2), Quaternion.identity);
        //Debug.Log("width2: " + width2);
        //Debug.Log("x2: " + x2);
        //Debug.Log("z2: " + z2);
        wall2.transform.localScale = new Vector3(width2, height, 0.1f);

        float width3 = Mathf.Abs((float)coordinates[2][1] - (float)coordinates[1][1]);
        float x3 = ((float)coordinates[1][0] + (float)coordinates[2][0]) / 2;
        float z3 = (float)coordinates[2][1] - (((float)coordinates[2][1] - (float)coordinates[1][1]) / 2);
        GameObject wall3 = Instantiate(wall, new Vector3(x3, heightWall, z3), Quaternion.identity);
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
        //Debug.Log("width4: " + width4);
        //Debug.Log("x4: " + x4);
        //Debug.Log("z4: " + z4);
        wall4.transform.localScale = new Vector3(width4, height, 0.1f);
        wall4.transform.Rotate(new Vector3(0, 90, 0));


        /*for(int i=1; i<nbDoors+1; i++)
        {
            String name = "Door" + i + ".json";
            Door door = DoorParser(name);
            CreatingDoor(door.getCoordinates(), door.getHeight());
        }

        for(int i=1; i<nbWindows+1; i++)
        {
            String name = "Window" + i + ".json";
            Window window = WindowParser(name);
            CreatingWindow(window.getCoordinates(), window.getHeight());
        }*/

    }

    public void CreatingDoor(double[][] coordinates, double height)
    {
        float heightDoor = ((float)height / 2) + (float)coordinates[0][2];
        if (coordinates[0][1] == coordinates[1][1])
        {
            float width1 = Mathf.Abs((float)coordinates[1][0] - (float)coordinates[0][0]);
            float x1 = Mathf.Min((float)coordinates[0][0],(float)coordinates[1][0]) + (width1 / 2);
            float z1 = ((float)coordinates[0][1] + (float)coordinates[1][1]) / 2;
            GameObject door1 = Instantiate(door, new Vector3(x1, heightDoor, z1), Quaternion.identity);
            door1.transform.localScale = new Vector3(width1, (float)height, 0.2f);
        } else
        {
            float width2 = Mathf.Abs((float)coordinates[1][1] - (float)coordinates[0][1]);
            float x2 = ((float)coordinates[0][0] + (float)coordinates[1][0]) / 2;
            float z2 = Mathf.Max((float)coordinates[1][1],(float)coordinates[0][1]) - (Mathf.Abs((float)coordinates[1][1] - (float)coordinates[0][1]) / 2);
            GameObject wall3 = Instantiate(door, new Vector3(x2, heightDoor, z2), Quaternion.identity);
            wall3.transform.localScale = new Vector3(width2, (float)height, 0.2f);
            wall3.transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    public void CreatingWindow(double[][] coordinates, double height)
    {
        float widthX = Mathf.Abs((float)coordinates[0][0] - (float)coordinates[1][0]);
        //si les deux coordonnes sont egales, ie leur difference vaut zéro, la fenetre est sur un mu orienté selon y
        if (widthX == 0)
        {
            float widthY = Mathf.Abs((float)coordinates[0][1] - (float)coordinates[1][1]);
            float y = (float)coordinates[1][1] - (((float)coordinates[1][1] - (float)coordinates[0][1]) / 2);
            float heightAboveGround = (float)coordinates[0][2] + ((float)height / 2);
            //Debug.Log("coord z: " + (float)coordinates[0][2]);
            //Debug.Log("height: " + height);
            //Debug.Log("height above ground: " + heightAboveGround);
            //Debug.Log("widthY: " + widthY);
            GameObject window1 = Instantiate(window, new Vector3(0, heightAboveGround, y), Quaternion.identity);
            window1.transform.localScale = new Vector3(widthY, (float)height, 0.2f);
            window1.transform.Rotate(new Vector3(0, 90, 0));

        }

        else
        {
            float x = (float)coordinates[1][0] - (((float)coordinates[1][0] - (float)coordinates[0][0]) / 2);
            float heightAboveGround = (float)coordinates[0][2] + ((float)height / 2);
            GameObject window1 = Instantiate(window, new Vector3(x, heightAboveGround, 0), Quaternion.identity);
            window1.transform.localScale = new Vector3(widthX, (float)height, 0.2f);

        }
    }
}
