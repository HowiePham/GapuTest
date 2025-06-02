using System.Collections.Generic;
using UnityEngine;

public class BaseTemplate<T> : ScriptableObject
{
    [SerializeField] private List<T> visualTemplateList = new List<T>();

    public List<T> VisualTemplateList => visualTemplateList;
}