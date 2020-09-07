using UnityEngine;
using System.Collections;
using UnityEngine.UI;

  public class SceneLoader : MonoBehaviour
  {
     public GameObject loadingPanel;
     public Slider loadingBar;
     public Text loadingText;
    
      public void back()
      {
          // reload the current scene
          SceneManager.LoadPreviousScene();
      }

      public void enter(string sceneName)
      {
          SceneManager.LoadScene( sceneName );
      }

      public void LoadLevel(string levelName)
      {
        StartCoroutine(LoadSceneAsync(levelName));
      }

    IEnumerator LoadSceneAsync ( string levelName )
    {
        loadingPanel.SetActive(true);
        
        AsyncOperation op = SceneManager.LoadSceneAsync( levelName );


        while (!op.isDone )
        {
            float progress = Mathf.Clamp01(op.progress / .9f);

            loadingBar.value = progress;
            loadingText.text = progress * 100f + "%";

            yield return null;
        }
    }
  }
