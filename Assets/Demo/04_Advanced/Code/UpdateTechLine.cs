using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Simple script which updates a UI image line
// between two rect transforms

namespace Databox.Examples
{
	public class UpdateTechLine : MonoBehaviour
	{
		public Image image;
		public float lineWidth;
	
		public RectTransform pos1;
		public RectTransform pos2;
		
		public Vector2 pivot;
		
	    // Update is called once per frame
	    void Update()
		{
			if (pos2 == null || pos1 == null)
				return;
				
			Vector3 differenceVector = (new Vector3(pos1.position.x + pos1.sizeDelta.x, pos1.position.y, pos1.position.z)) - pos2.position;
			
			image.rectTransform.sizeDelta = new Vector2( differenceVector.magnitude / image.canvas.scaleFactor, lineWidth);
		    image.rectTransform.pivot = pivot;
			image.rectTransform.position = pos2.position;
		    float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
			image.rectTransform.rotation = Quaternion.Euler(0,0, angle);
	    }
	}
}
