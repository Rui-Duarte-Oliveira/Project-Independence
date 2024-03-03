using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeSceneButton : MonoBehaviour, IPointerClickHandler
{
  [SerializeField] bool isUnloadingCurrent;
  [SerializeField] bool isActivatingLoadingScreen;
  [SerializeField] string sceneName;

  public void OnPointerClick(PointerEventData eventData)
  {
    SceneController.Instance.LoadScene(sceneName, isUnloadingCurrent, isActivatingLoadingScreen);
  }
}
