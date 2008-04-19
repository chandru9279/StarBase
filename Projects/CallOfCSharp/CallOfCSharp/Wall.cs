using System;
using System.Collections;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;


namespace CallOfCSharp
{
	public class Wall : CommonClass
	{

		private bool inAction = false;
		private bool dole = false;
		private float vyska = 1.5f;
		private int start = 0;
		private Vector3 realPos;

        public Wall(Device device, CallOfCS cocs, short posX, short posZ)
            : base(device, cocs, posX, posZ, 2)
		{
			this.posY = 0;
			this.realPos = new Vector3(posX*size_of_one_rectangle,posY*size_of_one_rectangle,posZ*size_of_one_rectangle);
			this.center = new Vector3((posX+0.5f)*size_of_one_rectangle,size_of_one_rectangle*vyska*0.5f,(posZ+0.5f)*size_of_one_rectangle);
			
			this.rotSpeed = 0.001f;
			vBuff = new VertexBuffer(typeof(CustomVertex.PositionNormalTextured), 24, device, Usage.None, CustomVertex.PositionNormalTextured.Format, Pool.Managed);
			OnVertexBufferCreate(vBuff,null);
			this.inited = true;
		}

		public override void setTransformations()
		{
			if(inAction){			
			cocs.aArea[posX,posZ] = false;

				if(!dole)
				{
					realPos.Y = rotSpeed*(start - System.Environment.TickCount);

					if(realPos.Y < -vyska*size_of_one_rectangle)
					{
						inAction = false;
						dole = true;
						cocs.aArea[posX,posZ] = true;
					}
				
				}
				else {
					realPos.Y = -vyska*size_of_one_rectangle - rotSpeed*(start - System.Environment.TickCount);

					if(realPos.Y > 0)
					{
						realPos.Y = 0;
						inAction = false;
						dole = false;						
					}
				}
			}
			transmatrix = Matrix.Scaling(size_of_one_rectangle,size_of_one_rectangle*vyska,size_of_one_rectangle);
			transmatrix *= Matrix.Translation(realPos);

		}


		public override void OnVertexBufferCreate(object sender, EventArgs e)
		{
			vBuff = (VertexBuffer) sender;
			
			this.verts = new CustomVertex.PositionNormalTextured[16];
			// front face                                         x      y      z      nx     ny     nz    tu    tv
			verts[0]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f, 0.0f, 1.0f);
			verts[1]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  0.0f,  0.0f,  0.0f, -1.0f, 1.0f, 0.0f);
			verts[2]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f, 1.0f, 1.0f);   
			
			verts[3]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  0.0f,  0.0f,  0.0f, -1.0f, 0.0f, 0.0f);    
			   
			// right face
			verts[4]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f, 1.0f);
			verts[5]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  1.0f,  1.0f,  0.0f,  0.0f, 1.0f, 0.0f);
			verts[6]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  1.0f,  1.0f,  0.0f,  0.0f, 1.0f, 1.0f);   
			
			verts[7]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f, 0.0f); 
			// back face
			verts[8]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f);
			verts[9]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f, 0.0f);
			verts[10]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f, 1.0f);   
			
			verts[11]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f, 0.0f);
			// left face
			verts[12]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  1.0f, -1.0f,  0.0f,  0.0f, 0.0f, 1.0f);
			verts[13]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f, 0.0f);
			verts[14]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f, 1.0f);   
			
			verts[15]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  1.0f, -1.0f,  0.0f,  0.0f, 0.0f, 0.0f);
//			// top face
//			verts[16]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f, 1.0f);
//			verts[17]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  1.0f,  0.0f,  1.0f,  0.0f, 1.0f, 0.0f);
//			verts[18]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f, 1.0f);   
//			
//			verts[19]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  1.0f,  0.0f,  1.0f,  0.0f, 0.0f, 0.0f);
//			// bottom face
//			verts[20]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  1.0f,  0.0f, -1.0f,  0.0f, 0.0f, 1.0f);
//			verts[21]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f, 0.0f);
//			verts[22]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  1.0f,  0.0f, -1.0f,  0.0f, 1.0f, 1.0f);   
//			
//			verts[23]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f, 0.0f);
					
			vBuff.SetData(verts,0,LockFlags.None);
			indexy = new short[24];
			short foo = 0;
			for(short i = 0; i < 4; i++)
			{
				indexy[foo++] = (short) (i*4);
				indexy[foo++] = (short) (i*4 + 1);
				indexy[foo++] = (short) (i*4 + 2);
				indexy[foo++] = (short) (i*4);
				indexy[foo++] = (short) (i*4 + 3);
				indexy[foo++] = (short) (i*4 + 1);
			}
			iBuff = new IndexBuffer(typeof(short), indexy.Length, device, 0 /* Usage.Dynamic | Usage.WriteOnly*/, Pool.Managed);
			iBuff.SetData(indexy,0,LockFlags.None);			
			this.type = PrimitiveType.TriangleList;
			this.count = indexy.Length/3;
			this.useIBuff = true;
		}


		public override void doAction(int Key)
		{
			if(Key == 1){
				if((this.distanceFromCamera() < 2f*size_of_one_rectangle) && !inAction)
				{
					if(!dole || ( (posX!=Math.Floor(cocs.myPosX/size_of_one_rectangle) ) || (posZ != Math.Floor(cocs.myPosZ/size_of_one_rectangle)) )){
					inAction = true;
						start = System.Environment.TickCount;
					}
				}
				}
		
		}



	}
}
