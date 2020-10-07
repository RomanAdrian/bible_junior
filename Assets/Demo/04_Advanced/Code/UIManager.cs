using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Databox.Utils;

/// <summary>
/// UIManager handles all ui relevant inputs and techtree
/// </summary>

namespace Databox.Examples
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance;
		
		public DataboxObjectManager manager;
		
		DataboxObject db;
		DataboxObject dbRuntime;
		
		public Button btnUI;
		public Button btnUISaveGame;
		
		public RectTransform buildMenu;
		public RectTransform saveMenu;
		
		public GameObject uiProgressWindow;
		public RectTransform uiProgress;
		
		string saveFolder = "DataboxExamples/04_Advanced/Save";
		
		string _fileName;
		public string FileName
		{
			get
			{
				return _fileName;		
			}
			set
			{
				_fileName = value;
			}
		}
		
		
		[Header("Techtree")]
		public GameObject techTreeButton;
		public GameObject techPrefab;
		public RectTransform techtreeContainer;
		public CanvasGroup techTreeWindow;
		bool researchActive = false;
		
		// Techtree object class we can use to store
		// all techtree relevant information
		[System.Serializable]
		public class Techtree
		{
			public string id;
			public RectTransform rect;
			public TechType tech;
			public TechTreeObject techObject;
			public int dependentCount = 0;
			public CanvasGroup techObjectCanvas;
			
			public Techtree (string _id, RectTransform _rect, TechType _tech, TechTreeObject _techObject, CanvasGroup _techObjectCanvas)
			{
				id = _id;
				rect = _rect;
				tech = _tech;
				techObject = _techObject;
				techObjectCanvas =  _techObjectCanvas;
			}
		}
		
		List<GameObject> saveGameButtons;
		List<GameObject> uiAssetButtons = new List<GameObject>();
		public List<Techtree> techTreeObjects = new List<Techtree>(); 
		Dictionary<string, int> techOrdering = new Dictionary<string, int>();
	
	
		// EVENTS
		///////////////////////
		public delegate void UIManagerEvents();
		public UIManagerEvents OnLoadGame;
		public UIManagerEvents OnNewGame;
		///////////////////////
		
		
		void OnDisable()
		{
			dbRuntime.OnDatabaseSaved -= RebuildSaveMenu;
		}
		
		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
		}
		
		void Start()
		{
			// get all database objects
			db = manager.GetDataboxObject("Initial");
			dbRuntime = manager.GetDataboxObject("Runtime");
			
			
			// subscribe to the database saved event.
			// As soon as user has saved a new file we need to rebuild the save game menu
			dbRuntime.OnDatabaseSaved += RebuildSaveMenu;
			
			// load the initial databse
			db.LoadDatabase();
			// clear the runtime database to make sure there's nothing left
			dbRuntime.ClearDatabase();
			// Reset back to initial values
			db.ResetToInitValues("Assets");
			
			// keep track of all generated save game ui buttons
			saveGameButtons = new List<GameObject>();
			
			// build the UI buttons for the build menu and the save game menu
			BuildAssetsMenu();	
			BuildSaveGameMenu();

		}
		
		
		// Called by the new game ui button in scene
		public void NewGame()
		{
			if (OnNewGame != null)
			{
				OnNewGame();
			}
			
			dbRuntime.ClearDatabase();
			
			// Reset back to initial values
			db.ResetToInitValues("Assets");
			
			BuildAssetsMenu();
			
			//LockTechtree();
		}
		
		// Called by the save game button in scene
		public void SaveGame()
		{
			// As we have set the save path on the runtime database to streaming asset path
			// we only need to add the additional save folder + filename
			var _d = Path.DirectorySeparatorChar;
			dbRuntime.SaveDatabase(saveFolder + _d + _fileName);
			
			// we also need to save the initial db to a different file
			// because our unlocked tech depends on it
			db.SaveDatabase(saveFolder + _d + (_fileName + "_Tech"));

		}
		
		public void DownloadCloud()
		{
			db.DownloadFromCloud();
		}
		
		// Called by the delete game button in scene
		public void DeleteSaveGame()
		{
			var _d = Path.DirectorySeparatorChar;
			var _path = Path.Combine(Application.streamingAssetsPath, saveFolder + _d + _fileName);
			var _techPath = Path.Combine(Application.streamingAssetsPath, saveFolder + _d + _fileName + "_Tech");
			// Delete runtime and tech file
			File.Delete(_path);
			File.Delete(_techPath);
			
			BuildSaveGameMenu();
			NewGame();
		}
		
		// Called by the SaveManager.cs on loading
		public void ShowProgress(float _progress)
		{
			uiProgressWindow.SetActive(true);
			uiProgress.sizeDelta = new Vector2(100*_progress, 30);
		}
		
		// Called by the SaveManager after loading is done
		public void HideProgress()
		{
			uiProgressWindow.SetActive(false);
		}
		
		// Create the build ui menu.
		// Iterate through all entries available in the Assets table of the initial database object and create a ui button
		void BuildAssetsMenu()
		{
			for (int i = 0; i < uiAssetButtons.Count; i ++)
			{
				Destroy(uiAssetButtons[i]);
			}
			
			uiAssetButtons = new List<GameObject>();
			
			foreach (var entry in db.DB["Assets"].entries.Keys)
			{
				var _tech = db.GetData<TechType>("Assets", entry, "Tech");
				if (_tech.IsResearched)
				{
					
					var _btn = Instantiate(btnUI) as Button;
					_btn.gameObject.transform.SetParent(buildMenu.transform, false);
					
					_btn.onClick.AddListener(() => {
		
						var _go = db.GetData<ResourceType>("Assets", entry, "Go").Load() as GameObject;
						Instantiate(_go, Vector3.zero, Quaternion.identity);
					
					});
					
					var _thumb = db.GetData<ResourceType>("Assets", entry, "Thumb").Load() as Texture2D;
					_btn.GetComponentInChildren<RawImage>().texture = _thumb;
					_btn.GetComponentInChildren<Text>().text = entry;
					
					uiAssetButtons.Add(_btn.gameObject);
				}
			}
		}
		
		// Create the save game menu
		// get all files available in the streaming assets path + saveFolder
		// and create the ui button with it's onClick event
		void BuildSaveGameMenu() 
		{
			// First we need to clear all existing save game buttons
			// before creating new ones
			for (int i = 0; i < saveGameButtons.Count; i ++)
			{
				Destroy(saveGameButtons[i]);
			}
			
			saveGameButtons = new List<GameObject>();
			
		
			if (!Directory.Exists(Path.Combine(Application.streamingAssetsPath, saveFolder)))
			{
				Directory.CreateDirectory(Path.Combine(Application.streamingAssetsPath, saveFolder));	
			}
			
			// get all files in our save folder
			var info = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, saveFolder));
			var fileInfo = info.GetFiles().Where(name => name.Extension != ".meta" && !name.ToString().Contains("_Tech")).Reverse();
			foreach (var file in fileInfo)
			{
				var _btn = Instantiate(btnUISaveGame) as Button;
				_btn.gameObject.transform.SetParent(saveMenu.transform, false);
				
				_btn.onClick.AddListener(() => {
					
					// Call OnLoadGame Event to make sure all objects in the scene
					// gets destroyed
					if (OnLoadGame != null)
					{
						OnLoadGame();
					}
					
					// Assign the current loaded savename
					FileName = file.Name.ToString();
					
					// Load the database
					var _d = System.IO.Path.DirectorySeparatorChar;
					dbRuntime.LoadDatabase(saveFolder + _d + file.Name.ToString());
					//// Load tech database
					db.LoadDatabase(saveFolder + _d + (file.Name.ToString() + "_Tech"));
							
					// Rebuild asset menu based on our unlocks
					BuildAssetsMenu();
					
				});
				
				// Assign file name to button text
				_btn.GetComponentInChildren<Text>().text = file.Name.ToString();
	
				saveGameButtons.Add(_btn.gameObject);
			}
			
			// resize the savemenu ui container for correct scrolling
			saveMenu.sizeDelta = new Vector2(0, saveGameButtons.Count * 30 + (saveGameButtons.Count * 3));
		}
		
		void RebuildSaveMenu()
		{
			BuildSaveGameMenu();
		}
		
		
		///////////////////////////////////
		// TECHTREE
		///////////////////////////////////
		public void UnlockTechtree()
		{
			techTreeButton.SetActive(true);	
		}
		
		public void LockTechtree()
		{
			techTreeButton.SetActive(false);
		}
		
		public void CloseTechtree()
		{
			techTreeWindow.alpha = 0f;
			techTreeWindow.blocksRaycasts = false;
			techTreeWindow.interactable = false;
		}
		
		public void BuildTechtree()
		{
			Debug.Log("build techtree");
			techTreeWindow.alpha = 1f;
			techTreeWindow.blocksRaycasts = true;
			techTreeWindow.interactable = true;
			
			for (int t = 0; t < techTreeObjects.Count; t ++)
			{
				Destroy(techTreeObjects[t].rect.gameObject);
			}
			
			techTreeObjects = new List<Techtree>();
			
			// first we need to instantiate all available entries
			foreach (var entry in db.DB["Assets"].entries.Keys)
			{
				var _tech = Instantiate (techPrefab) as GameObject;
				_tech.transform.SetParent(techtreeContainer.transform, false);
				
				var _techRect = _tech.GetComponent<RectTransform>();
				var _techValues = db.GetData<TechType>("Assets", entry, "Tech");
				
				var _techObject = _tech.GetComponent<TechTreeObject>();
				
				_techObject.title.text = entry;
				
				_techObject.btnResearch.onClick.AddListener(() => {
					
					Research(entry);
					
				});
				
				var _techCanvas = _tech.GetComponent<CanvasGroup>();
				
				techTreeObjects.Add(new Techtree(entry, _techRect, _techValues, _techObject, _techCanvas));
			}
			
			// now we need to sort them
			int _rootObjects = 0;
			for (int i = 0; i < techTreeObjects.Count; i ++)
			{
				if (techTreeObjects[i].tech.IsRootLevel)
				{
					techTreeObjects[i].rect.position = new Vector3(techTreeObjects[i].rect.position.x, techTreeObjects[i].rect.position.y - (_rootObjects * 80), techTreeObjects[i].rect.position.z);
					techTreeObjects[i].dependentCount = 0;
					_rootObjects ++;
				}
				else
				{
				
					// search for dependent tech
					for (int j = 0; j < techTreeObjects.Count; j ++)
					{
						if (techTreeObjects[j].id ==  techTreeObjects[i].tech.Dependency)
						{
							// connect line
							techTreeObjects[i].techObject.line.pos1 = techTreeObjects[j].rect;
							// set correct position based on position of dependent tech
							techTreeObjects[i].rect.position = new Vector3((techTreeObjects[j].rect.position.x + techTreeObjects[j].rect.sizeDelta.x + 20), techTreeObjects[j].rect.position.y - (techTreeObjects[j].dependentCount * 80), techTreeObjects[j].rect.position.z);
							techTreeObjects[j].dependentCount ++;						
						}
					}
				}
			}
			
			techOrdering = new Dictionary<string, int>();
			
			// we need to do a second sorting pass
			int _rootObjects1 = 0;
			for (int i = 0; i < techTreeObjects.Count; i ++)
			{
			
				if (techTreeObjects[i].tech.IsRootLevel)
				{
					if (i - 1 >= 0)
					{
						techTreeObjects[i].rect.position = new Vector3(techTreeObjects[i].rect.position.x, techTreeObjects[i].rect.position.y - ((_rootObjects1-1) * 80) - (techTreeObjects[i-1].dependentCount * 80), techTreeObjects[i].rect.position.z);
						_rootObjects1 ++;
					}
				}
			
				
				for (int j = 0; j < techTreeObjects.Count; j ++)
				{
					if (techTreeObjects[j].id ==  techTreeObjects[i].tech.Dependency && !techTreeObjects[i].tech.IsRootLevel)
					{
						// temporary store to amount of tech which has the same dependency
						var _orderCount = 1;
						if (techOrdering.TryGetValue(techTreeObjects[j].id, out _orderCount))
						{
							_orderCount ++;
							techOrdering[techTreeObjects[j].id] = _orderCount;
						}
						else
						{
							techOrdering.Add(techTreeObjects[j].id, 0);
						}
						
						techTreeObjects[i].rect.position = new Vector3((techTreeObjects[j].rect.position.x + techTreeObjects[j].rect.sizeDelta.x + 20), techTreeObjects[j].rect.position.y - (_orderCount * 80), techTreeObjects[i].rect.position.z);

					}
				
				}
			}
			
			CheckTechtree();
		}
		
		// Set unlock state depending on research state
		public void CheckTechtree()
		{
			// first set everything to false
			for (int i = 0; i < techTreeObjects.Count; i ++)
			{
				techTreeObjects[i].techObjectCanvas.alpha = 0.2f;
				techTreeObjects[i].techObjectCanvas.interactable = false;
				techTreeObjects[i].tech.Unlocked = false;
				techTreeObjects[i].techObject.btnResearch.gameObject.SetActive(true);
				techTreeObjects[i].techObject.progress.sizeDelta = new Vector2(0, 50);
			}
			
			for (int i = 0; i < techTreeObjects.Count; i ++)
			{
				// first level of our techtree is always unlocked and available for research
				if (techTreeObjects[i].tech.IsRootLevel)
				{
					techTreeObjects[i].techObjectCanvas.alpha = 1f;
					techTreeObjects[i].techObjectCanvas.interactable = true;
					techTreeObjects[i].tech.Unlocked = true;
					techTreeObjects[i].techObject.btnResearch.gameObject.SetActive(true);
					techTreeObjects[i].techObject.progress.sizeDelta = new Vector2(0, 50);
				}
				else
				{
					for (int j = 0; j < techTreeObjects.Count; j ++)
					{
						if (techTreeObjects[i].tech.Dependency == techTreeObjects[j].id && techTreeObjects[j].tech.IsResearched)
						{
							techTreeObjects[i].techObjectCanvas.alpha = 1f;
							techTreeObjects[i].techObjectCanvas.interactable = true;
							techTreeObjects[i].tech.Unlocked = true;
						}
					}
				}
				
				if (techTreeObjects[i].tech.IsResearched)
				{
					techTreeObjects[i].techObject.btnResearch.gameObject.SetActive(false);
					techTreeObjects[i].techObject.progress.sizeDelta = new Vector2(200, 50);
				}
			}
		}
		
		// Called by a research button in the techtree
		public void Research(string _id)
		{
			if (researchActive)
				return;
				
			StartCoroutine(ResearchIE(_id));
		}
		
		IEnumerator ResearchIE(string _id)
		{
			Debug.Log("Start research");
			researchActive = true;
			
			// search for the techtree object 
			var _index = 0;
			for (int j = 0; j < techTreeObjects.Count; j ++)
			{
				if (techTreeObjects[j].id == _id)
				{
					_index = j;
				}
			}
			
			var _tech = techTreeObjects[_index].tech;
			var _researchTime = _tech.ResearchTime;
		
			var _startTime = Time.time;
			var _currentResearchTime = 0f;
			
			// wait till research is done
			while (Time.time < _startTime + _researchTime)
			{
				_currentResearchTime = Time.time - _startTime;
				techTreeObjects[_index].techObject.progress.sizeDelta = new Vector2((_currentResearchTime * 200) / _researchTime, 50f);
				yield return null;
			}
			
			Debug.Log("research done");
			
			_tech.IsResearched = true;
			
			// after research we need to rebuild the build asset menu as well as
			// updating the techtree for new available tech
			CheckTechtree();
			BuildAssetsMenu();
			
			researchActive = false;
			
			yield return null;
		}
	}
}
