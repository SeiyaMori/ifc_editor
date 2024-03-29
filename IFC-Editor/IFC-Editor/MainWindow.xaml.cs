using IFC_Editor.Models;
using PropertyTools.Wpf;
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

            DG.ItemsSource = LoadCollectionData();
        }

        private void DrawingControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private List<Blog> LoadCollectionData()
        {
            var db = new BloggingContext();
            List<Blog> blogs = db.Blogs.OrderBy(b => b.BlogId).ToList();
            return blogs;
        }
    }
}