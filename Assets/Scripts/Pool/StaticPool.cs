
using System.Collections.Generic;
using System.Linq;

public class StaticPool<T> where T : BasePoolMember
{
    private List<T> _list = new();
    public IEnumerable<T> Members => _list.AsEnumerable();

    private int _currentStep;

    public void Add(T target)
    {
        if (_list.Contains(target)) return;

        _list.Add(target);
    }

    public void Remove(T target)
    {
        if (_list.Contains(target))
        {
            _list.Remove(target);
        }
    }

    public T GetInactiveElement()
    {
        var targets = _list.AsEnumerable();

        var length = targets.Count();
        for (int i = 0; i < length; i++)
        {
            // Case: Step is out of list
            if (++_currentStep >= length)
            {
                _currentStep = 0;
            }

            // Case: Element at step is inactive
            var element = targets.ElementAt(_currentStep);
            if (!element.IsActive)
            {
                return element;
            }
        }

        return null;
    }
}
