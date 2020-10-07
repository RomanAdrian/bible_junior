using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Databox.Examples
{
	public class SimpleDataboxLinking : MonoBehaviour
	{
		public string tableID;
		public string objectID;
		public ObjectMoveUpDown objectScript;
		public Renderer objectRenderer;
		
		public DataboxObject database;
		
		void OnEnable()
		{
			database.OnDatabaseLoaded += OnLoaded;
		}
		
		void OnDisable()
		{
			database.OnDatabaseLoaded -= OnLoaded;
		}
	
		
		// Assign Data on database loaded
		public void OnLoaded()
		{
			// Get all data we need from the runtime database and assign it to the object
			var _v3 = database.GetData<Vector3Type>(tableID, objectID, "Position");
			var _color = database.GetData<ColorType>(tableID, objectID, "Color");
			var _speed = database.GetData<FloatType>(tableID, objectID, "Speed");
			var _direction = database.GetData<FloatType>(tableID, objectID, "Direction");
	
			this.transform.position = _v3.Value;
			objectRenderer.material.color = _color.Value;
			
			
			objectScript.speed = _speed;
			objectScript.direction = _direction;
			objectScript.position = _v3;
			objectScript.color = _color;
		}
	}
}
