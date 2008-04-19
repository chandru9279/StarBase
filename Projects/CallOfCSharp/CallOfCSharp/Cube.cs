using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;


namespace CallOfCSharp
{
	public class Cube : CommonClass
	{
		private float constSpeed = 0.0001f;
		private float rotation = 0;
		private int lastMove = 0;
		
		public Cube(Device device, CallOfCS cocs, short posX, short posZ) 
            : base(device,cocs,posX,posZ,7)
		{			
            this.center = new Vector3((posX + 0.5f) * size_of_one_rectangle, size_of_one_rectangle * 0.7f, (posZ + 0.5f) * size_of_one_rectangle);
            
			this.rotSpeed = constSpeed;
			lastMove = System.Environment.TickCount;
			triangles = new ArrayList();


			//Totally describe the 3D cube using three dimensional points
			this.verts = new CustomVertex.PositionNormalTextured[24];
			// Front face                                         x      y      z      nx     ny     nz    tu    tv
			verts[0]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f, 0.0f, 1.0f);
			verts[1]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  0.0f,  0.0f,  0.0f, -1.0f, 1.0f, 0.0f);
			verts[2]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f, 1.0f, 1.0f);   			
			verts[3]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  0.0f,  0.0f,  0.0f, -1.0f, 0.0f, 0.0f);    			 
			// Right face
			verts[4]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f, 1.0f);
			verts[5]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  1.0f,  1.0f,  0.0f,  0.0f, 1.0f, 0.0f);
			verts[6]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  1.0f,  1.0f,  0.0f,  0.0f, 1.0f, 1.0f);   			
			verts[7]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f, 0.0f); 
			// Back face
			verts[8]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f);
			verts[9]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f, 0.0f);
			verts[10]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f, 1.0f); 
			verts[11]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f, 0.0f);
			// Left face
			verts[12]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  1.0f, -1.0f,  0.0f,  0.0f, 0.0f, 1.0f);
			verts[13]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f, 0.0f);
			verts[14]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f, 1.0f); 
			verts[15]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  1.0f, -1.0f,  0.0f,  0.0f, 0.0f, 0.0f);			
			// Top face
			verts[16]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f, 1.0f);
			verts[17]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  1.0f,  0.0f,  1.0f,  0.0f, 1.0f, 0.0f);
			verts[18]  = new CustomVertex.PositionNormalTextured( 1.0f,  1.0f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f, 1.0f); 
			verts[19]  = new CustomVertex.PositionNormalTextured( 0.0f,  1.0f,  1.0f,  0.0f,  1.0f,  0.0f, 0.0f, 0.0f);
			// Bottom face
			verts[20]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  1.0f,  0.0f, -1.0f,  0.0f, 0.0f, 1.0f);
			verts[21]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f, 0.0f);
			verts[22]  = new CustomVertex.PositionNormalTextured( 1.0f,  0.0f,  1.0f,  0.0f, -1.0f,  0.0f, 1.0f, 1.0f); 
			verts[23]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f, 0.0f);
			for(short i = 0; i < 6; i++)
			{
				triangles.Add(new OneTriangle(device,cocs,verts[(i*4)],verts[(i*4 + 1)],verts[(i*4 + 2)]) );
				triangles.Add(new OneTriangle(device,cocs,verts[(i*4)],verts[(i*4 + 3)],verts[(i*4 + 1)]) );
			}
			this.useTriangles = true;
			this.inited = true;
		}		
		
		public override void setTransformations()
		{		
			rotSpeed = constSpeed * (float)Math.Pow(this.distanceFromCamera(),1.6f);
			rotation += rotSpeed * (System.Environment.TickCount - lastMove);
            lastMove = System.Environment.TickCount;		
			transmatrix = Matrix.Translation(-0.5f,-0.4f,-0.5f);
            transmatrix *= Matrix.Scaling(size_of_one_rectangle * 0.5f, size_of_one_rectangle * 0.5f, size_of_one_rectangle * 0.5f);
			transmatrix *= Matrix.RotationY(rotation);
			transmatrix *= Matrix.RotationX(rotation * 0.5f);
            transmatrix *= Matrix.Translation((posX) * size_of_one_rectangle + size_of_one_rectangle / 2, 0.7f * size_of_one_rectangle, (posZ) * size_of_one_rectangle + size_of_one_rectangle / 2);
		}

		public override void doAction(int Key)
        {
            if ((Key==1) &&(this.distanceFromCamera() < cocs.size_of_one_rectangle))
            {
				cocs.items++;
				cocs.numberOfItems--;
				cocs.toRemoveFromUniverse.Add(this);
			}
		}
	}
}
