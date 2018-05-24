using MahApps.Metro.Controls.Dialogs;
using Spherification.src.components.sphere.createspheredialog;
using Spherification.src.sphere;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Spherification
{
    class MainWindowViewModel: INotifyPropertyChanged
    {

        private Color DEFAULT_COLOR = Colors.Cyan;
        private int SPHERE_GENERATION_OFFSET_X = 5;

        private readonly IDialogCoordinator _dialogCoordinator;
        private SphereService sphereService;
        private SphereDrawService sphereDrawService;
        private Model3DCollection sphereContainer;
        private Model3DCollection detailsSphereContainer;

        public Point3D[] activePoints { get; set; }
        public GeometryModel3D activeGeometry { get; set; }
        public Sphere activeSphere { get; set; }
        public Boolean isDetailsOpened { get; set; }

        public ObservableCollection<Sphere> spheres { get; set; }


        private int currentSphereGenerationOffsetX;

        public event PropertyChangedEventHandler PropertyChanged;

        //Constructor
        public MainWindowViewModel(IDialogCoordinator dialogCoordinator, Model3DCollection sphereContainer,Model3DCollection detailsSphereContainer)
        {
            // Dialog coordinator provided by Mahapps framework 
            // Either passed into MainViewModel constructor to conform to MVVM:-
            isDetailsOpened = true;
            _dialogCoordinator = dialogCoordinator;
            sphereService = new SphereService();
            sphereDrawService = new SphereDrawService();
            this.sphereContainer = sphereContainer;
            this.detailsSphereContainer = detailsSphereContainer;
            spheres = new ObservableCollection<Sphere>();

            // or just initialise directly here
            // _dialogCoordinator = new DialogCoordinator();
        }


        // handler to notify view when propery changed
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        public ActionCommand createSphereAction
        {
            get
            {
                return new ActionCommand(p => showCreateSphereDialog());
            }
        }

        public ActionCommand setActiveSphereAction
        {
            get
            {
                return new ActionCommand(p => setActiveSphere(p));
            }
            set
            {

            }
        }

        public void setActiveSphere(object sphere)
        {
           
            this.activeSphere = sphere as Sphere;
            isDetailsOpened = true;
            NotifyPropertyChanged();

            Console.WriteLine( detailsSphereContainer.Count());
            sphereDrawService.clearModel(detailsSphereContainer);
            Console.WriteLine(detailsSphereContainer.Count());

            activeGeometry =  sphereDrawService.draw(detailsSphereContainer, activeSphere, 0);
            
           
            activePoints = activeSphere.points.Cast<Point3D>().ToArray();
            
        }



            public async void showCreateSphereDialog()
            {
                Console.WriteLine("MainWindowViewModel@createSphere | create sphere in view model");

                var dialog = new CreateSphereDialog();

                dialog.Height = 300;
                dialog.Width = 400;

                var dialogViewModel = new CreateSphereDialogViewModel(async instance =>
                {
                    Console.WriteLine("MainWindowViewModel@createSphere | closing dialog");
                    await _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    //instance --> dialog ViewModel
                    //if (!(instance.Cancel || String.IsNullOrEmpty(instance.UserInput)) ProcessUserInput(instance.UserInput);
                    if (!instance.Cancel){
                        Console.WriteLine("MainWindowViewModel@createSphere | generating sphere ( name {0}, radius {1}, accuracy {2}, color {3}", instance.name, instance.radius, instance.accuracy, instance.color);
                        generateSphere(instance.color, instance.radius, instance.accuracy, instance.name);
                    }
                    
                });

                dialogViewModel.MessageText = "SPHERE CONFIGURATION";

                dialog.DataContext = dialogViewModel;

                await _dialogCoordinator.ShowMetroDialogAsync(this, dialog);

            }

            public void generateSphere(Color? color, int radius, int accuracy, String name) {
                Sphere sphere = sphereService.getSphere(name, radius, accuracy, color ?? (DEFAULT_COLOR));

                Console.WriteLine("Sphere {0}", sphere);
                sphereDrawService.draw(sphereContainer, sphere, currentSphereGenerationOffsetX);
                spheres.Add(sphere);
                

                currentSphereGenerationOffsetX += radius + SPHERE_GENERATION_OFFSET_X;
            }

        }
    }

