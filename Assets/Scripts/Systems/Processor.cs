using System;
using System.Collections.Generic;
using UnityEngine;


public class Processor : MonoBehaviour
{
  [SerializeField] private int tickRate;
  [SerializeField] private int culledObjectTickRateDivisor;

  public static Action StandardUpdate;

  public static Dictionary<Type, List<WorldObject>> WorldObjectRegistry;
  public static event EventHandler<OnTickEventArgs> UpdateTick;

  /// <summary>
  /// Runs X amount slower than the UpdateTick, in alternate frames.
  /// </summary>
  public static event EventHandler<OnTickEventArgs> CulledObjectUpdateTick;

  private OnTickEventArgs tickArgs;
  private OnTickEventArgs culledTickArgs;
  private float tickTimerMax;
  private float culledTickTimerMax;
  private float tickTimer;
  private float culledTickTimer;

  private void Awake()
  {
    WorldObjectRegistry = new Dictionary<Type, List<WorldObject>>();
  }

  private void Start()
  {
    tickArgs = new OnTickEventArgs(tickRate);
    tickTimerMax = 1.0f / tickRate;

    if(tickRate % culledObjectTickRateDivisor != 0)
    {
      Debug.LogError("CULLED OBJECT TICKRATE DIVISOR INCORRECT VALUE! \n CHECK CONDITIONAL STATEMENT!");
      return;
    }

    culledTickArgs = new OnTickEventArgs(tickRate / culledObjectTickRateDivisor);
    culledTickTimerMax = 1.0f / culledTickArgs.TickRate;
  }

  private void Update()
  {
    StandardUpdate?.Invoke();
    tickTimer += Time.deltaTime;
    culledTickTimer += Time.deltaTime;

    if (tickTimer >= tickTimerMax)
    {
      tickTimer -= tickTimerMax;

      tickArgs.PreviousFrameTime = tickArgs.CurrentFrameTime;
      tickArgs.CurrentFrameTime = Time.time;

      if (null != UpdateTick)
      {
        UpdateTick.Invoke(this, tickArgs);
      }
    }

    if(culledTickTimer >= culledTickTimerMax)
    {
      culledTickTimer -= culledTickTimerMax;

      culledTickArgs.PreviousFrameTime = tickArgs.CurrentFrameTime;
      culledTickArgs.CurrentFrameTime = Time.time;

      if (null != CulledObjectUpdateTick)
      {
        CulledObjectUpdateTick.Invoke(this, culledTickArgs);
      }
    }
  }
}
