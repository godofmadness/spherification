using SphereLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Spherification.src.sphere
{
    class SphereDrawService
    {


        public GeometryModel3D draw(Model3DCollection container, Sphere sphere, int offsetX)
        {



            MeshGeometry3D mesh = new MeshGeometry3D();

            for (int pi = 0; pi <= sphere.getAccurasy(); pi++)
            {


                for (int ti = 0; ti <= sphere.getAccurasy(); ti++)
                {


                    mesh.Positions.Add(new Point3D(sphere.getPoints()[pi, ti].X + offsetX, sphere.getPoints()[pi, ti].Y, sphere.getPoints()[pi, ti].Z));
                    mesh.Normals.Add((Vector3D)sphere.getPoints()[pi, ti]);
                    //mesh.TextureCoordinates.Add(new Point(theta / (2 * Math.PI), phi / (Math.PI)));
                }
            }

            for (int pi = 0; pi < sphere.getAccurasy(); pi++)
            {
                for (int ti = 0; ti < sphere.getAccurasy(); ti++)
                {
                    int x0 = ti;
                    int x1 = (ti + 1);
                    int y0 = pi * (sphere.getAccurasy() + 1);
                    int y1 = (pi + 1) * (sphere.getAccurasy() + 1);

                    mesh.TriangleIndices.Add(x0 + y0);
                    mesh.TriangleIndices.Add(x0 + y1);
                    mesh.TriangleIndices.Add(x1 + y0);

                    mesh.TriangleIndices.Add(x1 + y0);
                    mesh.TriangleIndices.Add(x0 + y1);
                    mesh.TriangleIndices.Add(x1 + y1);
                }
            }

            GeometryModel3D mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(new SolidColorBrush(sphere.getColor())));
            mGeometry.Transform = new Transform3DGroup();
            
            container.Add(mGeometry);

            return mGeometry;
        }


        // clears model without deleting light objects
        public void clearModel(Model3DCollection models)
        {
            Model3D light1 = models[0];
            Model3D light2 = models[1];

            models.Clear();
            models.Add(light1);
            models.Add(light2);
        }
    }
}
