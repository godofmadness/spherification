using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Spherification.src.sphere
{
    class SphereService
    {

        // math calculations of points and normals
        public Sphere getSphere(String name, int radius, int accurasy, Color color) {

            Sphere sphere = new Sphere();
            sphere.setAccurasy(accurasy);
            sphere.setRadius(radius);
            sphere.setColor(color);
            sphere.setName(name);

            Point3D[,] sphereCoords = new Point3D[accurasy + 1, accurasy + 1];
            Vector3D[,] sphereNormals = new Vector3D[accurasy + 1, accurasy + 1];


            // longitude and latitude sectors
            double dt = ((360.0 / 180.0) * Math.PI) / accurasy;
            double dp = ((180.0 / 180.0) * Math.PI) / accurasy;

            for (int pi = 0; pi <= accurasy; pi++)
            {
                double phi = pi * dp;

                for (int ti = 0; ti <= accurasy; ti++)
                {
                    double theta = ti * dt;

                    
                    sphereCoords[pi, ti] = getPointInCartesiansCoords(theta, phi, radius);
                    Console.WriteLine("sphere coords {0} {1} {2}", sphereCoords[pi, ti].X, sphereCoords[pi, ti].Y, sphereCoords[pi, ti].Z);
                    sphereNormals[pi, ti] = (Vector3D) getPointInCartesiansCoords(theta, phi, radius);
                }
            }

            sphere.setPoints(sphereCoords);
            sphere.setNormals(sphereNormals);


            return sphere;
        }


        // casts point in spherifical coords to cartesian (x, y, z)
        private Point3D getPointInCartesiansCoords(double theta, double phi, double radius)
        {
            double x = radius * Math.Sin(theta) * Math.Sin(phi) ;
            double y = radius * Math.Cos(phi);
            double z = radius * Math.Cos(theta) * Math.Sin(phi);

            return new Point3D(x, y, z);
        }
    }
}
