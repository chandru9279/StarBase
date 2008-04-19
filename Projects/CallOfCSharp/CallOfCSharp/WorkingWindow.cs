using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;


namespace CallOfCSharp
{
    //Delegate for Custom Idle Event
    public delegate void CustomIdleEventHandler(WorkingWindow sender);
    //Implement Idisposable to write .dispose() function    
    public class WorkingWindow : IDisposable
    {
        //This provides the area where we show everything..
        public MainWindow form = null;
        private Size size;        
        public bool deviceLost = false;
        private Device device = null;
        private PresentParameters [] presentParameters = null;
        //For saving location and size after fullscreen back to windowed....
        private Point lastFrmLocation;
        private Size lastFrmSize;
        public Device Device
        { 
            get
            {
                if (device == null) throw new Exception("Device not created");
                return device;
            }
        }
        public Size Size
        {
            get
            {return size;}
            set
            {
                size = value;
                form.ClientSize = value;
            }
        }
        public event CustomIdleEventHandler Idle;              
        
        //this is how u call an redirect to an overloaded constructor.. this calls the next one
        /*public WorkingWindow():this(100, 100, 100, 100, "Call Of CSharp")
        { }*/

        public WorkingWindow(int width, int height, int left, int top, string title)
        {
            form = new MainWindow();
            form.Text = title;
            Size = new Size(width, height);
            form.Location = new Point(left, top);
            lastFrmSize = form.Size;
            lastFrmLocation = form.Location;
            
        }

        public void CreateDevice()
        {
            DisplayMode DispMode = Manager.Adapters[Manager.Adapters.Default.Adapter].CurrentDisplayMode;
            presentParameters = new PresentParameters[1];
            presentParameters[0] = new PresentParameters();
            presentParameters[0].Windowed = true;
            presentParameters[0].BackBufferCount = 1;
            presentParameters[0].BackBufferFormat = DispMode.Format;
            presentParameters[0].BackBufferWidth = 0;
            presentParameters[0].BackBufferHeight = 0;
            presentParameters[0].SwapEffect = SwapEffect.Discard;
            presentParameters[0].EnableAutoDepthStencil = true;
            presentParameters[0].AutoDepthStencilFormat = DepthFormat.D16;
            presentParameters[0].PresentationInterval = PresentInterval.Immediate;
            device = new Device(0, DeviceType.Hardware, form, CreateFlags.SoftwareVertexProcessing, presentParameters);
            if (device == null) throw new Exception("Device not created");
            device.DeviceResizing += new CancelEventHandler(DoDeviceResizing);
        }

        protected virtual void DoDeviceResizing(object sender, CancelEventArgs e)
        {
            if (presentParameters[0].Windowed) e.Cancel = false;
            else e.Cancel = true;
        }

        public void SwitchToFullscreen()
        {
            if (!presentParameters[0].Windowed) return;

            lastFrmLocation = form.Location;
            lastFrmSize = form.Size;

            form.TopMost = true;
            form.FormBorderStyle = FormBorderStyle.None;

            DisplayMode DispMode = Manager.Adapters[Manager.Adapters.Default.Adapter].CurrentDisplayMode;            
            presentParameters[0].Windowed = false;
            presentParameters[0].BackBufferCount = 1;
            presentParameters[0].BackBufferFormat = DispMode.Format;
            presentParameters[0].BackBufferWidth = DispMode.Width;
            presentParameters[0].BackBufferHeight = DispMode.Height;
            presentParameters[0].SwapEffect = SwapEffect.Discard;
            presentParameters[0].EnableAutoDepthStencil = true;
            presentParameters[0].AutoDepthStencilFormat = DepthFormat.D16;
            presentParameters[0].PresentationInterval = PresentInterval.Immediate;

            Size = new Size(DispMode.Width, DispMode.Height);
            device.Reset(presentParameters);
        }

        public void SwitchToWindowed()
        {
            if (presentParameters[0].Windowed) return;
            DisplayMode DispMode = Manager.Adapters[Manager.Adapters.Default.Adapter].CurrentDisplayMode;            
            presentParameters[0].Windowed = true;
            presentParameters[0].BackBufferCount = 1;
            presentParameters[0].BackBufferFormat = Format.Unknown;
            presentParameters[0].BackBufferWidth = 0;
            presentParameters[0].BackBufferHeight = 0;
            presentParameters[0].SwapEffect = SwapEffect.Discard;
            presentParameters[0].EnableAutoDepthStencil = true;
            presentParameters[0].AutoDepthStencilFormat = DepthFormat.D16;
            presentParameters[0].PresentationInterval = PresentInterval.Immediate;
            device.Reset(presentParameters);
            form.TopMost = false;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.Size = lastFrmSize;
            form.Location = lastFrmLocation;
        }

        public void Run()
        {
            form.Show();
            while (form.Created)
            {
                if (Idle != null && CheckRender())
                {
                    Idle(this);//raise of custom event Idle which modifies the back buffer.
                    Present();
                }
                Application.DoEvents(); //processes the events in queue
            }
        }
        
        //shows the next buffer contents, in the sequence of backbuffers this device has. 
        private void Present()
        {
            try
            {
                if (device != null) device.Present();
            }
            catch (DeviceLostException)
            {
                deviceLost = true;
            }
        }

        private bool CheckRender()
        {        
            int code;
            device.CheckCooperativeLevel(out code);
            switch ((ResultCode)code)
            {
                case ResultCode.DeviceLost:
                    System.Threading.Thread.Sleep(20);
                    return false;
                case ResultCode.DeviceNotReset:
                    try
                    {
                        if (!presentParameters[0].Windowed)
                            Size = new Size(presentParameters[0].BackBufferWidth, presentParameters[0].BackBufferHeight);
                        device.Reset(presentParameters);//maycause DeviceLostException exception
                        deviceLost = false;
                    }
                    catch (DeviceLostException)
                    {
                        deviceLost = true;
                    }
                    return true;
            }
            return true;
        }

        public void Dispose()
        {
            form.Close();
        }
    }
}