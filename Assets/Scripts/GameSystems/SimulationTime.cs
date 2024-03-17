using System;
using UnityEngine;

public static class SimulationTime
{
  private static float simulationDayDuration = 6;
  private static float dayDuration = 24;


  /// <summary>
  /// Amount of seconds a day takes to finish.
  /// </summary>
  public static float SimulationDayDuration { get => simulationDayDuration; set => simulationDayDuration = value; }

  /// <summary>
  /// Amount of hours in a day.
  /// </summary>
  public static float DayDuration { get => dayDuration; }
}
