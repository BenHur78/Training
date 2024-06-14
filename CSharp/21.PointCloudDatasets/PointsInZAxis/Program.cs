using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, this program generates points between A and B where the distance between a point to its previous point is 20 microns.");
        GeneratePointsOnXZAxis(false);        
    }

    public static void GeneratePointsOnXAxis()
    {
        // Define the origin, positive limit, and negative limit points
        Point3D origin = new Point3D(0, 35, 0);
        Point3D positiveLimit = new Point3D(10, 35, 0);
        Point3D negativeLimit = new Point3D(-10, 35, 0);

        // Generate the points between the limits
        double distance = 0.02; // 20 microns
        Point3D[] points = InternalGeneratePointsOnXAxis(origin, positiveLimit, negativeLimit, distance);

        // Write the points to a text file
        string filePath = "pointsOnXAxis.txt";

        Point3D normal = new Point3D(0, 0, 1);        
        WritePointsToFile(filePath, points, normal);
        Console.WriteLine("Points generated and written to file.");
    }

    public static void GeneratePointsOnXZAxis(bool usingSteps)
    {
        // Define the origin, positive limit, and negative limit points
        Point3D origin = new Point3D(0, 35, 0);
        Point3D positiveLimit = new Point3D(10, 35, 0);
        positiveLimit = new Point3D(15, 35, 0);
        positiveLimit = new Point3D(40, 35, 0);
        Point3D negativeLimit = new Point3D(-10, 35, 0);
        negativeLimit = new Point3D(-10, 35, -0.15);
        negativeLimit = new Point3D(-40, 35, -3);

        // Generate the points between the limits
        double distance = 0.02; // 20 microns        
        Point3D[] points = usingSteps ? InternalGeneratePointsOnXZAxisUsingSteps(origin, positiveLimit, negativeLimit, distance) : InternalGeneratePointsOnXZAxis(origin, positiveLimit, negativeLimit, distance);

        // Write the points to a text file
        string filePath = usingSteps ? "pointsOnXZAxis_steps.txt" : "pointsOnXZAxis.txt";

        Point3D normal = new Point3D(0, 0, 1);        
        WritePointsToFile(filePath, points, normal);
        Console.WriteLine("Points generated and written to file.");
    }

    public static void GeneratePointsOnZAxis()
    {
        // Define the origin, positive limit, and negative limit points
        Point3D origin = new Point3D(0, 35, 0);
        Point3D positiveLimit = new Point3D(0, 35, 2);
        Point3D negativeLimit = new Point3D(0, 35, -2);

        // Generate the points between the limits
        double distance = 0.02; // 20 microns
        Point3D[] points = InternalGeneratePointsOnZAxis(origin, positiveLimit, negativeLimit, distance);

        // Write the points to a text file
        string filePath = "points.txt";

        Point3D normal = new Point3D(0, 0, 1);        
        WritePointsToFile(filePath, points, normal);
        Console.WriteLine("Points generated and written to file.");
    }

    private static Point3D[] GeneratePointsOnXAxis(Point3D origin, Point3D positiveLimit, Point3D negativeLimit, double distance)
    {
        int numPoints = (int)((positiveLimit.Z - negativeLimit.Z) / distance) + 1;
        Point3D[] points = new Point3D[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            double z = negativeLimit.Z + i * distance;
            points[i] = new Point3D(origin.X, origin.Y, z);
        }

        return points;
    }

    private static Point3D[] InternalGeneratePointsOnXAxis(Point3D origin, Point3D positiveLimit, Point3D negativeLimit, double distance)
    {
        int numPoints = (int)((positiveLimit.X - negativeLimit.X) / distance) + 1;
        Point3D[] points = new Point3D[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            double x = negativeLimit.X + i * distance;
            points[i] = new Point3D(x, origin.Y, origin.Z);
        }

        return points;
    }

    private static Point3D[] InternalGeneratePointsOnXZAxis(Point3D origin, Point3D positiveLimit, Point3D negativeLimit, double distance)
    {
        int numPoints = (int)((positiveLimit.X - negativeLimit.X) / distance) + 1;
        Point3D[] points = new Point3D[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            double x = negativeLimit.X + i * distance;
            double z = negativeLimit.Z + i * (distance/2); //better
            z = negativeLimit.Z + i*(0.001); //even better
            points[i] = new Point3D(x, origin.Y, z);
        }

        return points;
    }

    /*
    *                  ----------
    *        ---------
    *   -----
    */
    private static Point3D[] InternalGeneratePointsOnXZAxisUsingSteps(Point3D origin, Point3D positiveLimit, Point3D negativeLimit, double distance)
    {
        int numPoints = (int)((positiveLimit.X - negativeLimit.X) / distance) + 1;
        Point3D[] points = new Point3D[numPoints*10 + numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            double x = negativeLimit.X + i * distance;
            double z = negativeLimit.Z + i * (distance/2);
            points[i + i*10] = new Point3D(x, origin.Y, z);

            for(int ii=1; ii <= 10; ii++)
            {
                double xx = points[i*10].X + ii*0.05; //0.001 = 1 micron
                points[i + i*10 + ii] = new Point3D(xx, origin.Y, z);
            }
        }

        return points;
    }

    private static Point3D[] InternalGeneratePointsOnZAxis(Point3D origin, Point3D positiveLimit, Point3D negativeLimit, double distance)
    {
        int numPoints = (int)((positiveLimit.Z - negativeLimit.Z) / distance) + 1;
        Point3D[] points = new Point3D[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            double z = negativeLimit.Z + i * distance;
            points[i] = new Point3D(origin.X, origin.Y, z);
        }

        return points;
    }

    private static void WritePointsToFile(string filePath, Point3D[] points, Point3D normal)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Point3D point in points)
            {
                string line = $"{point.X};{point.Y};{point.Z};{normal.X};{normal.Y};{normal.Z}";
                writer.WriteLine(line);
            }
        }
    }
}

class Point3D
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Point3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}