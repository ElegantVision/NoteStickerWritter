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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteSticker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string PathToApplication = "pack://application:,,,/NoteSticker;component/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateNote_Click(object sender, RoutedEventArgs e)
        {//opening a new window when clicked
            NoteWindowBuilder NewNote = new NoteWindowBuilder();
            
            
            
            //Window noteview = new Window();
            //noteview.Show();
            //if (noteview.IsLoaded)
            //{//add textbox to this window
            //    //StackPanel npanel = new StackPanel { Orientation = Orientation.Vertical };
            //    //npanel.Children.Add(new TextBox { FontSize = 40 });
            //    //noteview.Content = npanel;
            //}
        }
    }
}
