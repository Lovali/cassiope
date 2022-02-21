public class Room {
    public int numberOfDoors;
    public int numberofWindows;
    public int [][][] coordinates;

    public Room(int numberOfDoors, int numberofWindows, int[][][] coordinates) {
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

    public int[][][] getCoordinates() { 
        return coordinates;
    }

    public void setNumberOfDoors(int numberOfDoors) {
        this.numberOfDoors = numberOfDoors;
    }

    public void setNumberOfWindows(int numberofWindows) {
        this.numberofWindows = numberofWindows;
    }

    public void setCoordinates(int[][][] coordinates) {
        this.coordinates = coordinates;
    }
}