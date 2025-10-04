
using UnityEngine;

public interface IBasePoolMember
{
    bool IsActive { get; }
    void MarkAsActive();
    void MarkAsInactive();
    void Initialize(Vector3 position);
}
