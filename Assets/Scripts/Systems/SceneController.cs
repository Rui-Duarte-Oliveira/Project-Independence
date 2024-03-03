using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
  [SerializeField] private GameObject loadingCanvas;
  [SerializeField] private Image progressBar;
  [SerializeField] private bool isActivatingLoadingScreen;
  [SerializeField] private string startingScene;

  private static SceneController instance;

  private AsyncOperation sceneToLoad;
  private AsyncOperation sceneToUnload;

  public static SceneController Instance { get => instance; }

  private void Awake()
  {
    if(Instance != null)
    {
      Destroy(this);
      Debug.LogError("MULTIPLE SCENE CONTROLLERS!");
      return;
    }

    instance = this;

    LoadScene(startingScene, false, isActivatingLoadingScreen);
  }

  public async void LoadScene(string targetSceneName, bool isUnloadingCurrent, bool isActivatingLoadingScreen)
  {
    sceneToLoad = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);

    if (isUnloadingCurrent)
    {
      //GetScene is at 1 since we will always have a default persistent scene.
      sceneToUnload = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).name);
    }

    if (!isActivatingLoadingScreen)
    {
      sceneToLoad = null;
      return;
    }

    sceneToLoad.allowSceneActivation = false;

    loadingCanvas.SetActive(true);

    if(sceneToUnload != null)
    {
      while (sceneToLoad.progress < 0.99f && sceneToUnload.progress < 0.99f)
      {
        float progress = (sceneToLoad.progress / 2) + (sceneToUnload.progress / 2);
        progressBar.fillAmount = progress;
        await Task.Yield();
      }
    }
    else
    {
      while (sceneToLoad.progress < 0.99f)
      {
        progressBar.fillAmount = sceneToLoad.progress;
        await Task.Yield();
      }
    }

    sceneToLoad.allowSceneActivation = true;
    loadingCanvas.SetActive(false);
    sceneToUnload = null;
    sceneToLoad = null;
  }
}
