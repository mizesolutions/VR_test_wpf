using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsInput.Native;
using WindowsInput;
using System.Windows;

namespace VR_test_wpf
{
    public delegate void NotifyParentDelegate(CustomEventArgs customEventArgs);
    public delegate void NotifyParentDelegateMouse(object sender, MouseEventArgs m);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Child1 _child;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ParentMethod()
        {
            //Initializing Child class in Parent Constructor
            _child = new Child1();
            //Registering child class event
            _child.NotifyParentEvent += new NotifyParentDelegate(_child_NotifyParentEvent);
            _child.NotifyParentEventMouse += new NotifyParentDelegateMouse(_child_NotifyParentEventMouse);
            _child.ReadKeyInChild();
            
        }
        
        //Event Listener. This function will be called whenever child class raises event.
        private void _child_NotifyParentEvent(CustomEventArgs customEventArgs)
        {
            textBox_1.Text += "\n"+customEventArgs.Key.ToString();
            textBox_1.ScrollToEnd();
        }

        //Event Listener. This function will be called whenever child class raises event.
        private void _child_NotifyParentEventMouse(object sender,MouseEventArgs m)
        {
            circle.Center = m.GetPosition((IInputElement)sender);
        }

        private void Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            _child.Close();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            ParentMethod();
            label_1.Content += "Keystrokes:";
            label_3.Content += "Mouse Movement:";
            _child.Top = Top;
            _child.Left = (Left + Width) - 5;
            _child.Show();
        }
    }
}
