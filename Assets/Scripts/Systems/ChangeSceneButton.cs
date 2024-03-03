using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeSceneButton : MonoBehaviour, IPointerClickHandler
{
  [SerializeField] bool isUnloadingCurrent;
  [SerializeField] string sceneName;

  public void OnPointerClick(PointerEventData eventData)
  {
    SceneController.Instance.LoadScene(sceneName, isUnloadingCurrent);
  }
}
