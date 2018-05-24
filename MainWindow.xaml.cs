using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Spherification.src.components.sphere.createspheredialog;
using Spherification.src.sphere;

namespace Spherification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private SphereDrawService sphereDrawService;
        private SphereService sphereService;
        // Reference to the geometry for later use

        private bool mDown;
        private Point mLastPos;



        private double oldX = 0;


        private Color DEFAULT_COLOR = Colors.Cyan;
        int m_radius = 15;
        int m_resolution = 20;

        double m_startHorizontalAngle = 0.0;
        double m_endHorizontalAngle = 2 * Math.PI;
        double m_startVerticalAngle = -1 * Math.PI / 2;
        double m_endVerticalAngle = Math.PI / 2;

        public MainWindow()
        {
            InitializeComponent();
            sphereDrawService = new SphereDrawService();
            sphereService = new SphereService();



            this.DataContext = new MainWindowViewModel(DialogCoordinator.Instance, group.Children, details_group.Children);
        }




        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {



            camera.Position = new Point3D(
                camera.Position.X,
                camera.Position.Y,
                camera.Position.Z - e.Delta / 250D);
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mDown = false;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            mDown = true;
            Point pos = Mouse.GetPosition(details_viewport);
            mLastPos = new Point(
                    pos.X - details_viewport.ActualWidth / 2,
                    details_viewport.ActualHeight / 2 - pos.Y);
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("CALL");
            if (!mDown) return;
            Point pos = Mouse.GetPosition(details_viewport);
            Point actualPos = new Point(
                    pos.X - details_viewport.ActualWidth / 2,
                    details_viewport.ActualHeight / 2 - pos.Y);
            double dx = actualPos.X - mLastPos.X;
            double dy = actualPos.Y - mLastPos.Y;
            double mouseAngle = 0;

            if (dx != 0 && dy != 0)
            {
                mouseAngle = Math.Asin(Math.Abs(dy) /
                    Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2)));
                if (dx < 0 && dy > 0) mouseAngle += Math.PI / 2;
                else if (dx < 0 && dy < 0) mouseAngle += Math.PI;
                else if (dx > 0 && dy < 0) mouseAngle += Math.PI * 1.5;
            }
            else if (dx == 0 && dy != 0)
            {
                mouseAngle = Math.Sign(dy) > 0 ? Math.PI / 2 : Math.PI * 1.5;
            }
            else if (dx != 0 && dy == 0)
            {
                mouseAngle = Math.Sign(dx) > 0 ? 0 : Math.PI;
            }

            double axisAngle = mouseAngle + Math.PI / 2;

            Vector3D axis = new Vector3D(
                    Math.Cos(axisAngle) * 4,
                    Math.Sin(axisAngle) * 4, 0);

            double rotation = 0.02 *
                    Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));

            Transform3DGroup group = (this.DataContext as MainWindowViewModel).activeGeometry.Transform as Transform3DGroup;
            QuaternionRotation3D r =
                 new QuaternionRotation3D(
                 new Quaternion(axis, rotation * 180 / Math.PI));

            group.Children.Add(new RotateTransform3D(r));
            Console.WriteLine("CHANGE", actualPos);
            mLastPos = actualPos;
        }


        private void keyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Button press");
            if (e.Key == Key.Right)
            {
                camera.Position = new Point3D(
                 camera.Position.X + 3,
                 camera.Position.Y,
                 camera.Position.Z);
            }

            if (e.Key == Key.Left)
            {
                camera.Position = new Point3D(
                 camera.Position.X - 3,
                 camera.Position.Y,
                 camera.Position.Z);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("nenens");
        }
    }
}
