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
using System.Windows.Forms;
using System.Drawing;


namespace VR_test_wpf
{
    public delegate void NotifyParentDelegate(CustomEventArgs customEventArgs);
    public delegate void NotifyParentDelegateMouse(object sender, System.Windows.Input.MouseEventArgs m);
    

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Child1 _child;
        private NotifyIcon MyNotifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            MinimizeToTray.Enable(this);
            CenterWindowOnScreen();
            MyNotifyIcon = new NotifyIcon
            {
                Icon = new Icon(fileName: @"../../img/inf.ico")
            };
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
        }

        void MyNotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                MyNotifyIcon.BalloonTipTitle = "Minimize Sucessful";
                MyNotifyIcon.BalloonTipText = "Minimized the app ";
                MyNotifyIcon.ShowBalloonTip(400);
                MyNotifyIcon.Visible = true;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                MyNotifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2) - 200;
            this.Top = (screenHeight / 2) - (windowHeight / 2);
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
            textBox_1.Text += "\n" + customEventArgs.Key.ToString();
            textBox_1.ScrollToEnd();
        }

        //Event Listener. This function will be called whenever child class raises event.
        private void _child_NotifyParentEventMouse(object sender, System.Windows.Input.MouseEventArgs m)
        {
            circle.Center = m.GetPosition((IInputElement)sender);
        }

        private void Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            ChildGroupChange();
            _child.Close();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            ChildGroupChange();
            ParentMethod();
            label_1.Content += "Keystrokes:";
            label_3.Content += "Mouse Movement:";
            _child.Top = Top;
            _child.Left = (Left + Width) - 5;
            _child.Show();

        }

        private void ChildGroupChange()
        {
            if(textBox_1.Visibility == Visibility.Visible)
            {
                btn_Start.IsEnabled = true;
                btn_Stop.IsEnabled = false;
                label_1.Visibility = Visibility.Hidden;
                label_3.Visibility = Visibility.Hidden;
                grid_HelloWorld.Visibility = Visibility.Hidden;
                textBox_1.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_Start.IsEnabled = false;
                btn_Stop.IsEnabled = true;
                label_1.Visibility = Visibility.Visible;
                label_3.Visibility = Visibility.Visible;
                grid_HelloWorld.Visibility = Visibility.Visible;
                textBox_1.Visibility = Visibility.Visible;
            }
        }

        private void Btn_Sim_Type_Click(object sender, RoutedEventArgs e)
        {
            ParentMethod();
            WindowState = WindowState.Minimized;
            label_1.Content += "Keystrokes:";
            _child.KeySim();
            WindowState = WindowState.Normal;
            this.Activate();
        }

        private void Btn_Sim_Mouse_Click(object sender, RoutedEventArgs e)
        {
            ParentMethod();
            WindowState = WindowState.Minimized;
            System.Windows.Point start = Mouse.GetPosition(this);
            _child.MouseSim(start);
            WindowState = WindowState.Normal;
            this.Activate();
        }

    }

}
