using Spherification.src.model.system.dependencyloader;
using Spherification.src.model.system.persistance;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Spherification
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private String sphereLibraryPath = @"C:\Users\Max\source\repos\SphereLib\SphereLib\bin\Debug\netstandard2.0\SphereLib.dll";


        private void loadApplicationState(object sender, StartupEventArgs e) {
            Console.WriteLine("Loading application state");
            
            PersistanceService.loadApplicationState();
            //loadDynamicDependencies();
        }

        private void saveApplicationState(object sender, ExitEventArgs e)
        {
            PersistanceService.persistApplicationState();
        }

        private void loadDynamicDependencies()
        {
            
            DynamicDependencyLoader.load(sphereLibraryPath);
            
        }
    }
}
