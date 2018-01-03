using System;
using System.Windows;
using System.Windows.Input;
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
            var sim = new InputSimulator();
            sim.Keyboard
                .KeyPress(VirtualKeyCode.VK_A)
                .Sleep(1000)
                .KeyPress(VirtualKeyCode.VK_W)
                .Sleep(1000)
                .KeyPress(VirtualKeyCode.VK_D)
                .Sleep(1000)
                .KeyPress(VirtualKeyCode.VK_S)
                .Sleep(1000)
                .KeyPress(VirtualKeyCode.LEFT)
                .Sleep(1000)
                .KeyPress(VirtualKeyCode.UP)
                .Sleep(1000)
                .KeyPress(VirtualKeyCode.RIGHT)
                .Sleep(1000)
                .KeyPress(VirtualKeyCode.DOWN)
                .Sleep(1000)
                .KeyPress(VirtualKeyCode.RETURN);
            //NotifyParent("a");
            //NotifyParent("b");
            //NotifyParent("c");
            //NotifyParent("d");
            //SendKeys.Send(Key.A);
            //Window_KeyUp(this, SendKeys.E1);

            //SendKeys.Send(Key.W);
            //Window_KeyUp(this, SendKeys.E1);

            //SendKeys.Send(Key.D);
            //Window_KeyUp(this, SendKeys.E1);

            //SendKeys.Send(Key.S);
            //Window_KeyUp(this, SendKeys.E1);

            //SendKeys.Send(Key.Left);
            //Window_KeyUp(this, SendKeys.E1);

            //SendKeys.Send(Key.Up);
            //Window_KeyUp(this, SendKeys.E1);

            //SendKeys.Send(Key.Right);
            //Window_KeyUp(this, SendKeys.E1);

            //SendKeys.Send(Key.Down);
            //Window_KeyUp(this, SendKeys.E1);
        }

        public void MouseSim(Point start)
        {
            
            var sim = new InputSimulator();
            for (int i = 1; i < 10; i++)
            {
                sim.Mouse.MoveMouseBy(i * -5, i);
                sim.Mouse.Sleep(50);
            }
            sim.Mouse.RightButtonClick();
            sim.Mouse.Sleep(1000);
            for (int i = 1; i < 10; i++)
            {
                sim.Mouse.MoveMouseBy(i * 5, i);
                sim.Mouse.Sleep(50);
            }
            sim.Mouse.RightButtonClick();
            sim.Mouse.Sleep(1000);
            for (int i = 1; i < 10; i++)
            {
                sim.Mouse.MoveMouseBy(i, i * -5);
                sim.Mouse.Sleep(50);
            }
            sim.Mouse.RightButtonClick();
            sim.Mouse.Sleep(1000);
            for (int i = 1; i < 10; i++)
            {
                sim.Mouse.MoveMouseBy(i, i * 5);
                sim.Mouse.Sleep(50);
            }
            sim.Mouse.RightButtonClick();
            sim.Mouse.Sleep(1000);
            for (int i = 1; i < 8; i++)
            {
                sim.Mouse.MoveMouseBy(i * -5, i);
                sim.Mouse.Sleep(50);
            }
            sim.Mouse.RightButtonClick();
            sim.Mouse.Sleep(3000);
            for (int i = 1; i < 7; i++)
            {
                sim.Mouse.MoveMouseBy(i, i * -6);
                sim.Mouse.Sleep(50);
            }
            
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
