using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a more advanced example on how you can use Databox to save and load
/// objects created at runtime.
/// 
/// ++ Load Procedure ++
/// ++++++++++++++++++++
/// The load procedure works like this:
/// 1. first we load the database file
/// 
/// 2. This manager is subscribed to the database OnDatabaseLoaded event. So after
/// the file has been loaded we start a coroutine which takes care of the instantiation and the loading progress
/// 
/// 3. In the coroutine we iterate through all entries in the table "Assets" from the runtime database and
/// instantiate all objects based on their ids. Additionally we also store all new existing objects as well as
/// all old/current entry ids to a list.
/// 
/// 4. After finishing instantiation we call - on all newly created objects - our linking method (LinkToExistingDatabase) to make sure
/// they get registered to the runtime database. The linking method makes sure to link from and to the runtime database with it's new instance id
/// of the instantiated prefab.
/// 
/// 5. Then we remove all old entry ids from the database which we have stored to a list (step 3).
/// +++++++++++++++++++++++
/// </summary>
namespace Databox.Examples
{
	public class SaveManager : MonoBehaviour {
	
		public DataboxObjectManager manager;
		DataboxObject database;
		float loadProgress;
		
	
		
		public class ExistingObjects
		{
			public string tableID;
			public string entryID;
			
			public ExistingObjects (string _tableID, string _entryID)
			{
				tableID = _tableID;
				entryID = _entryID;
			}
		}
		
		List<ExistingObjects> existingObjectIDs;
		
		
		void Start()
		{
			database = manager.GetDataboxObject("Runtime");
		
			if (database != null)
			{
				database.OnDatabaseLoaded += OnDatabaseLoaded;
			}
		}
		
		void OnDisable()
		{
			if (database != null)
			{
				database.OnDatabaseLoaded -= OnDatabaseLoaded;
			}
		}
		
		void OnDatabaseLoaded()
		{
			StartCoroutine(RestoreObjectsFromSaveGame());
		}
	
		// Object loading routine
		IEnumerator RestoreObjectsFromSaveGame()
		{
			
			// store all existing ids from the database table so we can remove and clean all old entries from the database after instantiation
			existingObjectIDs = new List<ExistingObjects>();
			
			// store linking script temporarily so we can call LinkToExistingDatabase after instantiation
			var _objects = new List<AdvancedDataboxLinking>();
			
			// Get "Assets" table from database
			var _db = database.DB["Assets"];
			
			// keep track of the current instantiated object for our loading progress bar
			var _currentCount = 0;
			
			// re instantiate all objects in the assets table
			foreach(var entry in _db.entries.Keys)
			{
				existingObjectIDs.Add(new ExistingObjects("Assets", entry));
				
				var _inst = Instantiate(database.GetData<ResourceType>("Assets", entry, "Go").Load() as GameObject);
	
				_inst.GetComponent<PlaceAsset>().enabled = false;
						
				var _linkScript = _inst.GetComponent<AdvancedDataboxLinking>();
				_linkScript.objectId = entry;
						
				_objects.Add(_linkScript);
				
				// loading progress bar
				loadProgress = ((float)_currentCount / (float)_db.entries.Keys.Count);
				_currentCount ++;
				UIManager.Instance.ShowProgress(loadProgress);
				
				// wait for one frame to make sure scene does not freeze. This increases loading time of course
				yield return null;
			}
			
			UIManager.Instance.HideProgress();
			
			for(int i = 0; i < _objects.Count; i ++)
			{
				_objects[i].LinkToExistingDatabase(_objects[i].objectId);
			}
			
			// clear all previous entries in database
			for (int i = 0; i < existingObjectIDs.Count; i ++)
			{
				database.RemoveEntry(existingObjectIDs[i].tableID, existingObjectIDs[i].entryID);
			}
			
			yield return null;
		}
		
	}
}
