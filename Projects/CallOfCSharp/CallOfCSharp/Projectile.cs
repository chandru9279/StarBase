using System;
using System.Collections;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;


namespace CallOfCSharp
{
	public class Projectile : CommonClass
	{

		Vector3 position;
		Vector3 direction;
		float angleZX = 0;
		float angleZY = 0;
		float speed = 0.015f;		
		int startTime;

        public Projectile(Device device, CallOfCS cocs, Vector3 position, float angleZX, float angleZY)
            : base(device, cocs, 0, 0,6)
		{
			startTime = System.Environment.TickCount;			
			this.position = position;
			this.angleZX = angleZX;
			this.angleZY = angleZY;
			this.direction = new Vector3((float)-Math.Sin(angleZX),(float) Math.Sin(angleZY),(float) Math.Cos(angleZX));
			this.direction.Normalize();

			this.rotSpeed = 0.008f;



			vBuff = new VertexBuffer(typeof(CustomVertex.PositionNormalTextured), 18, device, 0/*Usage.Dynamic | Usage.WriteOnly*/, CustomVertex.PositionNormalTextured.Format, Pool.Managed);//24 - je prasarna...
		
			OnVertexBufferCreate(vBuff,null);

			this.inited = true;
		}


		public override void OnVertexBufferCreate(object sender, EventArgs e)
		{
			vBuff = (VertexBuffer) sender;
		
			

			//nejprve si vytvorime hejno bodu na ktere pak budeme odkazovat z ibufferu
			this.verts = new CustomVertex.PositionNormalTextured[18];
			short foo = 0;
			// top                                         x      y      z      nx     ny     nz    tu    tv
			verts[foo++]  = new CustomVertex.PositionNormalTextured( -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 0.0f, 1.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f, 1.0f, 0.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.5f,  0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f, 1.0f);   
			
			//bottom                                         x      y      z      nx     ny     nz    tu    tv
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.5f,  -0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 0.0f, 1.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f, 1.0f, 0.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( -0.5f,  -0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f, 1.0f);   
			
			//left                                                      x      y      z      nx     ny     nz    tu    tv
			verts[foo++]  = new CustomVertex.PositionNormalTextured( -0.5f,  -0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 0.0f, 1.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f, 1.0f, 0.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f, 1.0f);
					
			//right                                                      x      y      z      nx     ny     nz    tu    tv
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.5f,  0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 0.0f, 1.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.0f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f, 1.0f, 0.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.5f,  -0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f, 1.0f);

			//front1                                                      x      y      z      nx     ny     nz    tu    tv
			verts[foo++]  = new CustomVertex.PositionNormalTextured( -0.5f,  -0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 0.0f, 1.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f, 0.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.5f,  0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f, 1.0f);

			//front2                                                      x      y      z      nx     ny     nz    tu    tv
			verts[foo++]  = new CustomVertex.PositionNormalTextured( -0.5f,  -0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 0.0f, 1.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.5f,  0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f, 0.0f);
			verts[foo++]  = new CustomVertex.PositionNormalTextured( 0.5f,  -0.5f,  0.0f,  0.0f,  1.0f, 0.0f, 1.0f, 1.0f);

			vBuff.SetData(verts,0,LockFlags.None);			

			this.type = PrimitiveType.TriangleList;
			this.count = verts.Length/3;

		
		}

		public override void setTransformations()
		{
			transmatrix = Matrix.Scaling(0.2f,0.2f,1);

			transmatrix *= Matrix.RotationZ(rotSpeed * System.Environment.TickCount);

			transmatrix *= Matrix.RotationX(-angleZY);
			transmatrix *= Matrix.RotationY(-angleZX);
			
			transmatrix *= Matrix.Translation(position);
			Vector3 foo = direction;
			foo.Multiply(speed*(System.Environment.TickCount - startTime));
			transmatrix *= Matrix.Translation(foo);
			foo.Add(position);
			this.center = foo;

			if(System.Environment.TickCount - startTime > 10000)
				cocs.toRemoveFromUniverse.Add(this);


			//
				
		}


		public override void doAction(int Key){
			foreach(Target ikx in cocs.allTargets){
				if(this.center.Y > 0){
				Vector3 foo = new Vector3(this.center.X,this.center.Y,center.Z);
				foo.TransformCoordinate(device.Transform.World);

					if(ikx.distanceFromPoint(foo) < 0.5f*size_of_one_rectangle){
						if(ikx.active){
						cocs.toRemoveFromUniverse.Add(ikx);
						ikx.active = false;
						cocs.aArea[ikx.posX,ikx.posZ] = true;
						cocs.targets++;
						cocs.numberOfTargets--;
						}
					cocs.toRemoveFromUniverse.Add(this);
					}
				}


			}
		
		}
	}
}
