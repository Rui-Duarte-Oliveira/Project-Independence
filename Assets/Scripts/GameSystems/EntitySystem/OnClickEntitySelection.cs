using UnityEngine;

public class OnClickEntitySelection : OnEntitySelection
{
  [SerializeField] private LayerMask clickableLayer;
  [SerializeField] private LayerMask groundLayer;

  private SelectableEntity hitEntity;
  private RaycastHit raycastHit;
  private Ray ray;

  protected override void StandardUpdate()
  {
    base.StandardUpdate();
    ClickSelection(Input.mousePosition);
  }

  private void ClickSelection(Vector2 inputPosition)
  {
    if(Input.GetMouseButtonDown(0))
    {
      ray = mainCamera.ScreenPointToRay(inputPosition);

      if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, clickableLayer))
      {
        if(!raycastHit.collider.gameObject.TryGetComponent(out hitEntity))
        {
          Debug.LogError($"Object {raycastHit.collider.transform.name} is not a selectable entity!");
          return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
          entitySelection.AdditiveClickSelect(hitEntity);
        }
        else
        {
          entitySelection.ClickSelect(hitEntity);
        }
      }
      else 
      {
        if(!Input.GetKey(KeyCode.LeftShift)) 
        {
          entitySelection.DeselectAll();
        }
      }
    }
  }
}
