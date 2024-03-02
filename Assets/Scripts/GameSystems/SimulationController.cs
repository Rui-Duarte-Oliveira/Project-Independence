using System;
using UnityEngine;

public class SimulationController : WorldObject
{
  [SerializeField] private int secondsToTick;

  public static Action SimulationLoop;

  private int tickCounter;

  protected override void Awake()
  {
    base.Awake();
    tickCounter = 0;
  }

  protected override void UpdateTick(object sender, OnTickEventArgs eventArgs)
  {
    ++tickCounter;

    if(tickCounter == (eventArgs.TickRate * secondsToTick)) 
    {
      tickCounter = 0;
      Simulate();
    }
  }

  private void Simulate()
  {
    SimulationLoop?.Invoke();
  }
}
