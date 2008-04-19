using System;
using System.Collections;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;



namespace CallOfCSharp
{
	public class OneTriangle : CommonClass, IComparable
	{
		public OneTriangle(Device device, CallOfCS cocs, CustomVertex.PositionNormalTextured first, CustomVertex.PositionNormalTextured second, CustomVertex.PositionNormalTextured third)
            : base(device,cocs,0,0,0)
		{		
			verts = new CustomVertex.PositionNormalTextured[3];
			verts[0] = first;
			verts[1] = second;
			verts[2] = third;            
			this.center = new Vector3((first.X+second.X+third.X)/3,(first.Y+second.Y+third.Y)/3,(first.Z+second.Z+third.Z)/3);
			vBuff = new VertexBuffer(typeof(CustomVertex.PositionNormalTextured), 3, device, Usage.None, CustomVertex.PositionNormalTextured.Format, Pool.Managed);
			vBuff.SetData(verts,0,LockFlags.None);
			this.count = 1;
			this.type = PrimitiveType.TriangleList;			
		}
		public int CompareTo(Object second){
			OneTriangle s = (OneTriangle) second;
			if(this.distanceFromCameraPlain() > s.distanceFromCameraPlain())
					return -1;
			else if (this.distanceFromCameraPlain() == s.distanceFromCameraPlain())
					return 0;
			else
					return 1;
		}
	}
}