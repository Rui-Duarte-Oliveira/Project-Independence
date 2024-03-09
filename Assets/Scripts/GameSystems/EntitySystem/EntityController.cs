using UnityEngine;

[RequireComponent(typeof(EntitySelection))]
public class EntityController : WorldObject
{
  private EntitySelection entitySelection;

  protected override void Start()
  {
    base.StandardUpdate();
    entitySelection = GetComponent<EntitySelection>();
  }
}
