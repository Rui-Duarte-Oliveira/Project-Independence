using System;
using System.Collections.Generic;
using UnityEngine;


public class Processor : MonoBehaviour
{
  [SerializeField] private int tickRate;
  [SerializeField] private int culledObjectUpdateTickDivisor;

  public static Action StandardUpdate;

  public static Dictionary<Type, List<WorldObject>> WorldObjectRegistry;
  public static event EventHandler<OnTickEventArgs> UpdateTick;

  /// <summary>
  /// Runs X amount slower than the UpdateTick.
  /// </summary>
  public static event EventHandler<OnTickEventArgs> CulledObjectUpdateTick;

  private OnTickEventArgs tickArgs;
  private float tickTimerMax;
  private float tickTimer;
  private int tick;

  private void Awake()
  {
    WorldObjectRegistry = new Dictionary<Type, List<WorldObject>>();
  }

  private void Start()
  {
    tickArgs = new OnTickEventArgs(tickRate);
    tick = 0;
    tickTimerMax = 1.0f / tickRate;
  }

  private void Update()
  {
    StandardUpdate?.Invoke();
    tickTimer += Time.deltaTime;

    if (tickTimer >= tickTimerMax)
    {
      tickTimer -= tickTimerMax;
      ++tick;

      if (null != UpdateTick)
      {
        UpdateTick.Invoke(this, tickArgs);
      }

      if (0 == tick / culledObjectUpdateTickDivisor)
      {
        tick = 0;

        if (null != CulledObjectUpdateTick)
        {
          CulledObjectUpdateTick.Invoke(this, tickArgs);
        }
      }
    }
  }
}
