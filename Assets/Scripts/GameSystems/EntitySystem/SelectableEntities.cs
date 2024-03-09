using System.Collections.Generic;
using UnityEngine;

public class SelectableEntities<T> : List<T> where T : SelectableEntity
{
  public new void Add(T item)
  {
    item.OnSelected();
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
