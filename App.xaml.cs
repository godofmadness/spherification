using Spherification.src.model.system.persistance;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Spherification
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        private void loadApplicationState(object sender, StartupEventArgs e) {
            Console.WriteLine("Loading application state");
            PersistanceService.loadApplicationState();
        }

        private void saveApplicationState(object sender, ExitEventArgs e)
        {
            PersistanceService.persistApplicationState();
        }
    }
}
