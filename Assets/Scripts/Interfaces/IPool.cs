using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool
{
    Pool ParentPool { get; set; }
    bool StayOnScene { get; set; }
    void Instantiate(Pool poolParent);
    void Begin(Vector3 position, string tag, Vector3 pos);
    void End();
}
