using IFC_Editor.Models;
using IFC_Editor.Controllers;
using PropertyTools.Wpf;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;
using Xbim.ModelGeometry.Scene;

namespace IFC_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Get all IfcBuildingElement instances
            IfcStore model = IfcStore.Open(@"C:\Users\Seiya\Desktop\example_building.ifc");
            var objs = model.Instances.OfType<IfcBuildingElement>();

            // Element Controller
            ElementController elementController = new ElementController();


            // Clear SQLite
            
            List<Element> existingItems = elementController.GetAllElements();
            foreach (Element item in existingItems)
            {
                elementController.DeleteElement(item);
            }

            // Create Elements from Ifc elements
            foreach (var item in objs)
            {
                //System.Diagnostics.Debug.WriteLine(item);

                elementController.CreateElement(item.GlobalId, item.Name, elementController.GetElementType(item));
            }
            elementController.SaveDb();


            DG.ItemsSource = elementController.GetAllElements();

            // 3D view
            Loaded += MainWindow_Loaded;

            openFile();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ModelProvider.Refresh();
        }

        private ObjectDataProvider ModelProvider
        {
            get
            {
                return MainFrame.DataContext as ObjectDataProvider;
            }
        }

        public void openFile()
        {
            var model = IfcStore.Open(@"C:\Users\Seiya\Desktop\example_building.ifc");
            var context = new Xbim3DModelContext(model);
            context.CreateContext();
            ModelProvider.ObjectInstance = model;
        }

        private void DrawingControl_MouseMove(object sender, MouseEventArgs e)
        {

        }
        
    }
}