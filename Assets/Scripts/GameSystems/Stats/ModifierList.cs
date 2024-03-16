using System.Collections.Generic;

public class ModifierList<T> : List<T> where T : Modifier
{
  private bool isDirty;
  public bool IsDirty { get => isDirty; set => isDirty = value; }

  public ModifierList() : base()
  {
    isDirty = false;
  }

  public new void Add(T item)
  {
    base.Add(item);
    isDirty = true;
  }

  public new void Remove(T item)
  {
    base.Remove(item);
    isDirty = true;
  }

  public new void Clear()
  {
    base.Clear();
    isDirty = true;
  }
}
