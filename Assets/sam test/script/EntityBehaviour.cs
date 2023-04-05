using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EntityBehaviour : BT.Tree
{
    public LayerMask objectsToDetect;
    public EntityBehaviour teammate;
    public GameObject preferedTarget;
}

