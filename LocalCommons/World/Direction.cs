// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System;

namespace LocalCommons.World
{
	/// <summary>
	/// Describes the direction an entity is looking at.
	/// </summary>
	public struct Direction
	{
	    /// <summary>
	    /// X rotate (left/right).
	    /// </summary>
	    public readonly short X;

        /// <summary>
        /// Y rotate (up/down).
        /// </summary>
        public readonly short Y;

        /// <summary>
        /// Z rotate (depth).
        /// </summary>
        public readonly short Z;

        public Direction(short x, short y, short z)
        {
            X = x;
            Y = y;
            Z = z;
        }

	    public Direction(Direction rot)
	    {
	        X = rot.X;
	        Y = rot.Y;
	        Z = rot.Z;
	    }

        /// <summary>
        /// Returns new position with X, Y, and Z being 0.
        /// </summary>
        public static Direction Zero => new Direction(0, 0, 0);

	 //   public readonly float Cos;
	 //   public readonly float Sin;

  //      /// <summary>
  //      /// Creates new direction from values.
  //      /// </summary>
  //      /// <param name="cos"></param>
  //      /// <param name="sin"></param>
  //      public Direction(float cos, float sin)
		//{
		//	Cos = cos;
		//	Sin = sin;
		//}

		///// <summary>
		///// Creates new direction from direction.
		///// </summary>
		///// <param name="dir"></param>
		//public Direction(Direction dir)
		//{
		//	Cos = dir.Cos;
		//	Sin = dir.Sin;
		//}

		///// <summary>
		///// Creates direction from degree, e.g. 0~360,
		///// going anti-clockwise, 0 being down.
		///// </summary>
		///// <param name="degree"></param>
		//public Direction(double degree)
		//{
		//	degree -= 45;
		//	degree *= Math.PI / 180.0;

		//	Cos = (float)Math.Cos(degree);
		//	Sin = (float)Math.Sin(degree);
		//}
	}
}
