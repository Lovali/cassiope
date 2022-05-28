public class IOT
{
    private double[] coordinates;
    private double height;

    public IOT(double[] coordinates, double height)
    {
        this.coordinates = coordinates;
        this.height = height;
    }

    public double[] getCoordinates()
    {
        return coordinates;
    }

    public double getHeight()
    {
        return height;
    }

    public void setCoordinates(double[] coordinates)
    {
        this.coordinates = coordinates;
    }

    public void setHeight(double height)
    {
        this.height = height;
    }
}
