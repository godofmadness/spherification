using SphereLib;
using Spherification.src.sphere;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spherification.src.model
{
    [Serializable]
    public class ApplicationState
    {
        private ObservableCollection<Sphere> spheres;

        internal ObservableCollection<Sphere> Spheres { get => spheres; set => spheres = value; }
   
     
        public ApplicationState()
        {
            
            Spheres = new ObservableCollection<Sphere>();
        }
    }
}
