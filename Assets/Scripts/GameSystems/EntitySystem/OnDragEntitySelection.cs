using UnityEngine;

public class OnDragEntitySelection : OnEntitySelection
{
  //Graphical
  [SerializeField] private RectTransform visualBoxSelectionRectTransform;

  //Logical
  private Rect logicalBoxSelection;
  private Vector2 startPosition;
  private Vector2 endPosition;

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
    DragSelection(Input.mousePosition);
  }

  private void DragSelection(Vector2 inputPosition)
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
      DrawSelection(inputPosition);
    }

    if (Input.GetMouseButtonUp(0))
    {
      SelectEntities();
      startPosition = Vector2.zero;
      endPosition = Vector2.zero;
      DrawVisual();
    }
  }

  private void DrawVisual()
  {
    Vector2 boxStart = startPosition;
    Vector2 boxEnd = endPosition;

    Vector2 boxCenter = (boxStart + boxEnd) / 2;
    visualBoxSelectionRectTransform.position = boxCenter;

    Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
    visualBoxSelectionRectTransform.sizeDelta = boxSize;
  }

  private void DrawSelection(Vector2 inputPosition)
  {
    if (inputPosition.x < startPosition.x)
    {
      //dragging left
      logicalBoxSelection.xMin = inputPosition.x;
      logicalBoxSelection.xMax = startPosition.x;
    }
    else
    {
      //dragging right
      logicalBoxSelection.xMin = startPosition.x;
      logicalBoxSelection.xMax = inputPosition.x;
    }

    if (inputPosition.y < startPosition.y)
    {
      //dragging down
      logicalBoxSelection.yMin = inputPosition.y;
      logicalBoxSelection.yMax = startPosition.y;
    }
    else
    {
      //dragging up
      logicalBoxSelection.yMin = startPosition.y;
      logicalBoxSelection.yMax = inputPosition.y;
    }
  }

  private void SelectEntities()
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
        if (!isAdditive)
        {
          entitySelection.DragSelect(entity);
          continue;
        }

        entitySelection.AdditiveDragSelect(entity);
      }
    }
  }
}
