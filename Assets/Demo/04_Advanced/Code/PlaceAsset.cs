using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Databox.Examples
{
	public class PlaceAsset : MonoBehaviour
	{
		public DataboxObjectManager manager;
		public LayerMask terrainLayerMask;
		public AdvancedDataboxLinking databoxLinking;
		
		float waitForSnap = 0f;
		
		void Awake()
		{
			waitForSnap = Time.time;
		}
		
		public void LateUpdate()
		{
			if (Time.time < waitForSnap + 0.1f)
				return;
				
			// Grid Snapping
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit _hit;
			Vector3 _hitPos = transform.position;
			
			if (Physics.Raycast(ray, out _hit, terrainLayerMask))
			{
				_hitPos = _hit.point;
			}
	
			transform.position = new Vector3(
				Mathf.Round( _hitPos.x / 1 ),
				0f, //Mathf.Round( _hitPos.y / 1 ),
				Mathf.Round( _hitPos.z / 1 ));
		
		
			if (Input.GetMouseButtonUp(0))
			{
				var _go = manager.GetDataboxObject("Initial").GetData<ResourceType>("Assets", databoxLinking.objectId, "Go").Load() as GameObject;
				Instantiate(_go, this.transform.position,  Quaternion.identity);
			
				// Place asset and register to database
				databoxLinking.LinkToNewDatabase();
				this.enabled = false;
			}
			
			if (Input.GetMouseButtonUp(1))
			{
				Destroy(this.gameObject);
			}
		}
	}
}
