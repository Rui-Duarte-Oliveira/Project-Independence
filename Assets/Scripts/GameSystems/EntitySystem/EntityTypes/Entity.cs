
public class Entity : WorldObject
{
  protected PlayerManager owner;
  public PlayerManager Owner { get => owner; set => owner = value; }
}
