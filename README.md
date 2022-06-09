# Cassiopée : IoT 3D Pack

## Description of the project

This application's goal is to aware people of IoTs' omnipresence.  
It generates 3D buildings, which plans are kept in a database in JSON format.

## Used Tools

* Database: NGSI-LD, an Orion-LD specification made for building representation (https://tech-wiki.notion.site/NGSI-LD-Guide-ad6a9c4bb9e6426db9595c065bcf7c83)
* Application: Unity (https://unity.com)
* Multi-platform tool: UMI3D (https://umi3d-consortium.org/)

## Demonstration 

[video]

## Details

### Database Server

Building's plans are saved in a docker server, with **NGSI-LD** specifications. It uses Mongo-DB as database system.  
With the previous link in **Used Tools**, you will be able to install the Orion-LD server, to launch it, and to use it on your laptop.  
The tutorial tells that Orion-LD can only be used under Linux. But you can launch it from Windows, with **WSL** or **Docker Desktop** (https://www.docker.com/products/docker-desktop/)  
If you want to launch Orion-LD from a terminal, please use go to the file that contains the ```docker-compose.yml``` and type: 

```sudo docker-compose up```
  
Once the server is launched, you can start **creating entities**.  
To see the format of Buildings, Floors, Rooms, Windows and Doors, please check:  
https://github.com/Free-Hugs/Local-Server  
It is important to have a good id for the different entities. We highly recommand you to use the followings:  
* **Building** : ```building:[building_name]```
* **Floor** : ```[building_name]:Floor[floor_number]```
* **Room** : ```[building_name]:[room_name]```
* **Window** : ```[building_name]:[room_name]:[window_name]```
* **Door** : ```[building_name]:[room_name]:[window_name]```
These ids will be used to get the components for 3D Buildings. The parsers are made for these formats of ids.

**To add an entity in the database**, you can use a curl command in your terminal (no need to cd into the file containing your docker-compose.yml).  
```curl [server-adress]:[port]/ngsi-ld/v1/entities -s -S -H 'Content-Type: application/ld+json' -d @- <<EOF```  
Once you typed this, type the data you want to add.  
Then, finish your data with ```EOF```.

**To read an entity from your database**
```curl [server-adress]:[port]/ngsi-ld/v1/entities/[entity]%3A[URI]%3A[here] -s -S -H 'Accept: application/ld+json'```  
*NB*: ```%3A``` is the equivalent of ```:```. The API request can work with ```:``` in your browser and in Unity.

**To update an entity in your database**

```curl [server-adress]:[port]/ngsi-ld/v1/entities/[entity]%3A[URI]%3A[here] -s -S -H 'Content-Type: application/json' -H 'Link: https://pastebin.com/raw/Mgxv2ykn' -d @- <<EOF```  
Put ```{}``` first, then write down the datas you want to update. 
Same as entity addition: please add ```EOF``` at the end of your update.
