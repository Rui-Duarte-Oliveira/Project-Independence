using System;
using UnityEngine;

public class SimulationController : WorldObject
{
  [SerializeField, Tooltip("Amount of seconds it takes for a full day to pass.")] private float simulationDayDuration;

  public static Action OnHourUpdate;
  public static Action OnDayUpdate;

  private float simulationHourDuration;
  private float dayTimeCounter;
  private float hourTimeCounter;

  protected override void Awake()
  {
    base.Awake();

    SimulationTime.SimulationDayDuration = simulationDayDuration;

    simulationHourDuration = simulationDayDuration / SimulationTime.DayDuration;
    dayTimeCounter = 0.0f;
    hourTimeCounter = 0.0f;
  }

  protected override void UpdateTick(object sender, OnTickEventArgs eventArgs)
  {
    base.UpdateTick(sender, eventArgs);

    hourTimeCounter += eventArgs.DeltaTime;

    if (hourTimeCounter >= simulationHourDuration)
    {
      hourTimeCounter -= simulationHourDuration;
      dayTimeCounter += simulationHourDuration;
      OnHourUpdateCall();
    }

    if (dayTimeCounter >= simulationDayDuration) 
    {
      dayTimeCounter = 0.0f;
      OnDayUpdateCall();
    }
  }

  private void OnHourUpdateCall()
  {
    OnHourUpdate?.Invoke();
  }

  private void OnDayUpdateCall()
  {
    OnDayUpdate?.Invoke();
  }
}
