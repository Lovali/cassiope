﻿public class Window
{
    private double[][] coordinates;
    private int height;

    public Window(double[][] coordinates, int height)
    {
        this.coordinates = coordinates;
        this.height = height;
    }

    public double[][] getCoordinates()
    {
        return coordinates;
    }

    public int getHeight()
    {
        return height;
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
