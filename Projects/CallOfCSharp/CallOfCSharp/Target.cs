using System;
using System.Collections;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;


namespace CallOfCSharp
{
	public class Target : CommonClass
	{
		float posXReal = 0;
		float posZReal = 0;
		int rand = 0;
		float height = 1f;
		public bool active = true;//Target is active till it's hit
		
		public  Target(Device device, CallOfCS cocs, short posX, short posZ)
            : base(device, cocs, posX, posZ, 3)
		{
			this.rotSpeed = 0.0005f;

			posXReal = (posX+0.5f)*size_of_one_rectangle;
			posZReal = (posZ+0.5f)*size_of_one_rectangle;
			System.Random foo = new Random(posX);
			rand = foo.Next(0,10000000);
			vBuff = new VertexBuffer(typeof(CustomVertex.PositionNormalTextured), 4, device, 0/*Usage.Dynamic | Usage.WriteOnly*/, CustomVertex.PositionNormalTextured.Format, Pool.Managed);//24 - je prasarna...
			//vBuff.Created += new EventHandler(this.OnVertexBufferCreate);
			//device.DeviceReset += new EventHandler(this.OnVertexBufferCreate);
			OnVertexBufferCreate(vBuff,null);

			this.inited = true;
		}

		public override void OnVertexBufferCreate(object sender, EventArgs e)
		{
			vBuff = (VertexBuffer) sender;
			//
			//================= pres iBuff-----------------

			//nejprve si vytvorime hejno bodu na ktere pak budeme odkazovat z ibufferu
			this.verts = new CustomVertex.PositionNormalTextured[4];
			
			// back face
			verts[0]  = new CustomVertex.PositionNormalTextured( 1.0f,  -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f);
			verts[1]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f, 0.0f);
			verts[2]  = new CustomVertex.PositionNormalTextured( 0.0f,  -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f, 1.0f);   
			
			verts[3]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f, 0.0f);  
			
					
			vBuff.SetData(verts,0,LockFlags.None);			
			//device.SetStreamSource(0,vBuff,0 );

			//points are created so we can prepare ibuffer

			indexy = new short[6];//fixme pevna velikost neni ok
			short foo = 0;
			indexy[foo++] = 0;
			indexy[foo++] = 3;
			indexy[foo++] = 1;
				indexy[foo++] = 0;
				indexy[foo++] = 1;
				indexy[foo++] = 2;
				
			
			iBuff = new IndexBuffer(typeof(short), indexy.Length, device, 0 /* Usage.Dynamic | Usage.WriteOnly*/, Pool.Managed);
			iBuff.SetData(indexy,0,LockFlags.None);
			//device.Indices = iBuff;
			this.type = PrimitiveType.TriangleList;
			this.count = indexy.Length/3;//fixme to by nemelo byt natvrdo
			this.useIBuff = true;


			//this.iBuff = new IndexBuffer();
			//=================konec pres iBuff-------*/
		}

		public override void setTransformations()
		{
			Vector3 foo = new Vector3(cocs.myPosX-posXReal,0,cocs.myPosZ-posZReal);
			foo.Normalize();
			transmatrix = Matrix.Translation(-0.5f,0,-0.5f);
			transmatrix *= Matrix.Scaling(size_of_one_rectangle*0.7f,size_of_one_rectangle*height,size_of_one_rectangle*0.7f);
			transmatrix *= Matrix.RotationY((float)Math.Atan2(   (cocs.myPosX-posXReal),   (cocs.myPosZ-posZReal)  ));

			this.center = new Vector3(   (posX)*size_of_one_rectangle + size_of_one_rectangle/2,  ((float)Math.Sin(   (rand+System.Environment.TickCount) *rotSpeed)+0f)*size_of_one_rectangle*height*0.5f    ,   (posZ)*size_of_one_rectangle + size_of_one_rectangle/2   );
			transmatrix *= Matrix.Translation(center);
			

			//
				
		}

	}
}
