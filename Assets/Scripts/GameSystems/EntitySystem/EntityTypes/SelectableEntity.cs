using System.Collections.Generic;

public class SelectableEntity : Entity
{
  protected bool isSelected;
  public bool IsSelected { get => isSelected; }

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

    isSelected = false;
  }

  public virtual void OnSelected() => isSelected = true;

  public virtual void OnDeselected() => isSelected = false;
}
