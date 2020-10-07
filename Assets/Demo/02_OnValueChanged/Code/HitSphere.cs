using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;

namespace Databox.Examples
{
	public class HitSphere : MonoBehaviour
	{
		
		public DataboxObject database;
	
		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				if(Physics.Raycast(ray, out hit))
				{
					// Please take in mind, this is only for example purpose. During production you should cache the health.
					var _health = database.GetData<FloatType>("Example", "Sphere", "Health");
					
					_health.Value --;
				}
			}
		}
		
	}
}