public class Floor {

    private int numberOfRooms;
    private int[] roomsOnFloor;
    private double[][] coordinates;
    private int height;

    public Floor(int numberOfRooms, int[] roomsOnFloor, double[][] coordinates, int height)
    {
        this.numberOfRooms = numberOfRooms;
        this.roomsOnFloor = roomsOnFloor;
        this.coordinates = coordinates;
        this.height = height;
    }

    public int getNumberOfRooms()
    {
        return numberOfRooms;
    }

    public int[] getRoomsOnFloor()
    {
        return roomsOnFloor;
    }

    public double[][] getCoordinates()
    {
        return coordinates;
    }

    public int getHeight()
    {
        return height;
    }

    public void setNumberOfRooms(int numberOfRooms)
    {
        this.numberOfRooms = numberOfRooms;
    }

    public void setRoomsOnFloor(int[] roomsOnFloor)
    {
        this.roomsOnFloor = roomsOnFloor;
    }

    public void setCoordinates(double[][] coordinates)
    {
        this.coordinates = coordinates;
    }

    public void setHeight(int height)
    {
        this.height = height;
    }
}