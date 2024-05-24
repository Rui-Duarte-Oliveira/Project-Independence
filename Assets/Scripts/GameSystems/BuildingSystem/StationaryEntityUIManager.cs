using TMPro;
using UnityEngine;

public class StationaryEntityUIManager : WorldObject
{
  [SerializeField] private TMP_Text entityNameText;
  [SerializeField] private TMP_Text entityDescriptionText;

  [SerializeField] private GameObject buildingSlots;

  private StationaryEntity currentEntity;

  public StationaryEntity CurrentEntity
  {
    get => currentEntity;
    set
    {
      currentEntity = value;

      if (currentEntity != null)
      {
        gameObject.SetActive(true);
        UpdateUI();
      }
      else
      {
        gameObject.SetActive(false);
      }
    }
  }

  protected override void Awake()
  {
    base.Awake();
    CurrentEntity = null;
  }

  public void UpdateBuildingUI(ConstructionEntity town)
  {
    for (int i = 0; i < buildingSlots.transform.childCount; ++i)
    {
      buildingSlots.transform.GetChild(i).gameObject.SetActive(i < town.MaxBuildingSlots);
    }
  }

  private void UpdateUI()
  {
    bool isTown = CustomLib.IsOfSameType(currentEntity.GetType(), typeof(ConstructionEntity));

    UpdateText(isTown);
    buildingSlots.SetActive(isTown);

    if (isTown)
    {
      UpdateBuildingUI((ConstructionEntity)currentEntity);
    }
  }

  private void UpdateText(bool isTown)
  {
    entityNameText.text = CurrentEntity.EntityName;

    if (isTown)
    {
      entityDescriptionText.text = "";
      return;
    }

    entityDescriptionText.text = CurrentEntity.EntityDescription;
  }
}
