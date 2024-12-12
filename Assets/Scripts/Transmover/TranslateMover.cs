using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateMover : Transformcontroller
{
    void Update()
    {
       MoveTranslate(new Vector2(-0.01f, 0f));
    }
}
