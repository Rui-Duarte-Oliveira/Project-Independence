using System;
using System.Collections.Generic;

public class SelectableEntities<T> : List<T> where T : SelectableEntity
{
  public new void Add(T item)
  {
    item.OnSelected();

    if(Count > 0)
    {
      if(!CustomLib.IsOfSameType(this[0].GetType(), item.GetType()))
      {
        Clear();
      }
    }

    base.Add(item);
  }

  public new void Remove(T item)
  {
    item.OnDeselected();
    base.Remove(item);
  }

  public new void Clear() 
  { 
    for (int i = 0; i < Count; ++i)
    {
      this[i].OnDeselected();
    }

    base.Clear();
  }

  public void Clear(T item)
  {
    List<T> itemsToRemove = new List<T>();
    for (int i = 0; i < Count; ++i)
    {
      if (this[i] == item)
      {
        continue;
      }

      itemsToRemove.Add(this[i]);
    }

    for (int i = 0; i < itemsToRemove.Count; ++i)
    {
      Remove(itemsToRemove[i]);
    }
  }
}
