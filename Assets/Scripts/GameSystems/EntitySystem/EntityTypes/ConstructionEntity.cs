using UnityEngine;

public class ConstructionEntity : StationaryEntity
{
  [SerializeField] private Building[] buildingSlots;
  [SerializeField, Range(0, 6)] private int maxBuildingSlots;

  public Building[] BuildingSlots { get => buildingSlots; }
  public int MaxBuildingSlots { get => maxBuildingSlots; }

  public virtual void UpdateBuildingUI()
  {
    manager.UpdateBuildingUI(this);
  }
}
