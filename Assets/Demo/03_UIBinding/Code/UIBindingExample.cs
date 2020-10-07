using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;

namespace Databox.Examples
{
	public class UIBindingExample : MonoBehaviour
	{
		public DataboxObject database;
		
		public void LoadDB()
		{
			database.LoadDatabase();
		}
		
		public void SaveDB()
		{
			database.SaveDatabase();
		}
	}
}
