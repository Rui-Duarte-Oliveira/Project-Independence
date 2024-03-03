using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
  [SerializeField] private GameObject loadingCanvas;
  [SerializeField] private Image progressBar;
  [SerializeField] private string StartingScene;

  private static SceneController instance;

  private Scene previousScene;
  private AsyncOperation sceneToLoad;

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

    LoadScene(StartingScene, false);
  }

  public async void LoadScene(string targetSceneName, bool isUnloadingCurrent)
  {
    //GetScene is at 1 since we will always have a default persistent scene.
    previousScene = SceneManager.GetSceneAt(1);

    if (isUnloadingCurrent)
    {
      UnLoadScene(previousScene.name);
    }

    sceneToLoad = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
    sceneToLoad.allowSceneActivation = false;

    loadingCanvas.SetActive(true);
    while (sceneToLoad.progress < 0.9f)
    {
      await Task.Delay(100);
      progressBar.fillAmount = sceneToLoad.progress;
    }

    sceneToLoad.allowSceneActivation = true;
    loadingCanvas.SetActive(false);
  }

  private void UnLoadScene(string targetSceneName)
  {
    SceneManager.UnloadSceneAsync(targetSceneName);
  }
}
