using System;
using System.Collections;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace CallOfCSharp
{
    public class Floor : CommonClass
    {
        int sizeX = 0;
        int sizeZ = 0;        

        public Floor(Device device, int sizeX, int sizeZ, CallOfCS cocs)
            : base(device,cocs,0,0,1)
        {
            this.sizeX = sizeX;
            this.sizeZ = sizeZ;
            vBuff = new VertexBuffer(typeof(CustomVertex.PositionNormalTextured), sizeX * sizeZ * 4, device, 0, CustomVertex.PositionNormalTextured.Format, Pool.Managed);            
            OnVertexBufferCreate(vBuff, null);
            this.inited = true;
        }

        public override void OnVertexBufferCreate(object sender, EventArgs e)
        {
            vBuff = (VertexBuffer)sender;
            this.verts = new CustomVertex.PositionNormalTextured[sizeX * sizeZ * 4];
            int foo = 0;
            for (int i = 0; i < sizeZ; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    verts[foo++] = new CustomVertex.PositionNormalTextured(j * size_of_one_rectangle, 0, i * size_of_one_rectangle, 0, 1, 0, 0, 1);
                    verts[foo++] = new CustomVertex.PositionNormalTextured(j * size_of_one_rectangle, 0, (i + 1) * size_of_one_rectangle, 0, 1, 0, 0, 0);
                    verts[foo++] = new CustomVertex.PositionNormalTextured((j + 1) * size_of_one_rectangle, 0, (i + 1) * size_of_one_rectangle, 0, 1, 0, 1, 0);
                    verts[foo++] = new CustomVertex.PositionNormalTextured((j + 1) * size_of_one_rectangle, 0, i * size_of_one_rectangle, 0, 1, 0, 1, 1);
                }
            }

            vBuff.SetData(verts, 0, LockFlags.None);
            indexy = new short[sizeX * sizeZ * 6];
            foo = 0;
            for (short i = 0; i < sizeX; i++)
            {
                for (short j = 0; j < sizeZ; j++)
                {
                    indexy[foo++] = (short)(i * sizeZ * 4 + j * 4);
                    indexy[foo++] = (short)(i * sizeZ * 4 + j * 4 + 2);
                    indexy[foo++] = (short)(i * sizeZ * 4 + j * 4 + 3);
                    indexy[foo++] = (short)(i * sizeZ * 4 + j * 4);
                    indexy[foo++] = (short)(i * sizeZ * 4 + j * 4 + 1);
                    indexy[foo++] = (short)(i * sizeZ * 4 + j * 4 + 2);
                }
            }
            iBuff = new IndexBuffer(typeof(short), indexy.Length, device, 0, Pool.Managed);
            iBuff.SetData(indexy, 0, LockFlags.None);            
            this.type = PrimitiveType.TriangleList;
            this.count = Math.Abs(sizeX * sizeZ * 2);
            this.useIBuff = true;
        }
    }
}