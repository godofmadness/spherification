using SphereLib;
using Spherification.src.model.system.comproccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Spherification.src.model.sphere
{
    class SphereClientService 
    {

        public Sphere getSphere(string name, int radius, int accurasy, Color color)
        {
            SphereService cCom = getCOMReference();

            if (cCom != null)
            {
                return cCom.getSphere(name, radius, accurasy, color);
            } else
            {
                return null;
            }
        }


        private SphereService getCOMReference()
        { 
            Guid CLSID_ShellDesktop = new Guid(ComGUID.COMClass_GUID);
            Type shellDesktopType = Type.GetTypeFromCLSID(CLSID_ShellDesktop, true);
            object shellDesktop = Activator.CreateInstance(shellDesktopType);

            return shellDesktop as SphereService;
        }

        public Point3D getPointInCartesiansCoords(double theta, double phi, double radius)
        {
            SphereService cCom = getCOMReference();

            if (cCom != null)
            {
                return cCom.getPointInCartesiansCoords(theta, phi, radius);
            }
            {
                return new Point3D();
            }
        }


      
    }
}
