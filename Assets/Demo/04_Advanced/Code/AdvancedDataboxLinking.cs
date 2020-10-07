using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Databox.Examples
{
	/// <summary>
	/// A more advanced example on how to register objects to a different runtime database.
	/// This script also listens to OnLoadGame and OnNewGame event to make sure
	/// the object destroys itself.
	/// </summary>
	public class AdvancedDataboxLinking : MonoBehaviour
	{
		public DataboxObjectManager manager;
		
		public string objectId;
		public string fromDBId;
		public string toDBId;
		
		[SerializeField]
		public List<UnityEvent> initializationMethods = new List<UnityEvent>();
		
		public AssignObjectValues assignObjectValues;
		
		DataboxObject database;
		
		void OnEnable()
		{
			UIManager.Instance.OnLoadGame += OnLoadGame;
			UIManager.Instance.OnNewGame += OnNewGame;
		}
		
		void OnDisable()
		{
			if (UIManager.Instance != null)
			{
				UIManager.Instance.OnLoadGame -= OnLoadGame;
				UIManager.Instance.OnNewGame -= OnNewGame;
			}
		}
		
		// Links this object from the initial database to the runtime database.
		// Called by the PlaceAsset.cs script after user has clicked
		public void LinkToNewDatabase()
		{
			var _fromDB = manager.GetDataboxObject(fromDBId);
			var _toDB = manager.GetDataboxObject(toDBId);
		
			database = _toDB;
			
			_fromDB.RegisterToDatabase(_toDB, "Assets", objectId, this.gameObject.GetInstanceID().ToString());
			
			objectId = this.gameObject.GetInstanceID().ToString();
			
			// Set/Get position and start change color routine
			assignObjectValues.SetPositionToDB(objectId, database);
			//assignObjectValues.ChangeColor(objectId, database);
			
			CallInitializationMethods();
		}
		
		// Links/Overrides an exisiting entry from the runtime database with its new id
		// Called by the SaveManager.cs script after reinstantiating the object
		public void LinkToExistingDatabase(string _existingID)
		{
			objectId = this.gameObject.GetInstanceID().ToString();
			
			var _toDB = manager.GetDataboxObject(toDBId);
			
			database = _toDB;
			
			_toDB.RegisterToDatabase(_toDB, "Assets", _existingID, objectId);
			
			
			// Set/Get position and start change color routine
			assignObjectValues.AssignPositionFromDB(objectId, _toDB);
			//assignObjectValues.ChangeColor(objectId, _toDB);
			
			CallInitializationMethods();
		}
		
		void OnLoadGame()
		{
			// destroy objects as we re-instantiate each objects which exists
			// in the runtime database
			Destroy(this.gameObject);
		}
		
		void OnNewGame()
		{
			Destroy(this.gameObject);
		}
		
		void CallInitializationMethods()
		{
			for (int i = 0; i < initializationMethods.Count; i ++ )
			{
				initializationMethods[i].Invoke();
			}
		}
	}
}