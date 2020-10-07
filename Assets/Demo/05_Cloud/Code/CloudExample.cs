using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Databox;

namespace Databox.Examples
{
	public class CloudExample : MonoBehaviour
	{
		
		public DataboxObject db;
		
		void OnEnable()
		{
			db.OnDatabaseCloudDownloaded += OnCloudDownloadReady;
			db.OnDatabaseCloudDownloadFailed += OnCloudDownloadFailed;
			
			db.OnDatabaseCloudUploaded += OnCloudUploadReady;
			db.OnDatabaseCloudUploadFailed += OnCloudUploadFailed;
		}
		
		void OnDisable()
		{
			db.OnDatabaseCloudDownloaded -= OnCloudDownloadReady;
			db.OnDatabaseCloudDownloadFailed -= OnCloudDownloadFailed;
		}
		
	
		// Override cloud version with local version
		public void UploadToCloud()
		{
			db.UploadToCloud();
		}
		
		public void DownloadCloud()
		{
			db.DownloadFromCloud();
		}
			
		void OnCloudDownloadReady()
		{
			Debug.Log("cloud download ready");
		}
		
		void OnCloudDownloadFailed()
		{
			Debug.LogError("cloud download failed. Please check server url.");
		}
		
		void OnCloudUploadReady()
		{
			Debug.Log("cloud upload ready");
		}
		
		void OnCloudUploadFailed()
		{
			Debug.LogError("cloud upload failed. Please check server url");
		}
	}
}