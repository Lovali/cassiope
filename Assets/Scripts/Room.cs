using System.Collections.Generic;

public class Room {
    private int numberOfDoors;
    private int numberofWindows;
    private double[][] coordinates;

    public Room(int numberOfDoors, int numberofWindows, double[][] coordinates) {
        this.numberOfDoors = numberOfDoors;
        this.numberofWindows = numberofWindows;
        this.coordinates = coordinates;
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

    public void setNumberOfDoors(int numberOfDoors) {
        this.numberOfDoors = numberOfDoors;
    }

    public void setNumberOfWindows(int numberofWindows) {
        this.numberofWindows = numberofWindows;
    }

    public void setCoordinates(double[][] coordinates) {
        this.coordinates = coordinates;
    }
}