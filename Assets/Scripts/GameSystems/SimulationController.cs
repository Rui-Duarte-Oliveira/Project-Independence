using System;
using UnityEngine;

public class SimulationController : WorldObject
{
  [SerializeField] private float simulationLoopMaxTime;
  [SerializeField] private int simulationLoopTime;

  public static Action SimulationTimedUpdate;

  public static Action PreSimulationLoop;
  public static Action SimulationLoop;
  public static Action PostSimulationLoop;

  private float simulationTime;
  private float simulationTimeCounter;
  private float loopTimeCounter;

  protected override void Awake()
  {
    base.Awake();
    loopTimeCounter = 0;
    simulationTime = 1.0f * simulationLoopTime;
  }

  protected override void UpdateTick(object sender, OnTickEventArgs eventArgs)
  {
    base.UpdateTick(sender, eventArgs);

    loopTimeCounter += eventArgs.DeltaTime;
    simulationTimeCounter += eventArgs.DeltaTime;

    if (simulationTimeCounter >= simulationTime)
    {
      simulationTimeCounter -= simulationTime;
      TimedUpdateCall();
    }

    if (loopTimeCounter >= simulationLoopMaxTime) 
    {
      loopTimeCounter -= simulationLoopMaxTime;

      PreLoopCall();
      LoopCall();
      PostLoopCall();
    }
  }

  private void TimedUpdateCall()
  {
    SimulationTimedUpdate?.Invoke();
  }

  private void PreLoopCall()
  {
    PreSimulationLoop?.Invoke();
  }

  private void LoopCall()
  {
    SimulationLoop?.Invoke();
  }

  private void PostLoopCall()
  {
    PostSimulationLoop?.Invoke();
  }
}
