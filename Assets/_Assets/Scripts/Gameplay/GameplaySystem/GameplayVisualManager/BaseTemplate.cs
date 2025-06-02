using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseTemplate<T> : ScriptableObject
{
    [SerializeField] private List<T> templateList = new List<T>();

    public List<T> TemplateList => templateList;
}