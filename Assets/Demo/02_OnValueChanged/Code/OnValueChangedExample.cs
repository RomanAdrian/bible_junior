using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;

// Every databox type has an on value change callback.
// This example shows you how to register a callback method and check for it's value.
namespace Databox.Examples
{
	public class OnValueChangedExample : MonoBehaviour
	{
		// the actual database
		public DataboxObject database;
		// the health variable
		FloatType health;
		
		
		void OnEnable()
		{
			database.OnDatabaseLoaded += BindValue;
		}

		public void OnDisable()
		{
			health.OnValueChanged -= OnValueChanged;
			database.OnDatabaseLoaded -= BindValue;
		}
		
		public void Start()
		{
			database.LoadDatabase();
		}
		
		void BindValue()
		{
			// get the health variable reference of type FloatType
			health = database.GetData<FloatType>("Example", "Sphere", "Health");	
			// register callback
			health.OnValueChanged += OnValueChanged;
		}
		
		// Callback method
		void OnValueChanged(DataboxType _type)
		{
			Debug.Log("Health value changed: " + health.Value);
			
			// check if health is 0 or less
			if (health.Value <= 0)
			{
				Debug.Log("DEAD");
				
				// reset value back to initial value
				Debug.Log("Reset back to initial value");
				health.Reset();
			}
		}
		
	}
}