using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HiddenItemVisualTemplate", menuName = "Data/Template/HiddenItemVisualTemplate", order = 1)]
public class HiddenItemVisualTemplate : ScriptableObject
{
    [SerializeField] private List<Sprite> hiddenItemVisualTemplateList = new List<Sprite>();

    public List<Sprite> HiddenItemVisualTemplateList => hiddenItemVisualTemplateList;
}