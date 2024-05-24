using UnityEngine;

public class StationaryEntity : SelectableEntity
{
  [SerializeField] private string entityName;
  [SerializeField] private string entityDescription;

  protected StationaryEntityUIManager manager;

  public string EntityName { get => entityName; }
  public string EntityDescription { get => entityDescription; }
  public StationaryEntityUIManager Manager { get => manager; }

  protected override void Start()
  {
    base.Start();
    manager = (StationaryEntityUIManager)Processor.WorldObjectRegistry[typeof(StationaryEntityUIManager)][0];
  }

  public override void OnSelected()
  {
    base.OnSelected();

    manager.CurrentEntity = this;
  }

  public override void OnDeselected()
  {
    base.OnDeselected();

    if(manager.CurrentEntity == this)
    {
      manager.CurrentEntity = null;
    }
  }
}
