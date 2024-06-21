using System;
using System.Collections.Generic;

public class CompositeDisposable
{
    private readonly List<IDisposable> _disposables = new();

    public void Add(IDisposable disposable)
    {
        _disposables.Add(disposable);
    }

    public void DisposeAll()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}