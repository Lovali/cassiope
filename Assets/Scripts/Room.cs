using System.Collections.Generic;

public class Room {
    private int numberOfDoors;
    private int numberofWindows;
    private double[][] coordinates;
    private int height;
    private string name;

    public Room(int numberOfDoors, int numberofWindows, double[][] coordinates, int height) {
        this.numberOfDoors = numberOfDoors;
        this.numberofWindows = numberofWindows;
        this.coordinates = coordinates;
        this.height = height;
    }

    public int getNumberOfDoors() {
        return numberOfDoors;
    }

    public int getNumberOfWindows() {
        return numberofWindows;
    }

    public double[][] getCoordinates() {
        return coordinates;
    }

    public int getHeight()
    {
        return height;
    }
    public string getName()
    {
        return name;
    }

    public void setNumberOfDoors(int numberOfDoors) {
        this.numberOfDoors = numberOfDoors;
    }

    public void setNumberOfWindows(int numberofWindows) {
        this.numberofWindows = numberofWindows;
    }

    public void setCoordinates(double[][] coordinates) {
        this.coordinates = coordinates;
    }

    public void setName(string newName)
    {
        this.name = newName;
    }
}