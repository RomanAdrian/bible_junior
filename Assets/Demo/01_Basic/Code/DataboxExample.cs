using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Databox.Examples
{
	public class DataboxExample : MonoBehaviour
	{
		
		public DataboxObject database;
		public SimpleDataboxLinking cube;
		public SimpleDataboxLinking sphere;
		
		
		public void Start()
		{
			database.LoadDatabase();	
		}
		
		public void LoadDatabase()
		{
			database.LoadDatabase();
		}
		
		public void SaveDatabase()
		{
			database.SaveDatabase();
		}
		
		public void Reset()
		{
			database.ResetToInitValues("EXAMPLE");
			
			cube.OnLoaded();
			sphere.OnLoaded();
		}
	
	}
}