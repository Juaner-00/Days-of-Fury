using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool
{
    void Instantiate();
    void Begin(Vector3 position, string tag);
    void End();
}
