using System;
using System.Collections.Generic;

public class SelectableEntities<T> : List<T> where T : SelectableEntity
{
  public new void Add(T item)
  {
    item.OnSelected();

    if(Count > 0)
    {
      if(!IsOfSameType(this[0].GetType(), item.GetType()))
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

  private bool IsOfSameType(Type firstType, Type secondType)
  {
    if (firstType == secondType)
    {
      return true;
    }

    if (firstType.IsAssignableFrom(secondType) || secondType.IsAssignableFrom(firstType))
    {
      return true;
    }

    HashSet<Type> firstAncestors = GetAncestorTypes(firstType);
    HashSet<Type> secondAncestors = GetAncestorTypes(secondType);

    foreach (Type ancestor in firstAncestors)
    {
      if (secondAncestors.Contains(ancestor))
      {
        return true;
      }
    }

    return false;
  }

  private HashSet<Type> GetAncestorTypes(Type type)
  {
    HashSet<Type> ancestors = new HashSet<Type>();
    Type currentType = type.BaseType;

    while (currentType != null && currentType != typeof(SelectableEntity))
    {
      ancestors.Add(currentType);
      currentType = currentType.BaseType;
    }

    if (ancestors.Contains(typeof(SelectableEntity)))
    {
      ancestors.Remove(typeof(SelectableEntity));
    }

    return ancestors;
  }
}
