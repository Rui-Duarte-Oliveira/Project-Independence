
public class EntitySelection : WorldObject
{
  private SelectableEntities<SelectableEntity> selectedEntities;

  protected override void Awake()
  {
    base.Awake();
    selectedEntities = new SelectableEntities<SelectableEntity>();
  }

  public void ClickSelect(SelectableEntity entityToAdd)
  {
    DeselectAll();
    selectedEntities.Add(entityToAdd);
  }

  public void AdditiveClickSelect(SelectableEntity entityToAdd)
  {
    if(!selectedEntities.Contains(entityToAdd))
    {
      selectedEntities.Add(entityToAdd);
      return;
    }

    selectedEntities.Remove(entityToAdd);
  }

  /// <summary>
  /// Must DeselectAll first!
  /// </summary>
  /// <param name="entityToAdd"></param>
  public void DragSelect(SelectableEntity entityToAdd)
  {
    selectedEntities.Add(entityToAdd);
  }

  public void AdditiveDragSelect(SelectableEntity entityToAdd)
  {
    if (!selectedEntities.Contains(entityToAdd))
    {
      selectedEntities.Add(entityToAdd);
      return;
    }

    selectedEntities.Remove(entityToAdd);
  }

  public void DeselectEntity(SelectableEntity entityToDeselect)
  {

  }

  public void DeselectAll()
  {
    selectedEntities.Clear();
  }
}
