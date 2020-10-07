using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Databox.Examples
{
	/// <summary>
	/// Assign position and color from database. This is called by the AdvancedDataboxLinking.cs script
	/// after object has been registetered or loaded from the database.
	/// </summary>
	public class AssignObjectValues : MonoBehaviour
	{
		
		public MeshRenderer meshRenderer;	
		DataboxObject database;
		string objectId;

		// POSITION Value
		public void SetPositionToDB(string _objectId, DataboxObject _database)
		{
			objectId = _objectId;
			database = _database;
			
			var _position = database.GetData<Vector3Type>("Assets", objectId, "Position");
			_position.Value = this.transform.position;			
		}
	
		public void AssignPositionFromDB(string _objectId, DataboxObject _database)
		{
			objectId = _objectId;
			database = _database;
				
			var _position = database.GetData<Vector3Type>("Assets", objectId, "Position");
			this.transform.position = _position.Value;
		}
		
		// COLOR Value
		public void ChangeColor(string _objectId, DataboxObject _database)
		{
			objectId = _objectId;
			database = _database;
				
			// assign color
			var _color = database.GetData<ColorType>("Assets", objectId, "Color");
			meshRenderer.material.color = _color.Value;
				
			StartCoroutine(ChangeColor());
		}
			
		IEnumerator ChangeColor()
		{
			// get reference
			var _color = database.GetData<ColorType>("Assets", objectId, "Color");
			var _coroutineOffset = database.GetData<FloatType>("Assets", objectId, "CoroutineOffset");
			var _offset = _coroutineOffset.Value;
				
			while (true)
			{	
				var _startTime = Time.time;
				while (Time.time < _startTime + 1f + _offset)
				{
					_coroutineOffset.Value = (_startTime + 1) - Time.time;
					yield return null;
				}
					
				// change color
				_color.Value = new Color (Random.Range(0f, 255f) / 255f, Random.Range(0f, 255f) / 255f, Random.Range(0f, 255f) / 255f);
				meshRenderer.material.color = _color.Value;
					
				yield return null;
			}
		}
	}
}
