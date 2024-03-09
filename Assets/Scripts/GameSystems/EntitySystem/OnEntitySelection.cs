using UnityEngine;

[RequireComponent(typeof(EntitySelection))]
public class OnEntitySelection : WorldObject
{
  protected Camera mainCamera;
  protected EntitySelection entitySelection;

  protected override void Start()
  {
    mainCamera = Camera.main;
    entitySelection = GetComponent<EntitySelection>();
  }
}
