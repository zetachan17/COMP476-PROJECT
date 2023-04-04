using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(strg_seek), typeof(StatTracked))]
public class ScoutBahaviour : EntityBehaviour
{

    protected override Node SetupTree()
    {

        return new Selector(new() {
              new Sequence(new(){
                    new ObjectInSightCond(this),
                    new IsPowerUpCond(this),
                    new Selector(new()
                    {
                        new Sequence(new (){
                            new IsStealthPowerUpCond(this),
                            new SeekTarget(this),
                        }),
                        new Sequence(new()
                        {
                            new IsShieldPowerUp(this),
                            new IsBelowHalfHp(this),
                            new SeekTarget(this)
                        }),
                        new Sequence(new()
                        {
                            new CommunicateLocationToTeammate(this),
                            new Avoid(this)
                        })
                    })
              }),
              new Sequence(new() {
                    new ObjectInSightCond(this),
                    new IsEnemy(this),
                    new CommunicateLocationToTeammate(this),
                    new PersueFromDistance(this)
              }),
              new Sequence(new() {
                    new ObjectInSightCond(this),
                    new IsState(this),
                    new SeekTarget(this)
              }),
              new Sequence(new()
              {
                  new ObjectInSightCond(this),
                  new IsObstacle(this),
                  new Avoid(this)
              })
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}

