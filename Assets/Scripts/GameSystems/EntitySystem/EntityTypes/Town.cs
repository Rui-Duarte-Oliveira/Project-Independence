using UnityEngine;

public class Town : StationaryEntity
{
  [SerializeField] private Building[] buildingSlots;
  [SerializeField] private int maxBuildingSlots;
}
