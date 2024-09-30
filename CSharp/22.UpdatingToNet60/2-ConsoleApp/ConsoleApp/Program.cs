using MeshingWrapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace HelloWorldApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // See https://aka.ms/new-console-template for more information
            Console.WriteLine("Hello, World!");
            InternalCreateMesh();
        }

        public static void InternalCreateMesh()
        {
            PointCloud revengePointCloud = null;
            HalfedgeMesh halfedgeMesh = null;

            try
            {
                IReadOnlyList<double[]> points = new List<double[]>
                {
                    new double[] { 1.0, 2.0, 3.0 },
                    new double[] { 4.0, 5.0, 6.0 },
                    new double[] { 7.0, 8.0, 9.0 }
                };

                IReadOnlyList<float[]> normals = new List<float[]>()
                {
                    new float[] { 0.0F, 0.0F, 1.0F },
                    new float[] { 0.0F, 0.0F, 1.0F },
                    new float[] { 0.0F, 0.0F, 1.0F },
                };


                revengePointCloud =
                    new PointCloud(points, normals);

                var resolution = BallMeshingParams.AutoTuneResolution(new[] { revengePointCloud });
                Debug.Assert(resolution == 5.1961523294448853);

                halfedgeMesh = new HalfedgeMesh();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}