using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : WorldObject
{
  [SerializeField]
  private List<SelectableEntity> selectableEntities;

  private EntitySelection entitySelection;

  public List<SelectableEntity> SelectableEntities { get => selectableEntities; set => selectableEntities = value; }

  protected override void Start()
  {
    base.Start();
    entitySelection = (EntitySelection)Processor.WorldObjectRegistry[typeof(EntitySelection)][0];
  }

  public void AddEntity(Entity entityToAdd)
  {
    entityToAdd.Owner = this;

    if (entityToAdd is SelectableEntity)
    {
      selectableEntities.Add((SelectableEntity)entityToAdd);
    }
  }

  public void RemoveEntity(Entity entityToRemove)
  {
    entityToRemove.Owner = null;

    if(entityToRemove is SelectableEntity)
    {
      SelectableEntity selectableEntity = (SelectableEntity)entityToRemove;
      selectableEntities.Remove(selectableEntity);

      if (selectableEntity.IsSelected)
      {
        entitySelection.Deselect(selectableEntity);
      }
    }
  }
}
