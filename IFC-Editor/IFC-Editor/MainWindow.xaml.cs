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
using Xbim.Ifc4.Interfaces;
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

            ElementContext db = new ElementContext();

            // Clear SQLite
            
            List<Element> ExistingItems = LoadElementData();
            foreach (Element item in ExistingItems)
            {
                DeleteElement(db, item);
            }

            // Create Elements from Ifc elements
            foreach (var item in objs)
            {
                //System.Diagnostics.Debug.WriteLine(item);
                CreateElement(db, item.GlobalId, item.Name, GetElementType(item));
            }
            db.SaveChanges();

            /*
            System.Diagnostics.Debug.WriteLine("HELLO");
            System.Diagnostics.Debug.WriteLine(model.ToString());
            var context = new Xbim3DModelContext(model);
            context.CreateContext();
            DrawingControl.Model = model;
            DrawingControl.LoadGeometry(model);*/


            DG.ItemsSource = LoadElementData();

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
            //ModelProvider.Refresh();
        }

        private void DrawingControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void CreateElement(ElementContext db, string ElementId, string Name, string ElementType)
        {
            db.Add(new Element { ElementId = ElementId, Name = Name, ElementType = ElementType, Exclude = false});
        }

        private List<Element> LoadElementData()
        {
            var db = new ElementContext();
            List<Element> elements = db.Elements.OrderBy(b => b.ElementId).ToList();
            return elements;
        }

        private void DeleteElement(ElementContext db, Element Element)
        {
            db.Remove(Element);
        }

        private string GetElementType(IfcBuildingElement item)
        {
            if (item is IIfcWall wall)
            {
                return "Wall";
            }
            else if (item is IIfcDoor door)
            {
                return "Door";
            }
            else if (item is IIfcWindow window)
            {
                return "Window";
            }
            else if (item is IIfcRoof roof)
            {
                return "Roof";
            }
            else if (item is IIfcFace face)
            {
                return "Face";
            }
            return "Unknown";
            /*
            var properties = item.IsDefinedBy
            .Where(r => r.RelatingPropertyDefinition is IIfcPropertySet)
            .SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
            .OfType<IIfcPropertySingleValue>();
            foreach (var property in properties)
            {
                System.Diagnostics.Debug.WriteLine(property.NominalValue);
                if (property.Name == "Type")
                {
                    return property.NominalValue.ToString();
                }
            }
                
            return "No value";*/
        }
    }
}