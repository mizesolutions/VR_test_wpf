using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using VRapper.GdiLibrary;
using VRapper.HidLibrary;

namespace VR_test_wpf
{
    class PVRReader
    {
        private HidDevice _hidDevice;
        public string Path { get; set; }

        public PVRReader()
        {
            _hidDevice = HidDevices.GetDevice("\\\\?\\hid#vid_0483&pid_0021#7&1b9e8dbf&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}");
            try
            {
                Path = _hidDevice.DevicePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Opening Device:\r\n" + ex, "HID Error");
            }
        }

        public string GetHidDeviceList()
        {
            string temp = "";
            List<HidDevice> pvr = HidDevices.Enumerate().ToList();
            foreach (var hd in pvr)
            {
                temp += hd + " \n";
            }

            return temp;
            //return hidDevices.GetType().FullName;
            //DisplayDevices.EnumerateDevices().ToList();
        }

        public string GetDisplayDeviceList()
        {
            string temp = "";
            List<DisplayDevice> pvr = DisplayDevices.EnumerateDevices().ToList();
            foreach (var dd in pvr)
            {
                temp += dd + " \n";
            }

            return temp;
            //return hidDevices.GetType().FullName;
            //DisplayDevices.EnumerateDevices().ToList();
        }

        public string GetDevice()
        {
            return Path;
        }

        public void OpenDevice()
        {
            try
            {
                _hidDevice.OpenDevice();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Opening HID: \r\n" + ex, "HID Open Error");
            }
        }

        public string IsOpen()
        {
            return _hidDevice.IsOpen.ToString();
        }

        public string CreateReport()
        {
            try
            {
                return _hidDevice.CreateReport().ToString();
            }
            catch (Exception ex)
            {
                return "Report error: \r\n" + ex;
            }
            
        }

    }
}
