//
// ModelObject.cs
// RhinoMobile.Model
//
// Created by dan (dan@mcneel.com) on 9/19/2013
// Copyright 2013 Robert McNeel & Associates.  All rights reserved.
// OpenNURBS, Rhinoceros, and Rhino3D are registered trademarks of Robert
// McNeel & Associates.
//
// THIS SOFTWARE IS PROVIDED "AS IS" WITHOUT EXPRESS OR IMPLIED WARRANTY.
// ALL IMPLIED WARRANTIES OF FITNESS FOR ANY PARTICULAR PURPOSE AND OF
// MERCHANTABILITY ARE HEREBY DISCLAIMED.
//
using System;
using System.Diagnostics;
using System.Collections.Generic;

using Rhino.Geometry;
using Rhino.DocObjects;
using RhinoMobile.Display;

namespace RhinoMobile.Model
{
	/// <summary>
	/// This is the base class for ModelMeshes, ModelInstanceDefs and ModelInstanceRefs
	/// This class wraps DocObjects in the 3dm file and associates all attributes and information
	/// with this specific object.
	/// </summary>
	public class ModelObject : IDisposable
	{
		#region properties
		/// <value> The identifier of the object in the database. </value>
		public Guid ObjectId { get; protected set; }
	
		/// <value> The layerIndex of this object in the model. </value>
		public int LayerIndex { get; set; }

		/// <value> True if the object is not hidden. </value>
		public bool Visible { get; set; }

		/// <value> As the base object for this class, this will always return 0. </value>
		public uint TriangleCount { get { return 0; } }

		/// <value> The DisplayMesh associated with this ModelObject </value>
		public DisplayMesh Mesh { get; protected set; }

		/// <value> The transform associated with this ModelObject. </value>
		public Transform XForm { get; protected set; }

		/// <value> The material applied to this model object. </value>
		public Material Material { get; set; }
		#endregion

		#region constructors and disposal
		public ModelObject ()
		{
			ObjectId = Guid.Empty;
			LayerIndex = -1;
			Visible = true;
		}
			
		/// <summary>
		/// Passively reclaims unmanaged resources when the class user did not explicitly call Dispose().
		/// </summary>
		~ ModelObject () { Dispose (false); }

		/// <summary>
		/// Actively reclaims unmanaged resources that this instance uses.
		/// </summary>
		public void Dispose()
		{
			Dispose (true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// For derived class implementers.
		/// <para>This method is called with argument true when class user calls Dispose(), while with argument false when
		/// the Garbage Collector invokes the finalizer, or Finalize() method.</para>
		/// <para>You must reclaim all used unmanaged resources in both cases, and can use this chance to call Dispose on disposable fields if the argument is true.</para>
		/// <para>Also, you must call the base virtual method within your overriding method.</para>
		/// </summary>
		/// <param name="disposing">true if the call comes from the Dispose() method; false if it comes from the Garbage Collector finalizer.</param>
		protected virtual void Dispose(bool disposing)
		{
			// Free unmanaged resources...

			// Free managed resources...but only if called from Dispose
			// (If called from Finalize then the objects might not exist anymore)
			if (disposing) {
				if (Mesh != null) {
					Mesh.Dispose ();
					Mesh = null;
				}

				if (Material != null) {
					Material.Dispose ();
					Material = null;
				}
			}	
		}
		#endregion

		#region methods
		/// <remarks>
		/// For Debugging purposes only.  If this method is being called, then some object
		/// has not been properly cast into a ModelMesh, a ModelInstanceDef, or a ModelInstanceRef.
		/// </remarks>
		public void ExplodeIntoArray(RMModel model, List<DisplayObject> array, Transform xForm)
		{
			Debug.WriteLine("Missing implementation or implementation should happen in derived class");
		}
		#endregion

	}
}