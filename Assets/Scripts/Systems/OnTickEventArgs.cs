using System;

public class OnTickEventArgs : EventArgs
{
  private int tickRate;
  public int TickRate { get => tickRate; set => tickRate = value; }

  public OnTickEventArgs(int tickRate)
  {
    this.tickRate = tickRate;
  }
}
