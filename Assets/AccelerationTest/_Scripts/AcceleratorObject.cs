using System;
using UnityEngine;

namespace AccelerationTest._Scripts
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class AcceleratorObject : MonoBehaviour
	{
		public float Speed = 10.0f;
		public float DownWardForce = 9.8f;
		private Rigidbody2D _rigidBody2D;

		public AccelerometerMode Mode;

		#region AccelerometerFields
		public float Smooth = 0.4f;
		public Vector2 NewPosition;
		public float NewRotation;
		public float Sensitivity = 6;
		private Vector3 _currentAcceleration, _initialAcceleration;
		#endregion
		
		private void Awake()
		{
			_rigidBody2D = GetComponent<Rigidbody2D>();
		}

		void Start ()
		{
			if (Mode == AccelerometerMode.Translate)
			{
				_rigidBody2D.AddForce(Vector2.down * DownWardForce, ForceMode2D.Impulse);
			}
			_initialAcceleration = Input.acceleration;
			_currentAcceleration = Vector3.zero;
		}
	
		void Update ()
		{
			if (Mode == AccelerometerMode.Translate)
			{
//				_currentAcceleration = Vector3.Lerp(_currentAcceleration, Input.acceleration - _currentAcceleration,
//					Time.deltaTime / Smooth);
//				NewPosition = new Vector2(Mathf.Clamp(_currentAcceleration.x * Sensitivity, -1, 1), Mathf.Clamp(_currentAcceleration.y * Sensitivity, -1, 1));
//				transform.Translate(NewPosition.x, NewPosition.y, 0);

				Vector3 acceleration = Input.acceleration;
				_rigidBody2D.AddForce(new Vector2(acceleration.x * Speed, acceleration.y * Speed));
			}
			else if (Mode == AccelerometerMode.Rotate)
			{
				_currentAcceleration = Vector3.Lerp(_currentAcceleration, Input.acceleration - _currentAcceleration,
					Time.deltaTime / Smooth);
				NewRotation = Mathf.Clamp(_currentAcceleration.x * Sensitivity, -1, 1);
				transform.Rotate(0, 0, -NewRotation);
				
//				transform.Translate(Input.acceleration.x, Input.acceleration.y, 0);
			}
		}
	}

	public enum AccelerometerMode
	{
		Translate,
		Rotate
	}
}
