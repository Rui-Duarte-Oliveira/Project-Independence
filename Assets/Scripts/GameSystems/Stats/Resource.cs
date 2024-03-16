using TMPro;
using UnityEngine;

public class Resource : WorldObject
{
  [Header("Values")]
  [SerializeField] private string resourceName;
  [SerializeField] private int resourceQuantity;

  [Header("Object References")]
  [SerializeField] private TMP_Text resourceNameText;
  [SerializeField] private TMP_Text resourceQuantityText;

  private ModifierList<Modifier> resourceModifiers;
  private float currentModifier;

  public ModifierList<Modifier> ResourceModifier { get => resourceModifiers; set => resourceModifiers = value; }
  public string ResourceName { get => resourceName; }
  public int ResourceQuantity 
  { 
    get => resourceQuantity;
    set
    {
      resourceQuantity = value;
      resourceQuantityText.text = resourceQuantity.ToString();
    }
  }

  protected override void Awake()
  {
    base.Awake();

    resourceNameText.text = resourceName + ":";
    resourceQuantityText.text = resourceQuantity.ToString();
  }

  public void AddResource(int quantityToAdd) 
  {
    if (resourceModifiers.IsDirty)
    {
      currentModifier = GetCalculatedModifierValue();
      resourceModifiers.IsDirty = false;
    }

    ResourceQuantity += Mathf.RoundToInt(quantityToAdd * currentModifier);
  }

  public void RemoveResource(int quantityToRemove) 
  {
    ResourceQuantity -= quantityToRemove;
  }

  public bool HasEnoughResourceQuantity(int quantityToRemove) 
  { 
    return ResourceQuantity >= quantityToRemove; 
  }

  private float GetCalculatedModifierValue()
  {
    if(0 == resourceModifiers.Count) 
    {
      return 1.0f;
    }

    float modifierValue = 1.0f;

    foreach(Modifier modifier in resourceModifiers)
    {
      modifierValue += modifier.ModifierValue;
    }

    return modifierValue;
  }
}