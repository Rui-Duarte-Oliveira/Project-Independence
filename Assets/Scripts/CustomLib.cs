using System;
using System.Collections.Generic;

public static class CustomLib
{
  public static bool IsOfSameType(Type firstType, Type secondType)
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

  public static  HashSet<Type> GetAncestorTypes(Type type)
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
