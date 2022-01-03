﻿using System.Collections;

namespace DBot.Domain;

public abstract class BaseHierarchicalCodeElement : HierarchicalCodeElement
{
    protected readonly List<CodeElement> _elements = new();

    public string Name { get; }

    protected BaseHierarchicalCodeElement(string name) => Name = name;
    
    public void AddChild(CodeElement element) => _elements.Add(element);
    public IEnumerator<CodeElement> GetEnumerator() => _elements.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}