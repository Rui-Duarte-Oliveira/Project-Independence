using System;
using UnityEngine;

[Serializable]
public class Modifier
{
  [SerializeField]
  private string modifierName;
  [SerializeField]
  private float modifierValue;

  public float ModifierValue { get => modifierValue; set => modifierValue = value; }
  public string ModifierName { get => modifierName; set => modifierName = value; }
}
