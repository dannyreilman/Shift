using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface CursorUser
{
    float GetMillisecondDelay();
    FlowManager.AcceptNote GetCursor();
}