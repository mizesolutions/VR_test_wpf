using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WindowsInput;
using WindowsInput.Native;

namespace VR_test_wpf
{
    /// <summary>
    /// Interaction logic for child1.xaml
    /// </summary>
    public partial class Child1 : Window
    {
        public event NotifyParentDelegate NotifyParentEvent;
        public event NotifyParentDelegateMouse NotifyParentEventMouse;
        private InputSimulator sim;

        public Child1()
        {
            InitializeComponent();
            sim = new InputSimulator();
        }
        

        public void ReadKeyInChild()
        {
            label_2.Content = "Please press key or Move the Mouse. Esc to quit:";
        }

        private void TextBox_2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() != ConsoleKey.Escape.ToString())
            {
                NotifyParent(e.Key.ToString());
                textBox_2.Clear();
            }
            else
            {
                Close();
            }
        }

        public void NotifyParent(string pressedKey)
        {
            if (NotifyParentEvent != null)
            {
                    CustomEventArgs customEventArgs = new CustomEventArgs(pressedKey);
                    //Raise Event. All the listeners of this event will get a call.
                    NotifyParentEvent(customEventArgs);
            }
        }

        public void KeySim()
        {
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_A);
        }

        public void MouseSim()
        {
            var sim = new InputSimulator();
            sim.Mouse
               .MoveMouseTo(0, 0)
               .Sleep(1000)
               .MoveMouseTo(5000, 5000)
               .Sleep(1000)
               .LeftButtonDown()
               .MoveMouseTo(10000, 35000)
               .LeftButtonUp();
        }

        public void NotifyParentMouse(object sender, MouseEventArgs m)
        {
            if (NotifyParentEvent != null)
            {
                //Raise Event. All the listeners of this event will get a call.
                NotifyParentEventMouse(sender,m);
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            NotifyParentMouse(sender,e);
        }

        public void CloseChild1()
        {
            Close();
        }

        private void Path_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
