using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;
/*

	Simple example to show how you can iterate through entries in a table and through values in an entry.
	Add this script to an empty game object, assign a databox object and set the table name

*/
namespace Databox.Examples
{
	public class APIExample : MonoBehaviour
	{
	    
		public DataboxObject data;	
		public string tableName;
		
		bool dataReady = false;
		
		// Subscribe to the OnDatabaseLoaded event
		// when called we will set the dataReady bool to true, which enables the GUI button
		void OnEnable()
		{
			data.OnDatabaseLoaded += DatabaseLoaded;
		}
		
		// Unsubscribe
		void OnDisable()
		{
			data.OnDatabaseLoaded -= DatabaseLoaded;
		}
		
		// Database has been loaded and is ready
		void DatabaseLoaded()
		{
			dataReady = true;
		}
		
		// Load the database on start
		void Start()
		{
			data.LoadDatabase();
		}
		
		void OnGUI()
		{
			GUI.enabled = dataReady;
			if (GUI.Button(new Rect(10, 10, 200, 50), "Return all float values"))
			{
				ReturnAllFloatTypes();
			}
			
			if (GUI.Button(new Rect(10, 70, 200, 50), "Return all int values"))
			{
				ReturnAllIntTypes();
			}
			
			if (GUI.Button(new Rect(10, 130, 200, 50), "Return list values from entry"))
			{
				ReturnListEntries();
			}
			
			GUI.enabled = true;
		}
		
		void ReturnAllFloatTypes()
		{
			// First we get a dictionary with all entries from a table
			var _table = data.GetEntriesFromTable(tableName);
			
			//Debug.Log(_table.Count + " Entries in " + tableName);
			
			// Then we iterate through all entries
			foreach(var entry in _table.Keys)
			{
				// Next we get for each entry all values inside of it
				var _values = data.GetValuesFromEntry(tableName, entry);
				
				//Debug.Log(_values.Count + " Values in " + entry);
				
				// Then we iterate through all values
				foreach ( var value in _values.Keys)
				{
					// Finally we try to get all float values inside of each entry
					FloatType _float;
					if (data.TryGetData<FloatType>(tableName, entry, value, out _float))
					{
						// Return the float value 
						Debug.Log(_float.Value);
					}
					
				}
			}
			
		
		}
		
		void ReturnAllIntTypes()
		{
			// First we get a dictionary with all entries from a table
			var _table = data.GetEntriesFromTable(tableName);
			
			//Debug.Log(_table.Count + " Entries in " + tableName);
			
			// Then we iterate through all entries
			foreach(var entry in _table.Keys)
			{
				// Next we get for each entry all values inside of it
				var _values = data.GetValuesFromEntry(tableName, entry);
				
				//Debug.Log(_values.Count + " Values in " + entry);
				
				// Then we iterate through all values
				foreach ( var value in _values.Keys)
				{
					// Finally we try to get all int values inside of each entry
					IntType _int;
					if (data.TryGetData<IntType>(tableName, entry, value, out _int))
					{
						// Return the int value 
						Debug.Log(_int.Value);
					}
					
				}
			}
		}
		
		void ReturnListEntries()
		{
			var _table = data.GetEntriesFromTable(tableName);
			
			//Debug.Log(_table.Count + " Entries in " + tableName);
			
			// Then we iterate through all entries
			foreach(var entry in _table.Keys)
			{
				// Next we get for each entry all values inside of it
				var _values = data.GetValuesFromEntry(tableName, entry);
				
				//Debug.Log(_values.Count + " Values in " + entry);
				
				// Then we iterate through all values
				foreach ( var value in _values.Keys)
				{
					// Finally we try to get all list values with a string inside of each entry
					StringListType _list;
					if (data.TryGetData<StringListType>(tableName, entry, value, out _list))
					{
						// if we have found a list of type StringListType
						// we can now iterate through all entries inside of that list
						for (int i = 0; i < _list.Value.Count; i ++)
						{
							Debug.Log(_list.Value[i]);
						}
					}
					
				}
			}
			
		}
	}
}
