using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Collections;

namespace CallOfCSharp
{
    public class CommonClass
    {
        public CallOfCS cocs = null;
        public Device device = null;
        public CustomVertex.PositionNormalTextured[] verts = null;
        public short[] indexy = null;
        public IndexBuffer iBuff = null;
        public VertexBuffer vBuff = null;
        public float size_of_one_rectangle = 2;
        public int count = 0;
        public short textureIndex = 0;
        public Matrix transmatrix = Matrix.Translation(0, 0, 0);
        public Matrix tmpmatrix;
        public VertexFormats vertexFormat = CustomVertex.PositionNormalTextured.Format;
        public PrimitiveType type;
        public float rotSpeed = 0;
        public float angelY = 0;
        public bool inited = false;
        public bool useIBuff = false;
        public bool useTriangles = false;
        public Vector3 center;
        public ArrayList triangles = null;

        //position of bottom left corner of the object in our world
        public short posX = 0;
        public short posY = 0;
        public short posZ = 0;

        public CommonClass(Device device, CallOfCS cocs, short posX, short posZ, short textureindex)
        {
            this.device = device;
            this.cocs = cocs;
            this.size_of_one_rectangle = cocs.size_of_one_rectangle;
            this.posX = posX;
            this.posZ = posZ;
            this.textureIndex = textureindex;
        }
        /*
        public CommonClass(Device device, PrimitiveType type, int count, CustomVertex.PositionNormalTextured[] verts)
        {
            this.device = device;
            this.verts = verts;
            this.type = type;
            this.count = count;
            vBuff.Created += new EventHandler(this.OnVertexBufferCreate);
            device.DeviceReset += new EventHandler(this.OnVertexBufferCreate);
            OnVertexBufferCreate(vBuff, null);
            inited = true;
        }*/

        public virtual void setTransformations()
        {
            if (rotSpeed != 0)
                transmatrix *= Matrix.RotationY(Environment.TickCount * rotSpeed);
        }

        public float distanceFromPoint(Vector3 other)
        {
            return distanceFromPoint(other.X, other.Y, other.Z);
        }

        public float distanceFromCamera()
        {
            return distanceFromPoint(cocs.myPosX, cocs.myPosY, cocs.myPosZ);
        }

        public float distanceFromPoint(float x, float y, float z)
        {
            Vector3 currentCenter = new Vector3(center.X, center.Y, center.Z);
            currentCenter.TransformCoordinate(device.Transform.World);
            return (float)Math.Sqrt(Math.Pow(currentCenter.X - x, 2) + Math.Pow(currentCenter.Y - y, 2) + Math.Pow(currentCenter.Z - z, 2));
        }

        public float distanceFromCameraPlain()
        {
            Vector3 currentCenter = new Vector3(center.X, center.Y, center.Z);
            currentCenter.TransformCoordinate(device.Transform.World);
            currentCenter.TransformCoordinate(device.Transform.View);
            return currentCenter.Z;
        }

        // These virtual functions allow the child classes to override them - else these work

        public virtual void OnVertexBufferCreate(object sender, EventArgs e) { }

        public virtual void doAction(int Key) { }

        public virtual void Render()
        {
            if (!inited) return;            
            tmpmatrix = device.Transform.World;
            setTransformations();
            device.Transform.World *= transmatrix;
            device.VertexFormat = this.vertexFormat;
            device.RenderState.AlphaTestEnable = false;
            device.RenderState.ZBufferEnable = true;
            device.RenderState.AlphaBlendEnable = true;
            device.RenderState.DestinationBlend = Blend.InvSourceAlpha;
            device.RenderState.SourceBlend = Blend.SourceAlpha;
            device.SetTexture(0, cocs.myTextures[cocs.textureSet + textureIndex]);

            if (useIBuff)
            {
                device.SetStreamSource(0, vBuff, 0);
                device.Indices = iBuff;
                device.DrawIndexedPrimitives(type, 0, 0, indexy.Length, 0, count);
            }
            else if (useTriangles)
            {
                device.RenderState.CullMode = Cull.None;
                triangles.Sort();
                foreach (OneTriangle ot in triangles)
                {
                    device.SetStreamSource(0, ot.vBuff, 0);
                    device.DrawPrimitives(ot.type, 0, ot.count);                    
                }
                device.RenderState.CullMode = Cull.CounterClockwise;
            }
            else
            {
                device.SetStreamSource(0, vBuff, 0);
                device.DrawPrimitives(type, 0, count);
            }
            device.Transform.World = tmpmatrix;
        }

    }
}