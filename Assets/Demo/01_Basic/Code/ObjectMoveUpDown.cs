using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Databox.Examples
{
	public class ObjectMoveUpDown : MonoBehaviour
	{
	   
		public MeshRenderer meshRenderer;
		public DataboxType type;
		public FloatType speed;
		public ColorType color;
		public Vector3Type position;
		public FloatType direction;
		
		float maxHeight = 4f;
		float nextColorChange = 0f;
		
		void Update()
		{
			transform.Translate (0, direction.Value*speed.Value*Time.deltaTime * 1, 0);
			
			if (transform.position.y > maxHeight)
			{
				direction.Value = -1f;
			}
			
			if (transform.position.y <= 0)
			{
				direction.Value = 1f;
			}
			
			if (Time.time > nextColorChange + 1f)
			{
				nextColorChange = Time.time;
				color.Value = new Color(Random.Range(0f, 255f) / 255f, Random.Range(0f, 255f) / 255f, Random.Range(0f, 255f) / 255f);
				meshRenderer.material.color = color.Value;
				
			}
			
			// update position value
			position.Value = this.transform.position;
		}
	}
}