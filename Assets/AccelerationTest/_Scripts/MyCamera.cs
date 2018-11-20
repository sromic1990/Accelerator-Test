using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace AccelerationTest._Scripts
{
	[RequireComponent(typeof(Camera))]
	public class MyCamera : MonoBehaviour
	{
		[System.Serializable]
		public struct CameraExtents
		{
			public float HorizontalExtents;
			public float VerticalExtents;
		}

		public struct CameraBoundary
		{
			public float LeftBoundary;
			public float RightBoundary;
			public float TopBoundary;
			public float BottomBoundary;
		}
		
		private Camera _camera;
		
		[SerializeField]private CameraExtents _extents;
		public CameraExtents Extents
		{
			get { return _extents; }
		}

		private CameraBoundary _boundary;
		public CameraBoundary Boundary
		{
			get { return _boundary; }
		}

		private void Awake()
		{
			_camera = GetComponent<Camera>();
			if (!_camera.orthographic)
			{
				_camera.orthographic = true;
			}
			StoreCameraExtents();
			StoreCameraBoundaries();
		}

		private void StoreCameraExtents()
		{
			_extents.VerticalExtents = _camera.orthographicSize * GetAspectRatio() * 2;
			_extents.HorizontalExtents = _extents.VerticalExtents * GetAspectRatio();
		}

		private void StoreCameraBoundaries()
		{
			_boundary.LeftBoundary = transform.position.x - _extents.HorizontalExtents;
			_boundary.RightBoundary = transform.position.x + _extents.HorizontalExtents;
			_boundary.TopBoundary = transform.position.y + _extents.VerticalExtents;
			_boundary.BottomBoundary = transform.position.y - _extents.VerticalExtents;
		}

		private static float GetAspectRatio()
		{
			return ((float)Screen.width / (float)Screen.height);
		}
	}
}
