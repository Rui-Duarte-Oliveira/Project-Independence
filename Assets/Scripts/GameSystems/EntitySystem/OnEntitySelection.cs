using UnityEngine;

[RequireComponent(typeof(EntitySelection))]
public class OnEntitySelection : WorldObject
{
  //Graphical
  [SerializeField] private RectTransform visualBoxSelectionRectTransform;
  [SerializeField] private LayerMask clickableLayer;

  private Camera mainCamera;
  private EntitySelection entitySelection;
  private SelectableEntity hitEntity;
  private RaycastHit raycastHit;
  private Ray ray;
  private Rect logicalBoxSelection;
  private Vector2 startPosition;
  private Vector2 endPosition;

  protected override void Start()
  {
    mainCamera = Camera.main;
    entitySelection = GetComponent<EntitySelection>();
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    startPosition = Vector2.zero;
    endPosition = Vector2.zero;

    //Reset box visual
    DrawVisual();
  }

  protected override void StandardUpdate()
  {
    base.StandardUpdate();
    MouseInput(Input.mousePosition);
  }

  private void MouseInput(Vector2 inputPosition)
  {
    if (Input.GetMouseButtonDown(0))
    {
      startPosition = inputPosition;
      logicalBoxSelection = new Rect();
    }

    if (Input.GetMouseButton(0))
    {
      endPosition = inputPosition;
      DrawVisual();
      DrawLogical(inputPosition);
    }

    if (Input.GetMouseButtonUp(0))
    {
      if (Vector3.Distance(startPosition, endPosition) < Mathf.Max(Screen.width, Screen.height) * 0.01f)
      {
        OnClickSelection(Input.mousePosition);
        return;
      }

      OnDragSelection();
      startPosition = Vector2.zero;
      endPosition = Vector2.zero;
      DrawVisual();
    }
  }

  private void OnDragSelection()
  {
    if (!Processor.WorldObjectRegistry.ContainsKey(typeof(SelectableEntity)))
    {
      Debug.LogWarning("No Entities present in scene!");
      return;
    }

    SelectableEntity entity = null;
    bool isAdditive = Input.GetKey(KeyCode.LeftShift);

    if (!isAdditive)
    {
      entitySelection.DeselectAll();
    }

    for (int i = 0; i < Processor.WorldObjectRegistry[typeof(SelectableEntity)].Count; ++i)
    {
      entity = (SelectableEntity)Processor.WorldObjectRegistry[typeof(SelectableEntity)][i];
      
      if (logicalBoxSelection.Contains(mainCamera.WorldToScreenPoint(entity.transform.position)))
      {
        if (entity.GetType() == typeof(StationaryEntity))
        {
          continue;
        }

        if (!isAdditive)
        {
          entitySelection.DragSelect(entity);
          continue;
        }

        entitySelection.AdditiveDragSelect(entity);
      }
    }
  }

  private void OnClickSelection(Vector2 inputPosition)
  {
    ray = mainCamera.ScreenPointToRay(inputPosition);

    if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, clickableLayer))
    {
      if (!raycastHit.collider.gameObject.TryGetComponent(out hitEntity))
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
      if (!Input.GetKey(KeyCode.LeftShift))
      {
        entitySelection.DeselectAll();
      }
    }
  }

  private void DrawVisual()
  {
    //Center
    visualBoxSelectionRectTransform.position = (startPosition + endPosition) / 2;

    //Size
    visualBoxSelectionRectTransform.sizeDelta = new Vector2(Mathf.Abs(startPosition.x - endPosition.x), Mathf.Abs(startPosition.y - endPosition.y));
  }

  private void DrawLogical(Vector2 inputPosition)
  {
    logicalBoxSelection.xMin = Mathf.Min(inputPosition.x, startPosition.x);
    logicalBoxSelection.xMax = Mathf.Max(inputPosition.x, startPosition.x);
    logicalBoxSelection.yMin = Mathf.Min(inputPosition.y, startPosition.y);
    logicalBoxSelection.yMax = Mathf.Max(inputPosition.y, startPosition.y);
  }
}
