using System.Collections.Generic;

public class SelectableEntity : Entity
{
  protected override void Awake()
  {
    base.Awake();
    if (Processor.WorldObjectRegistry.ContainsKey(typeof(SelectableEntity)))
    {
      Processor.WorldObjectRegistry[typeof(SelectableEntity)].Add(this);
    }
    else
    {
      Processor.WorldObjectRegistry.Add(typeof(SelectableEntity), new List<WorldObject>() { this });
    }
  }

  public virtual void OnSelected() { }

  public virtual void OnDeselected() { }
}
