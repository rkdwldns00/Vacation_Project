using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    public abstract void StartBuff(BuffSystem buffSystem);
    public abstract void UpdateBuff(BuffSystem buffSystem);
    public abstract void EndBuff(BuffSystem buffSystem);
}

