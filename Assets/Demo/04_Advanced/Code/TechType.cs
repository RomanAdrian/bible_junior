using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Linq;
 using System.Reflection;
 
using Databox;
using Databox.Ed;


[System.Serializable]
[DataboxTypeAttribute(Name = "Tech")]
public class TechType : DataboxType {
	
	// VALUES
	/////////////////////////////////////
	[SerializeField]
	public float ResearchTime;
	
	[SerializeField]
	public string DataTable;
	
	[SerializeField]
	public string Dependency;
	
	[SerializeField]
	public bool IsRootLevel;
	
	[SerializeField]
	private bool _unlocked;
	
	[SerializeField]
	public bool InitUnlocked;
	public bool Unlocked
	{
		get {return _unlocked;}
		set
		{
			if (value == _unlocked){return;	}
			
			_unlocked = value;
			if (OnValueChanged != null){OnValueChanged(this);}
		}
	}
	
	[SerializeField]
	private bool _isResearched;
	
	[SerializeField]
	public bool InitIsResearched;
	public bool IsResearched
	{
		get {return _isResearched;}
		set
		{
			if (value == _isResearched){return;}
			
			_isResearched = value;
			
			if (OnValueChanged != null){OnValueChanged(this);}
		}
	}
	/////////////////////////////////////

	
	public override void DrawEditor(DataboxObject _databox)
	{	
		using (new GUILayout.VerticalScope())
		{
			var _rt = ResearchTime.ToString();
			GUILayout.Label("Researchtime:");
			_rt = GUILayout.TextField(_rt);
			
			float.TryParse(_rt, out ResearchTime);
			
			IsRootLevel = GUILayout.Toggle(IsRootLevel, "Is root level");
			
			
			// If this tech is the root level of the techtree it does not depend from anything
			if (!IsRootLevel)
			{
				
				using (new GUILayout.VerticalScope("Box"))
				{
					GUILayout.Label("Tech dependency");
					
					#if UNITY_EDITOR
					EditorGUILayout.HelpBox("Select dependent tech (Unlock this tech if dependent tech is researched)", MessageType.Info);
					#endif
					
					using (new GUILayout.HorizontalScope())
					{
	
						GUILayout.Label("Table:");
						#if UNITY_EDITOR
						if (Application.isEditor && !Application.isPlaying)
						{
							var _tableRect = GUILayoutUtility.GetLastRect();
						
							var _tables = _databox.DB.Keys.ToArray();
							if (EditorGUILayout.DropdownButton(new GUIContent(DataTable), FocusType.Keyboard, GUILayout.MinWidth(200)))
							{
								FieldInfo field = this.GetType().GetField("DataTable");
								if (field != null)
								{
									PopupWindow.Show(_tableRect, new PopUps.PopupShowStringList(_tables.ToList(), _tableRect, this, field));
								}
							}
						}	
						else
						{
							DataTable = GUILayout.TextField(DataTable);
						}
						#else
						DataTable = GUILayout.TextField(DataTable);
						#endif 
						
						if (!string.IsNullOrEmpty(DataTable))
						{
							#if UNITY_EDITOR
							if (Application.isEditor && !Application.isPlaying)
							{
								GUILayout.Label("Entry:");
								var _entryRect = GUILayoutUtility.GetLastRect();
								var _entries = _databox.DB[DataTable].entries.Keys.ToArray();
								
								if (EditorGUILayout.DropdownButton(new GUIContent(Dependency), FocusType.Keyboard, GUILayout.MinWidth(200)))
								{
									FieldInfo _dependencyField = this.GetType().GetField("Dependency");
									if (_dependencyField != null)
									{
										PopupWindow.Show(_entryRect, new PopUps.PopupShowStringList(_entries.ToList(), _entryRect, this, _dependencyField));
									}
								}
							}
							else
							{
								Dependency = GUILayout.TextField(Dependency);
							}
							#else
							Dependency = GUILayout.TextField(Dependency);
							#endif
						}
					}
				}
			}
			else
			{
				// root level needs to be unlocked anytime
				Unlocked = true;
			}
			
			
			Unlocked = GUILayout.Toggle(Unlocked, "Is Unlocked");
			IsResearched = GUILayout.Toggle(IsResearched, "Is Researched");
			
		}
	
	}
	
	public override void DrawInitValueEditor()
	{
		GUI.color = Color.yellow;
		GUILayout.Label("Init Value:");
		GUI.color = Color.white;
		
		InitUnlocked = GUILayout.Toggle(InitUnlocked, "Is Unlocked");
		InitIsResearched = GUILayout.Toggle(InitIsResearched, "Is Researched");
	}
	
	// Reset value back to initial value
	public override void Reset()
	{
		Unlocked = InitUnlocked;
		IsResearched = InitIsResearched;
	}
	
	// Important for the cloud sync comparison
	public override string Equal(DataboxType _changedValue)
	{
		var _v = _changedValue as TechType;
		if (Unlocked != _v.Unlocked || IsResearched != _v.IsResearched)
		{
			// return original value and changed value
			return Unlocked.ToString() + " : " + _v.Unlocked.ToString();
		}
		else
		{
			return "";
		}
	}
}
