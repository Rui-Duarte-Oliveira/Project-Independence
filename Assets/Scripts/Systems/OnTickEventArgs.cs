using System;

public class OnTickEventArgs : EventArgs
{
  private int tickRate;
  private float previousFrameTime;
  private float currentFrameTime;
  public int TickRate { get => tickRate; set => tickRate = value; }
  public float PreviousFrameTime { get => previousFrameTime; set => previousFrameTime = value; }
  public float CurrentFrameTime { get => currentFrameTime; set => currentFrameTime = value; }
  public float DeltaTime { get => currentFrameTime - previousFrameTime; }

  public OnTickEventArgs(int tickRate)
  {
    this.tickRate = tickRate;
    previousFrameTime = 0;
    currentFrameTime = 0;
  }
}
