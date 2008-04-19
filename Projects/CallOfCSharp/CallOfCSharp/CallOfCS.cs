using System;
using System.Drawing;
using System.Collections;
using System.Windows;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectSound;


namespace CallOfCSharp
{
    public class CallOfCS
    {

        Microsoft.DirectX.Direct3D.Device device = null;
        public Microsoft.DirectX.DirectSound.Device dsDevice = null;
        public Microsoft.DirectX.DirectSound.SecondaryBuffer[] mySounds;        
        public WorkingWindow workingwindow; 

        public Texture[] myTextures;
        public Light[] myLights;
        Material material;

        public short textureSet = 0; // Texture Set (see loadTextures() method)
        bool lighting = true;
        public float size_of_one_rectangle = 2;
        public int frameRate = 0;
        int filter = 1; // 0 nna nearest neighbour, 1 nna linear, 2 nna mipmapping)
        //initial view angle
        public float telescope = 80;
        public float telescopeMax = 80;
        public float telescopeMin = 10;
        //size of map
        public short mapSizeX = 1;
        public short mapSizeZ = 1;
        public float mapRealSizeX = 1;
        public float mapRealSizeZ = 1;
        // camera pos in world
        public float myPosX = 0;
        public float myPosY = 1.8f;
        public float myPosZ = -5;
        //camera rotation
        public float angleZX = 0;//angle between Z+ and X+
        public float angleZY = 0;//Z+ and Y+
        public float angleXY = 0;//X+ and Y+
        //number of collected items
        public short items = 0;
        public short numberOfItems = 0; //items in the world
        //number of targets
        public short targets = 0;
        public short numberOfTargets = 0;
        //list of Objects that can do something in relation with distance from camera
        public ArrayList actionObjects = new ArrayList();
        //list of Targets
        public ArrayList allTargets = new ArrayList();        
        //for mouse moving
        public int centerX = 0;
        public int centerY = 0;
        public short upDownMovingDirection = 1; 
        //collision  matrices
        public Matrix futurePos;
        public Matrix tmpMatrix;
        public float colDist = 0.2f;
        public Microsoft.DirectX.Direct3D.Font font;
        public float rotSpeed = 0.003f;
        public float znearPlane = 0.1f;
        public float transspeed = 0.01f;
        Matrix myView = Matrix.Translation(0, 0, 0);
        //2D map of accessible points
        public bool[,] aArea;        
        int lastFrame = 0;
        int numberOfFrames = 0;        
        public ArrayList universe = new ArrayList();
        public ArrayList toRemoveFromUniverse = new ArrayList();
        /*
         * List of used keys
         * and times
         */
        public bool[] pressedKeys = new bool[256];
        public int[] pressedKeysTimes = new int[256];        
        public int lastShot = 0;
        public Sprite mySprite;
        int startTime;
        string yourtime = "";
        
        [STAThread]
        static void Main(string[] Args)
        {            
            CallOfCS game = new CallOfCS();        
            //device
            game.workingwindow = new WorkingWindow(800, 600, 0, 0, "Call Of CSharp");            
            game.workingwindow.CreateDevice();
            game.device = game.workingwindow.Device;
            //sound
            game.dsDevice = new Microsoft.DirectX.DirectSound.Device();
            game.dsDevice.SetCooperativeLevel(game.workingwindow.form, CooperativeLevel.Priority);            
            //init            
            game.Init();
            //cursor setup
            game.centerX = game.workingwindow.Size.Width / 2;
            game.centerY = game.workingwindow.Size.Height / 2;
            Cursor.Position = new Point(game.workingwindow.form.Left + game.centerX, game.workingwindow.form.Top + game.centerY);
            Cursor.Hide();                        
            game.workingwindow.form.KeyDown += new System.Windows.Forms.KeyEventHandler(game.kDown);
            game.workingwindow.form.KeyUp += new System.Windows.Forms.KeyEventHandler(game.kUp);            
            game.workingwindow.Idle += new CustomIdleEventHandler(game.idle);
            game.workingwindow.form.MouseMove += new System.Windows.Forms.MouseEventHandler(game.mMove);
            game.workingwindow.form.MouseDown += new System.Windows.Forms.MouseEventHandler(game.mbDown);
            game.workingwindow.form.MouseUp += new System.Windows.Forms.MouseEventHandler(game.mbUp);
            // Run
            game.workingwindow.Run();
        }

        public void Init()
        {
            new MapGetter().ReadFromFile("TheFirstMap.txt", device, this);                        
            loadTextures();            
            initSounds();
            for (short i = 0; i < pressedKeys.Length; i++)
                pressedKeys[i] = false;
            font = new Microsoft.DirectX.Direct3D.Font(device, new System.Drawing.Font("Arial", 12f));            
            myPosY = 0.9f * size_of_one_rectangle;
            colDist = (znearPlane / (float)Math.Sin((telescopeMax * 0.5 * (float)Math.PI) / 180)) + transspeed;           
            material = new Material();
            material.Diffuse = Color.White;
            material.Specular = Color.White;
            material.Ambient = Color.White;
            myView = Matrix.Translation(-myPosX, -myPosY, -myPosZ);
            lastFrame = Environment.TickCount;
            initSprite();
            startTime = System.Environment.TickCount;
        }

        public void idle(WorkingWindow o)
        {            
            device.BeginScene();            
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
            moveCamera();
            device.Transform.View = myView;            
            device.Transform.Projection = Matrix.PerspectiveFovLH(telescope * (float)Math.PI / 180, o.Size.Width / (float)o.Size.Height, znearPlane, (float)Math.Abs(Math.Sqrt(Math.Pow(mapRealSizeX, 2) + Math.Pow(mapRealSizeZ, 2))));            
            device.RenderState.ShadeMode = ShadeMode.Gouraud;
            device.RenderState.Lighting = lighting;
            setLights();           
            switch (filter)
            {
                case 0:
                    device.SamplerState[0].MinFilter = TextureFilter.Point;
                    device.SamplerState[0].MagFilter = TextureFilter.Point;
                    device.SamplerState[0].MipFilter = TextureFilter.None;
                    break;
                case 1:
                    device.SamplerState[0].MinFilter = TextureFilter.Linear;
                    device.SamplerState[0].MagFilter = TextureFilter.Linear;
                    device.SamplerState[0].MipFilter = TextureFilter.None;
                    break;
                case 2:
                    device.SamplerState[0].MinFilter = TextureFilter.Linear;
                    device.SamplerState[0].MagFilter = TextureFilter.Linear;
                    device.SamplerState[0].MipFilter = TextureFilter.Linear;
                    break;
            }
            device.Material = material;            
            foreach (CommonClass cc in actionObjects)
                cc.doAction(-1);
            foreach (CommonClass cc in universe)            
                if (cc != null)
                    cc.Render();
            
            //Remove Bullets and collected items etc. from remove list
            foreach (CommonClass cc in toRemoveFromUniverse)
            {
                universe.Remove(cc);
                actionObjects.Remove(cc);
                allTargets.Remove(cc);
            }
            toRemoveFromUniverse.Clear();

            //Display framerate (Idle events triggered per second)
            if ((Environment.TickCount - lastFrame) > 100)
            {
                frameRate = numberOfFrames * 1000 / (Environment.TickCount - lastFrame);
                lastFrame = Environment.TickCount;
                numberOfFrames = 0;
            }

            if (telescope == telescopeMin)
            {
                mySprite.Begin(SpriteFlags.AlphaBlend);                
                device.SetTexture(0, myTextures[textureSet + 4]);
                mySprite.Draw2D(myTextures[textureSet + 4], Rectangle.Empty, new Rectangle(0, 0, workingwindow.form.Width, workingwindow.form.Height), new Point(0, 0), 0f, new Point(0, 0), Color.White.ToArgb());
                mySprite.End();
            }
            else if (angleZY <= -0.75f)
            {
                mySprite.Begin(SpriteFlags.AlphaBlend);
                device.SetTexture(0, myTextures[textureSet + 5]);
                mySprite.Draw2D(myTextures[textureSet + 5], Rectangle.Empty, new Rectangle(0, 0, workingwindow.Size.Width, workingwindow.Size.Height), new Point(0, 0), 0f, new Point(0, (int)Math.Round(0.245f * workingwindow.Size.Width * ((-1.2f - angleZY) / -0.45f))), Color.White.ToArgb());
                mySprite.End();
            }

            mySprite.Begin(SpriteFlags.AlphaBlend);
            if (numberOfItems != 0 || numberOfTargets != 0)
            {
                int foo = System.Environment.TickCount - startTime;
                yourtime = " Time: " + foo.ToString();
            }
            font.DrawText(null, frameRate.ToString() + " Items: " + items + "/" + numberOfItems + " Targets: " + targets + "/" + numberOfTargets + yourtime, 0, 0, Color.White);
            mySprite.End();
            numberOfFrames++;
            device.EndScene();
        }

        public void initSprite()
        {
            mySprite = new Sprite(this.device);
        }

        public void initSounds()
        {

            mySounds = new Microsoft.DirectX.DirectSound.SecondaryBuffer[10];
            BufferDescription bufferDescription = new BufferDescription();
            bufferDescription.ControlVolume = true;
            bufferDescription.ControlPan = true;
            bufferDescription.ControlFrequency = true;
            // Tells the primary buffer to continue & play this buffer, even if the app loses focus.            
            bufferDescription.GlobalFocus = true;
            try
            {
                mySounds[0] = new SecondaryBuffer("./sounds/Bullet.wav", bufferDescription, dsDevice);//shooting
            }
            catch (Microsoft.DirectX.DirectSound.SoundException)
            {
                System.Console.WriteLine("Sounds are missing....");
                System.Environment.Exit(0);
            }
        }


        //Tests if [x,z] is an accesible possition in aArea array
        public bool isAccesiblePoint(float x, float y, float z/*Matrix pointOfView*/)
        {
            byte numberOfDirections = 12;//See collisioncalculation.jpg
            bool isAccessible = true;
            float tmpX;
            float tmpZ;
            for (byte i = 0; i < numberOfDirections; i++)
            {
                if (!isAccessible)
                    return false;
                tmpX = x + (float)Math.Cos(2 * Math.PI * i / numberOfDirections) * colDist;
                tmpZ = z + (float)Math.Sin(2 * Math.PI * i / numberOfDirections) * colDist;
                if (tmpX >= mapRealSizeX)
                    return false;
                else if (tmpZ >= mapRealSizeZ)
                    return false;
                else if (tmpX <= 0)
                    return false;
                else if (tmpZ <= 0)
                    return false;
                else
                    isAccessible = aArea[(short)Math.Floor(tmpX / size_of_one_rectangle), (short)Math.Floor(tmpZ / size_of_one_rectangle)];
            }
            return true;
        }


        //Set Position of Camera
        public void moveCamera()
        {

            float tmpX = myPosX;
            float tmpZ = myPosZ;
            int time = System.Environment.TickCount;

            
            if (pressedKeys[(int)Keys.A])
            {
                tmpX = myPosX - (float)Math.Cos(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.A]) + znearPlane);
                tmpZ = myPosZ - (float)Math.Sin(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.A]) + znearPlane);

                if (isAccesiblePoint(tmpX, myPosY, tmpZ))
                {
                    myPosX = myPosX - (float)Math.Cos(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.A]));
                    myPosZ = myPosZ - (float)Math.Sin(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.A]));
                }
                else if (isAccesiblePoint(tmpX, myPosY, myPosZ))
                {
                    myPosX = myPosX - (float)Math.Cos(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.A]));
                }
                else if (isAccesiblePoint(myPosX, myPosY, tmpZ))
                {
                    myPosZ = myPosZ - (float)Math.Sin(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.A]));
                }

                pressedKeysTimes[(int)Keys.A] = time;
            }


            if (pressedKeys[(int)Keys.D])
            {
                tmpX = myPosX + (float)Math.Cos(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.D]) + znearPlane);
                tmpZ = myPosZ + (float)Math.Sin(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.D]) + znearPlane);


                if (isAccesiblePoint(tmpX, myPosY, tmpZ))
                {
                    myPosX = myPosX + (float)Math.Cos(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.D]));
                    myPosZ = myPosZ + (float)Math.Sin(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.D]));
                }
                else if (isAccesiblePoint(tmpX, myPosY, myPosZ))
                {
                    myPosX = myPosX + (float)Math.Cos(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.D]));
                }
                else if (isAccesiblePoint(myPosX, myPosY, tmpZ))
                {
                    myPosZ = myPosZ + (float)Math.Sin(angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.D]));
                }
                pressedKeysTimes[(int)Keys.D] = time;
            }

            
            if (pressedKeys[(int)Keys.W])
            {
                tmpX = myPosX + (float)Math.Sin(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.W]) + znearPlane);
                tmpZ = myPosZ + (float)Math.Cos(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.W]) + znearPlane);

                if (isAccesiblePoint(tmpX, myPosY, tmpZ))
                {
                    myPosX = myPosX + (float)Math.Sin(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.W]));
                    myPosZ = myPosZ + (float)Math.Cos(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.W]));
                }
                else if (isAccesiblePoint(tmpX, myPosY, myPosZ))
                {
                    myPosX = myPosX + (float)Math.Sin(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.W]));

                }
                else if (isAccesiblePoint(myPosX, myPosY, tmpZ))
                {
                    myPosZ = myPosZ + (float)Math.Cos(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.W]));
                }
                pressedKeysTimes[(int)Keys.W] = time;
            }


            if (pressedKeys[(int)Keys.S])
            {
                tmpX = myPosX - (float)Math.Sin(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.S]) + znearPlane);
                tmpZ = myPosZ - (float)Math.Cos(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.S]) + znearPlane);

                if (isAccesiblePoint(tmpX, myPosY, tmpZ))
                {
                    myPosX = myPosX - (float)Math.Sin(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.S]));
                    myPosZ = myPosZ - (float)Math.Cos(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.S]));
                }
                else if (isAccesiblePoint(tmpX, myPosY, myPosZ))
                {
                    myPosX = myPosX - (float)Math.Sin(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.S]));

                }
                else if (isAccesiblePoint(myPosX, myPosY, tmpZ))
                {
                    myPosZ = myPosZ - (float)Math.Cos(-angleZX) * (transspeed * (time - pressedKeysTimes[(int)Keys.S]));
                }
                pressedKeysTimes[(int)Keys.S] = time;
            }   
            myView = Matrix.Translation(-myPosX, -myPosY, -myPosZ);
            myView *= Matrix.RotationY(angleZX);
            myView *= Matrix.RotationX(angleZY);

        }

       
        public void mbDown(Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            shoot();
            Object pro = new Projectile(device, this, new Vector3(myPosX, myPosY * 0.7f, myPosZ), angleZX, angleZY);
            universe.Add(pro);
            actionObjects.Add(pro);
        }

        public void shoot()
        {
            if (mySounds[0].Status.Playing)
                mySounds[0].SetCurrentPosition(0);
            else
                mySounds[0].Play(0, BufferPlayFlags.Default);
        }

        public void mbUp(Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            pressedKeys[(int)Keys.ControlKey] = false;
        }

        
        public void kDown(Object o, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //Exit 
                case Keys.F4:
                    if (e.KeyCode == Keys.Alt)
                        workingwindow.Dispose();
                    break;
                case Keys.Escape:
                    workingwindow.Dispose();
                    break;
                //Switch to FullScreen
                case Keys.F1:
                    {
                        workingwindow.SwitchToFullscreen();
                        centerX = workingwindow.form.Width / 2;
                        centerY = workingwindow.form.Height / 2;

                    }
                    break;
                //Switch to Windowed Mode
                case Keys.F2:
                    {
                        this.workingwindow.SwitchToWindowed();
                        centerX = workingwindow.form.Width / 2;
                        centerY = workingwindow.form.Height / 2;
                    }
                    break;
                // L turn on/off lighting
                case Keys.L:
                    lighting = !lighting;
                    break;                
                //E triggers action
                case Keys.E:
                    foreach (CommonClass cc in actionObjects)
                    {
                        cc.doAction(1);
                    }
                    break;
                //F changes texture filtering
                case Keys.F:
                    {
                        filter += 1;
                        if (filter > 2) filter = 0;
                    }
                    break;
                //Change view angle
                case Keys.T:
                    {
                        if (telescope == telescopeMin)
                        {
                            telescope = telescopeMax;
                            rotSpeed = 0.003f;
                        }
                        else
                        {
                            telescope = telescopeMin;
                            rotSpeed = 0.0003f;
                        }                      
                    }
                    break;
                //Up Down Moving-Direction
                case Keys.U:
                    upDownMovingDirection *= -1;
                    break;
                default:
                    if (!pressedKeys[e.KeyValue])
                    {
                        pressedKeysTimes[e.KeyValue] = System.Environment.TickCount;
                        pressedKeys[e.KeyValue] = true;
                    }
                    break;
            }
        }

        public void kUp(Object o, System.Windows.Forms.KeyEventArgs e)
        {
            pressedKeys[e.KeyValue] = false;
        }
        
        private void loadTextures()
        {            
            myTextures = new Texture[30];

            try
            {
                short index = 0;
                myTextures[index++] = TextureLoader.FromFile(device, "./textures/Empty.jpg");//Empty

                //First set ( textureSet = 0 )
                myTextures[index++] = TextureLoader.FromFile(device, "./textures/Floor.jpg"); //Floor
                myTextures[index++] = TextureLoader.FromFile(device, "./textures/Wall.jpg"); //Wall
                myTextures[index++] = TextureLoader.FromFile(device, "./textures/Target.jpg");//Target
                myTextures[index++] = TextureLoader.FromFile(device, "./sprites/Gunsight.png");//Gunsight mask
                myTextures[index++] = TextureLoader.FromFile(device, "./sprites/SelfMask.png");//Self mask
                myTextures[index++] = TextureLoader.FromFile(device, "./textures/Bullet.png");//Bullet
                myTextures[index++] = TextureLoader.FromFile(device, "./textures/CubeFace.png");//CubeFace                                
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Textures are missing.....");
                System.Environment.Exit(0);
            }

            foreach (Texture ikx in myTextures)
            {
                if (ikx != null)
                    ikx.GenerateMipSubLevels();
            }


        }

        private void setLights()
        {
            myLights = new Light[3];
            //Light around player
            Light light = new Light();
            light.Diffuse = Color.White;
            light.Specular = Color.White;
            light.Type = LightType.Point;
            light.Attenuation0 = 0.7f;
            light.Position = new Vector3(myPosX, myPosY, myPosZ);            
            light.Range = 30.0f;
            myLights[0] = new Light();
            myLights[0].FromLight(light);
            //Flashlight
            light.Type = LightType.Spot;
            light.Diffuse = Color.Beige;
            light.Ambient = Color.Beige;
            light.Direction = new Vector3(-myView.M31, (float)Math.Sin(angleZY), myView.M33);
            light.Position = new Vector3(myPosX, myPosY, myPosZ);
            light.Falloff = 1;
            light.Range = 200;
            light.InnerConeAngle = 0.55f;
            light.OuterConeAngle = 1.3f;
            light.Attenuation1 = 0.01f;
            myLights[1] = new Light();
            myLights[1].FromLight(light);
            byte index = 0;
            foreach (Light l in myLights)
            {
                if (l != null)
                {
                    device.Lights[index].FromLight(l);
                    device.Lights[index++].Enabled = true;
                }
            }
        }

        //Mouse Move
        private void mMove(Object sender, System.Windows.Forms.MouseEventArgs m)
        {
            int deltaX = centerX - m.X;
            int deltaY = centerY - m.Y;
            if ((deltaX + deltaY) != 0)
            {
                angleZX += rotSpeed * deltaX;
                angleZY += upDownMovingDirection * rotSpeed * deltaY;
                if (angleZY > 1)
                    angleZY = 1;
                else if (angleZY < -1.2f)
                    angleZY = -1.2f;
                if (device.PresentationParameters.Windowed)                
                    Cursor.Position = new Point(Cursor.Position.X + deltaX, Cursor.Position.Y + deltaY);                
                else                
                    Cursor.Position = new Point(centerX, centerY); 
            }
        }

    
    } 
} 
