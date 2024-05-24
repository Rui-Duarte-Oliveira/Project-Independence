using UnityEngine;
using UnityEngine.UI;

public class Building : WorldObject
{
  [SerializeField] private string buildingName;
  [SerializeField] private float constructionTimeInHours;

  private ConstructionEntity owner;
  private Image progressImage;

  private float currentConstructionTimeInHours;
  private float fillMax;

  public ConstructionEntity Owner { get => owner; set => owner = value; }
  public Image ProgressImage { get => progressImage; set => progressImage = value; }

  public virtual void OnConstructionStart(ConstructionEntity entity) 
  {
    owner = entity;
    SimulationController.OnHourUpdate += ConstructionLoop;
    fillMax = constructionTimeInHours;
  }

  public virtual void ConstructionLoop() 
  {
    ++currentConstructionTimeInHours;
    progressImage.fillAmount = currentConstructionTimeInHours / fillMax;
    if (constructionTimeInHours <= currentConstructionTimeInHours)
    {
      OnConstructionEnd();
      return;
    }
  }

  public virtual void OnConstructionEnd() 
  {
    SimulationController.OnHourUpdate -= ConstructionLoop;
    Owner.Manager.UpdateBuildingUI(Owner);
  }

  public virtual void OnDestruction() { }
}
