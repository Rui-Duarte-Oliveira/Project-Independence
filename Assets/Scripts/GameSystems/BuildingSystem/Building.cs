using UnityEngine;
using UnityEngine.UI;

public class Building : WorldObject
{
  [SerializeField] private string buildingName;
  [SerializeField] private float constructionTimeInHours;

  private Entity owner;
  private Image progressImage;

  private float currentConstructionTimeInHours;
  private float fillMax;

  public Entity Owner { get => owner; set => owner = value; }
  public Image ProgressImage { get => progressImage; set => progressImage = value; }

  public virtual void OnConstructionStart(Entity entity) 
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
  }

  public virtual void OnDestruction() { }
}
