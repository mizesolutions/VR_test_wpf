using System;
using System.Windows;
using System.Windows.Input;
using WindowsInput;

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

        private void TextBox_2_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != ConsoleKey.Escape.ToString())
            {
                NotifyParent(e.Text);
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
            //sim.Keyboard
            //    .KeyPress(VirtualKeyCode.VK_A)
            //    .KeyPress(VirtualKeyCode.VK_B);
            NotifyParent("a");
            NotifyParent("b");
            NotifyParent("c");
            NotifyParent("d");
        }

        public void MouseSim(Point start)
        {
            
            var sim = new InputSimulator();
            for (int i = 1; i < 10; i++)
            {
                sim.Mouse.MoveMouseBy(i * -5, i);
                sim.Mouse.Sleep(50);
            }
            for (int i = 1; i < 10; i++)
            {
                sim.Mouse.MoveMouseBy(i * 5, i);
                sim.Mouse.Sleep(50);
            }
            for (int i = 1; i < 10; i++)
            {
                sim.Mouse.MoveMouseBy(i, i * -5);
                sim.Mouse.Sleep(50);
            }
            for (int i = 1; i < 10; i++)
            {
                sim.Mouse.MoveMouseBy(i, i * 5);
                sim.Mouse.Sleep(50);
            }
            for (int i = 1; i < 8; i++)
            {
                sim.Mouse.MoveMouseBy(i * -5, i);
                sim.Mouse.Sleep(50);
            }
            for (int i = 1; i < 7; i++)
            {
                sim.Mouse.MoveMouseBy(i, i * -6);
                sim.Mouse.Sleep(50);
            }



            //sim.Mouse.MoveMouseTo(start.X, start.Y);
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

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() != ConsoleKey.Escape.ToString())
            {
                NotifyParent(e.Key.ToString());
            }
            else
            {
                Close();
            }
        }
    }
}
