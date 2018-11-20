using System;
using UnityEngine;

namespace AccelerationTest._Scripts
{
	[RequireComponent(typeof(BoxCollider2D), typeof(RectTransform))]
	public class ImageBoxCollider : MonoBehaviour
	{
		private RectTransform _objectRect, _parentCanvas;
		private BoxCollider2D _boxCollider;
		private Rect _canvasOld;

		private void Awake()
		{
			_objectRect = GetComponent<RectTransform>();
			_boxCollider = GetComponent<BoxCollider2D>();
			_parentCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

			if (_parentCanvas != null)
				_canvasOld = _parentCanvas.rect;

			_boxCollider.size = _objectRect.rect.size;
			_boxCollider.offset = new Vector2(0,0);
		}
		
		void Update () 
		{
			if(Math.Abs(_canvasOld.width - _parentCanvas.rect.width) > 0.01f
			   || Math.Abs(_canvasOld.height - _parentCanvas.rect.height) > 0.01f)
			{
				_boxCollider.size = _objectRect.rect.size;
				_boxCollider.offset = new Vector2(0, 0);
				_canvasOld = _parentCanvas.rect;
			}
		}
	}
}
