using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
namespace Spherification.src.sphere
{
    [Serializable]
    // sphere model defines all data known about sphere
    class Sphere
    {
        // number of polygons ( define how sphere os plane )
        public String name { get; set; }

        public int accurasy { get; set; }

        // radius
        public int radius { get; set; }

        // color
        [NonSerialized]
        private Color color;

        private String colorStr { get; set; }

        // all sphere points 
        public Point3D[,] points { get; set; }

        // normals
        private Vector3D[,] normals;



        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public int getAccurasy()
        {
            return accurasy;
        }

        public void setAccurasy(int accurasy)
        {
            this.accurasy = accurasy;
        }

        public String getColorStr()
        {
            return colorStr;
        }

        public void setColorStr(String colorStr)
        {
            this.colorStr = colorStr;
        }


        public int getRadius()
        {
            return radius;
        }

        public void setRadius(int radius)
        {
            this.radius = radius;
        }

        public Color getColor()
        {
            return color;
        }

        public void setColor(Color color)
        {
            this.colorStr = color.ToString();
            Console.WriteLine("COLOR STR {0}", colorStr);
            this.color = color;
        }

        public Point3D[,] getPoints()
        {
            return points;
        }

        public void setPoints(Point3D[,] points)
        {
            this.points = points;
        }

        public Vector3D[,] getNormals()
        {
            return normals;
        }

        public void setNormals(Vector3D[,] normals)
        {
            this.normals = normals;
        }

        

    }
}
