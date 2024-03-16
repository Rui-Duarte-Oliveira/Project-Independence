using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
  [SerializeField] private bool hasUpdate = true;
  [SerializeField] private bool isCullable = false;

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

    if (hasUpdate)
    {
      Processor.UpdateTick += UpdateTick;
      Processor.StandardUpdate += StandardUpdate;
    }
  }

  protected virtual void Start() { }
  protected virtual void FixedUpdate() { }
  protected virtual void OnEnable() { }
  protected virtual void OnDisable() { }


  public virtual void OnWorldObjectDestroyed()
  {
    Processor.WorldObjectRegistry[GetType()].Remove(this);
  }

  public virtual void OnWorldObjectCulled(bool value)
  {
    if(!isCullable || !hasUpdate)
    {
      return;
    }

    if (value)
    {
      Processor.UpdateTick -= UpdateTick;
      Processor.CulledObjectUpdateTick += CulledObjectUpdateTick;
      return;
    }

    Processor.UpdateTick += UpdateTick;
    Processor.CulledObjectUpdateTick -= CulledObjectUpdateTick;
  }

  protected virtual void StandardUpdate() { }
  protected virtual void UpdateTick(object sender, OnTickEventArgs eventArgs) { }
  protected virtual void CulledObjectUpdateTick(object sender, OnTickEventArgs eventArgs) { }
}
