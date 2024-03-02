using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
  protected virtual void Awake()
  {
    Type objectType = GetType();

    if (Processor.WorldObjectRegistry.ContainsKey(objectType))
    {
      Processor.WorldObjectRegistry[objectType].Add(this);
    }
    else
    {
      Processor.WorldObjectRegistry.Add(objectType, new List<WorldObject>() { this });
    }

    Processor.UpdateTick += UpdateTick;
    Processor.CulledObjectUpdateTick += CulledObjectUpdateTick;
  }

  protected virtual void Start() { }
  protected virtual void FixedUpdate() { }
  protected virtual void OnEnable() { }
  protected virtual void OnDisable() { }


  public virtual void OnWorldObjectDestroyed()
  {
    Processor.WorldObjectRegistry[GetType()].Remove(this);
  }

  protected virtual void UpdateTick(object sender, OnTickEventArgs eventArgs) { }
  protected virtual void CulledObjectUpdateTick(object sender, OnTickEventArgs eventArgs) { }
}
