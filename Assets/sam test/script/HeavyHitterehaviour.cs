using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using UnityEngine.Experimental.AI;

public class HeavyHitterehaviour : EntityBehaviour
{
    protected override Node SetupTree() {
        return new Selector(new() { 
            new Sequence(new(){ 
                new ObjectInSightCond(this),
                new TargetIsPrefered(this),
                new Selector(new()
                {
                    new Sequence(new(){ 
                        new IsPowerUpCond(this),
                        new SeekTarget(this)
                    }),
                    new Sequence(new()
                    {
                        new IsEnemy(this),
                        new PersueAndFireMissile(this),
                    }),
                    new Sequence(new()
                    {
                        new IsState(this),
                        new SeekTarget(this)
                    })
                })
            }),
            new Sequence(new(){ 
                new ObjectInSightCond(this),
                new ClosestObject(this),
                new Avoid(this)
            }),
        });
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
