using UnityEngine;

public class EntitySelection : WorldObject
{
  private SelectableEntities<SelectableEntity> selectedEntities;

  public SelectableEntities<SelectableEntity> SelectedEntities { get => selectedEntities; }

  protected override void Awake()
  {
    base.Awake();
    selectedEntities = new SelectableEntities<SelectableEntity>();
  }

  public void ClickSelect(SelectableEntity entityToAdd)
  {
    if (entityToAdd.GetType() == typeof(StationaryEntity))
    {
      DeselectAll();
      selectedEntities.Add(entityToAdd);
      return;
    }

    if (selectedEntities.Contains(entityToAdd))
    {
      if (selectedEntities.Count != 1)
      {
        DeselectAll(entityToAdd);
      }

      return;
    }

    DeselectAll();
    selectedEntities.Add(entityToAdd);
  }

  public void AdditiveClickSelect(SelectableEntity entityToAdd)
  {
    if (entityToAdd.GetType() == typeof(StationaryEntity))
    {
      ClickSelect(entityToAdd);
      return;
    }

    if (!selectedEntities.Contains(entityToAdd))
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

  public void Deselect(SelectableEntity entityToRemove)
  {
    if (selectedEntities.Contains(entityToRemove))
    {
      selectedEntities.Remove(entityToRemove);
    }
  }

  public void DeselectAll()
  {
    selectedEntities.Clear();
  }

  /// <summary>
  /// Deselect all entities except parameter input.
  /// </summary>
  /// <param name="entityToAdd"></param>
  public void DeselectAll(SelectableEntity entityToAdd)
  {
    selectedEntities.Clear(entityToAdd);
  }

  //public void SelectAllOfType<T>() where T : SelectableEntity
  //{
  //  if (!Processor.WorldObjectRegistry.ContainsKey(typeof(SelectableEntity)))
  //  {
  //    Debug.LogWarning("No Entities present in scene!");
  //    return;
  //  }

  //  SelectableEntity entity = null;

  //  for (int i = 0; i < Processor.WorldObjectRegistry[typeof(SelectableEntity)].Count; ++i)
  //  {
  //    entity = (SelectableEntity)Processor.WorldObjectRegistry[typeof(SelectableEntity)][i];
  //    selectedEntities.Add(entity);
  //  }
  //}
}
