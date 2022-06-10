# Cassiopée : IoT 3D Pack

## Description of the project

This application's goal is to aware people of IoTs' omnipresence.  
It generates 3D buildings, which plans are kept in a database in JSON format.

## Used Tools

* Database: NGSI-LD, an Orion-LD specification made for building representation (https://tech-wiki.notion.site/NGSI-LD-Guide-ad6a9c4bb9e6426db9595c065bcf7c83)
* Application: Unity (https://unity.com)
* JSON lecture on Unity: Newtonsoft
* Multi-platform tool: UMI3D (https://umi3d-consortium.org/)

## Demonstration 

[video]

## Details

### Database Server

Building's plans are saved in a docker server, with **NGSI-LD** specifications. It uses Mongo-DB as database system.  
With the previous link in **Used Tools**, you will be able to install the Orion-LD server, to launch it, and to use it on your laptop.  
The tutorial tells that Orion-LD can only be used under Linux. But you can launch it from Windows, with **WSL** or **Docker Desktop** (https://www.docker.com/products/docker-desktop/)  
**Important Note** : when you will launch the server, it will automatically select the **port 1026**. You can change this specification in the .yml file.  
If you want to launch Orion-LD from a terminal, please use go to the file that contains the ```docker-compose.yml``` and type: 

```sudo docker-compose up```
  
Once the server is launched, you can start **creating entities**.  
To see the format of Buildings, Floors, Rooms, Windows and Doors, please check:  
https://github.com/Free-Hugs/Local-Server  
It is important to have a good id for the different entities. We highly recommend you to use the followings:  
* **Building** : ```building:[building_name]```
* **Floor** : ```[building_name]:Floor[floor_number]```
* **Room** : ```[building_name]:[room_name]```
* **Window** : ```[building_name]:[room_name]:Window[window_number]```
* **Door** : ```[building_name]:[room_name]:Door[door_number]```
These ids will be used to get the components for 3D Buildings. The parsers are made for these formats of ids.

There are **two geometrical types** of entities:  
* **Polygons**: they contain **5 points in their coordinates** (if they have the shape of a square or rectangle). It concerns **Floors** and **Rooms**.  
* **LineString** : they contain **2 points in their coordinates**. It concerns **Doors** and **Windows**.  

**To add an entity in the database**, you can use a curl command in your terminal (no need to cd into the file containing your docker-compose.yml).  
```curl [server-adress]:[port]/ngsi-ld/v1/entities -s -S -H 'Content-Type: application/ld+json' -d @- <<EOF```  
Once you typed this, type the data you want to add.  
Then, finish your data with ```EOF```.  
Note: as we generate 3D models, we need to add a height in rooms' and floors' descriptions. You will find the right format for this on https://github.com/Free-Hugs/Local-Server.  

**To read an entity from your database**
```curl [server-adress]:[port]/ngsi-ld/v1/entities/[entity]%3A[URI]%3A[here] -s -S -H 'Accept: application/ld+json'```  
*NB*: ```%3A``` is the equivalent of ```:```. The API request can work with ```:``` in your browser and in Unity.

**To update an entity in your database**

```curl [server-adress]:[port]/ngsi-ld/v1/entities/[entity]%3A[URI]%3A[here] -s -S -H 'Content-Type: application/json' -H 'Link: https://pastebin.com/raw/Mgxv2ykn' -d @- <<EOF```  
Put ```{}``` first, then write down the data you want to update. 
Same as entity addition: please add ```EOF``` at the end of your update.  

**Important note**: when you input the coordinates of a floor/room/door/window, please enter the coordinates of the corners of the entity **on the ground** and not on the ceil.  

### Usage of API

**To use the API requests** described previously, you need to use:
```StartCoroutine(myFunction())```

The function used in the coroutine must be of type **IEnumerator**. This means that you need a yield return of the WWW object before any operation.  
There are two cases:  
* if the connection succeeds, do your operations
* if not, print something to be aware of it
Note that *Unity can show a warning as WWW is obsolete*. With the Unity version adapted to UMI3D (2019.4.20f1 when this app was created), you do not have to care about it.

### Some Object classes

Before using the Parsers, we need to create some classes to save the values.  
There are **5 classes**:  

1. **Assets/Scripts/Building.cs**  
Building only needs to contain the **number of floors contained** called ```nbFloorsAboveGround```.  

2. **Assets/Scripts/Floor.cs**  
* ```numberOfRooms``` contains the **number of Rooms (int)**  
* ```roomsOnFloor``` contains an **array of rooms' ids contained in the floor (string[])**  
* ```coordinates``` contains the **coordinates of the floor (double[][])**  
* ```height``` contains the **height of the floor (int)**  

3. **Assets/Scripts/Room.cs**  
* ```numberOfDoors``` contains the **number of Doors in the room (int)**  
* ```numberofWindows``` contains the **number of Windows in the room (int)**  
* ```coordinates``` contains the **coordinates of the room (double[][])**  
* ```height``` contains the **height of the room (int)**  
* ```name``` contains the **id of the room (string)**  

4. **Assets/Scripts/Door.cs**  
* ```coordinates``` contains the **coordinates of the door (double[][])**  
* ```height``` contains the **height of the door (int)**  

5. **Assets/Scripts/Window.cs**  
* ```coordinates``` contains the **coordinates of the window (double[][])**
* ```height``` contains the **height of the window (int)**

### Unity Parsers

To use the data in Unity, you need to parse them from JSON format to C# format.  
To generate 3D models, we need **coordinates** of rooms, doors and windows, and the **height** value of each room.  
Those operations are made in **Assets/Script/CreatorAPI.cs**.  
To use it, add it in as a component of an Empty GameObject.

**Add the prefabs** of Walls, Floors, Doors and Windows.  
Then, add the **IP Address of the NGSI-LD Server**. Keep the building string empty (you can remove it, it just confirms that the API request takes the right building).  

At the start of the Unity Scene, the Player will spawn in an empty dark room, with a menu in front of him.  
Available buildings are displayed thanks to the API request:  
```http://" + ipAddress + ":[port_number]/ngsi-ld/v1/entities/?type=[type_of_buildings]```  
The ```type_of_buildings``` is in the context of the entity created. In our script, it corresponds to ```urn:mytypes:building```.  
Then, the user can click on the start button, which hides the dark room and spawns the selected building.  

The JSON Parsing system uses **Newtonsoft**.  
The function ```SelectToken("token_name").Value<value_type>()``` provides the value of the field *token_name* with the type *value_type*.  
* If you get an intermediate object, please use it as **var**.
*Example*:  
```var nbOfRooms = Floor.SelectToken("numberOfRooms").Value<JObject>();``` gets  
```"numbersOfDoors": { "type": "Property", "value": 1 }```  
* Then, when you want to get the value, please select the right object type.  
*Example*:  
```int numberOfRooms = nbOfRooms.SelectToken("value").Value<int>();``` gets the value ```1```.  

The Parser works in **few steps**:  
1. **BuildingParser()**  
To create an object of class ```Building```.  
You only need to collect the number of floor it contains.  

2. **getFloor(string floorName)**
To create an object of class ```Floor``` and use the **CreatingFloor(args)** to generate the 3D model of the floor.  
The code gets the JSON object with the following API request:  
```"http://" + ipAddress + ":1026/ngsi-ld/v1/entities/" + building + ":"+ floorName```  
The rest of the code gets the fields needed to create a ```Floor``` object.  
An iteration in ```CreatingFloor(args)``` looks for the rooms in the floor and calls **getRoom(string roomName)**.  

3. **getRoom(string roomName))**  
To create an object of class ```Room``` and use the **CreatingRoom(args)** to generate the 3D model of the room.  
The code gets the JSON object with the following API request:  
```"http://" + ipAddress + ":1026/ngsi-ld/v1/entities/"+ roomName```  
The rest of the code gets the fields needed to create a ```Room``` object.  
An iteration in ```CreatingRoom(args)``` looks for the rooms in the floor and calls **getWindow(string windowName)** and **getDoor(string doorName)**.  

4. **getWindow(string windowName)**  
To create an object of class ```Window``` and use the **CreatingWindow(args)** to generate the 3D model of the window.  
The code gets the JSON object with the following API request:  
```"http://" + ipAddress + ":1026/ngsi-ld/v1/entities/"+ windowName```  

5. **getDoor(string doorName)**  
To create an object of class ```Door``` and use the **CreatingDoor(args)** to generate the 3D model of the door.  
The code gets the JSON object with the following API request:  
```"http://" + ipAddress + ":1026/ngsi-ld/v1/entities/"+ doorName```  

Note : this structure allows us to get all needed information for 3D model creation using only the name of the building.  

### Code of 3D Model Creators  

### UMI3D Implementation