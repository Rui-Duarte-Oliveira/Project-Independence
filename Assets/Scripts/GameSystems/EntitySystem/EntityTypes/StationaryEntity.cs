using System.Collections.Generic;
using UnityEngine;

public class StationaryEntity : SelectableEntity
{
  [SerializeField] private Material materialToSwap;
  [SerializeField] private List<MeshRenderer> meshRenderer;

  private Material defaultMat;

  public override void OnSelected()
  {
    base.OnSelected();

    for (int i = 0; i < meshRenderer.Count; i++)
    {
      defaultMat = meshRenderer[i].material;
      meshRenderer[i].material = materialToSwap;
    }
  }

  public override void OnDeselected()
  {
    base.OnDeselected();
    for (int i = 0; i < meshRenderer.Count; i++)
    {
      meshRenderer[i].material = defaultMat;
    }
  }
}
