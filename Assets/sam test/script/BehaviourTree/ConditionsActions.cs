using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace BT {

    internal class ObjectInSightCond : Node
    {
        private EntityBehaviour scoutBahaviour;

        public ObjectInSightCond(EntityBehaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            // Ray cast along object FOV 140 degress
            const int FOV = 140;
            const int MAX_DISTANCE = 50; // Meteres

            Collider[] objectsInRadius = Physics.OverlapSphere(scoutBahaviour.transform.position, MAX_DISTANCE, scoutBahaviour.objectsToDetect);
            List<GameObject> objectsInSight = new();

            foreach (Collider obj in objectsInRadius)
            {
                Vector3 directionToObject = (obj.transform.position - scoutBahaviour.transform.position).normalized;
                float angleToObject = Vector3.Angle(scoutBahaviour.transform.forward, directionToObject);

                if (angleToObject <= FOV * 0.5f)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(scoutBahaviour.transform.position, directionToObject, out hit, MAX_DISTANCE, scoutBahaviour.objectsToDetect))
                    {
                        if (hit.collider == obj)
                        {
                            // This object is in the Field of View
                            objectsInSight.Add(obj.gameObject);
                            Debug.Log($"[{GetType().Name}]: {obj.name} is in the FOV.");
                        }
                    }
                }
            }

            if (objectsInSight.Count > 0)
            {
                // Set data
                SetData("objects_in_sight", objectsInSight);
                return NodeState.SUCCESS;
            }
            else {
                SetData("objects_in_sight", objectsInSight);
                return NodeState.FAILURE;
            }
        }
    }

    internal class IsPowerUpCond : Node
    {
        private EntityBehaviour scoutBahaviour;

        public IsPowerUpCond(EntityBehaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            // Get object from data
            List<GameObject> objs = GetData<List<GameObject>>("objects_in_sight");
            //Assert.IsNotNull(objs);

            foreach (var o in objs)
            {
                if (o.tag == "powerup") {
                    SetData("to_seek", o);
                    return NodeState.SUCCESS;
                }
            }
            return NodeState.FAILURE;
        }
    }

    internal class IsStealthPowerUpCond : Node
    {
        private ScoutBahaviour scoutBahaviour;

        public IsStealthPowerUpCond(ScoutBahaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            GameObject powerup = GetData<GameObject>("to_seek");
            //Assert.IsNotNull(powerup);
            //Assert.IsNotNull(powerup.GetComponent<PowerUp>(), "Powerup taged objects must have PowerUp.cs Component");

            return powerup.GetComponent<PowerUp>().modifies == StatTracked.Stat.Visibility ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }

    internal class SeekTarget : Node
    {
        private EntityBehaviour bahaviour;

        public SeekTarget(EntityBehaviour scoutBahaviour)
        {
            this.bahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            GameObject to_seek = GetData<GameObject>("to_seek");
            //Assert.IsNotNull(to_seek);

            // TODO: This returns a Vector3, but we need to set the velocity of the agent
            bahaviour.GetComponent<strg_steerinAgent>().setSteering(
                strg_steerinAgent.SteeringOptions.Persue,
                new() { to_seek}
            );
            return NodeState.SUCCESS;
        }
    }

    internal class IsShieldPowerUp : Node
    {
        private ScoutBahaviour scoutBahaviour;

        public IsShieldPowerUp(ScoutBahaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            GameObject powerup = GetData<GameObject>("to_seek");
            //Assert.IsNotNull(powerup);
            //Assert.IsNotNull(powerup.GetComponent<PowerUp>(), "Powerup taged objects must have PowerUp.cs Component");

            return powerup.GetComponent<PowerUp>().modifies == StatTracked.Stat.Sheild ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }

    internal class IsBelowHalfHp : Node
    {
        private ScoutBahaviour scoutBahaviour;

        public IsBelowHalfHp(ScoutBahaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            StatTracked stats = scoutBahaviour.GetComponent<StatTracked>();
            var maxHp = stats.GetStat(StatTracked.Stat.MaxHp);
            var hp = stats.GetStat(StatTracked.Stat.Hp);

            return hp <= maxHp * 0.5 ? NodeState.SUCCESS: NodeState.FAILURE;
        }
    }

    internal class CommunicateLocationToTeammate : Node
    {
        private ScoutBahaviour scoutBahaviour;

        public CommunicateLocationToTeammate(ScoutBahaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            GameObject powerup = GetData<GameObject>("to_seek");
            //Assert.IsNotNull(powerup);
            scoutBahaviour.teammate.preferedTarget = powerup;
            return NodeState.SUCCESS;
        }
    }

    internal class Avoid : Node
    {
        private EntityBehaviour scoutBahaviour;

        public Avoid(EntityBehaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            GameObject powerup = GetData<GameObject>("to_seek");
            //Assert.IsNotNull(powerup);

            // TODO: This returns a vector3 but we need to set the velocity of the agent
            scoutBahaviour.GetComponent<strg_steerinAgent>().setSteering(
                strg_steerinAgent.SteeringOptions.Evade,
                new() { powerup }
            );

            return NodeState.SUCCESS;
        }
    }

    internal class IsEnemy : Node
    {
        private EntityBehaviour scoutBahaviour;

        public IsEnemy(EntityBehaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            // Get object from data
            List<GameObject> objs = GetData<List<GameObject>>("objects_in_sight");
            //Assert.IsNotNull(objs);

            foreach (var o in objs)
            {
                if ((o.tag == "player" || o.tag == "ai") && o != scoutBahaviour.teammate)
                {
                    SetData("to_seek", o);
                    return NodeState.SUCCESS;
                }
            }
            return NodeState.FAILURE;
        }
    }

    internal class PersueFromDistance : Node
    {
        private ScoutBahaviour scoutBahaviour;

        public PersueFromDistance(ScoutBahaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            GameObject enemy = GetData<GameObject>("to_seek");
            //Assert.IsNotNull(enemy);

            scoutBahaviour.GetComponent<strg_steerinAgent>().setSteering(
                     strg_steerinAgent.SteeringOptions.Persue, new() { enemy }
            );

            return NodeState.SUCCESS;
        }
    }

    internal class IsState : Node
    {
        private EntityBehaviour scoutBahaviour;

        public IsState(EntityBehaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            // Get object from data
            List<GameObject> objs = GetData<List<GameObject>>("objects_in_sight");
            //Assert.IsNotNull(objs);

            foreach (var o in objs)
            {
                if (o.tag == "stat")
                {
                    SetData("to_seek", o);
                    return NodeState.SUCCESS;
                }
            }
            return NodeState.FAILURE;
        }
    }

    internal class IsObstacle : Node
    {
        private ScoutBahaviour scoutBahaviour;

        public IsObstacle(ScoutBahaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            // Get object from data
            List<GameObject> objs = GetData<List<GameObject>>("objects_in_sight");
            //Assert.IsNotNull(objs);

            foreach (var o in objs)
            {
                if (o.tag == "obstacle")
                {
                    SetData("to_seek", o);
                    return NodeState.SUCCESS;
                }
            }
            return NodeState.FAILURE;
        }
    }
    internal class ClosestObject : Node
    {
        private EntityBehaviour scoutBahaviour;

        public ClosestObject(EntityBehaviour scoutBahaviour)
        {
            this.scoutBahaviour = scoutBahaviour;
        }

        public override NodeState Evaluate()
        {
            // Get object from data
            List<GameObject> objs = GetData<List<GameObject>>("objects_in_sight");
            //Assert.IsNotNull(objs);

            objs.Sort((a, b) => 
                Vector3.Distance(a.transform.position, scoutBahaviour.transform.position)
                .CompareTo(
                    Vector3.Distance(b.transform.position, scoutBahaviour.transform.position))
                );

            SetData("to_seek", objs[0]);

            return NodeState.SUCCESS;
        }
    }

    internal class TargetIsPrefered : Node
    {
        private HeavyHitterehaviour heavyHitterehaviour;

        public TargetIsPrefered(HeavyHitterehaviour heavyHitterehaviour)
        {
            this.heavyHitterehaviour = heavyHitterehaviour;   
        }

        public override NodeState Evaluate()
        {
            if (heavyHitterehaviour.preferedTarget == null)
                return NodeState.FAILURE;


            List<GameObject> objs = GetData<List<GameObject>>("objects_in_sight");
            //Assert.IsNotNull(objs);

            foreach (GameObject obj in objs)
            {
                if (obj == heavyHitterehaviour.preferedTarget)
                {
                    SetData("to_seek", obj);
                    return NodeState.SUCCESS;
                }
            }

            return NodeState.FAILURE;
        }
    }

    internal class PersueAndFireMissile : Node
    {
        private HeavyHitterehaviour heavyHitterehaviour;

        public PersueAndFireMissile(HeavyHitterehaviour heavyHitterehaviour)
        {
            this.heavyHitterehaviour = heavyHitterehaviour;
        }

        public override NodeState Evaluate()
        {
            GameObject enemy = GetData<GameObject>("to_seek");
            //Assert.IsNotNull(enemy);

            // This returns a vector3 but we need to set the velocity of the agent
            //Assert.IsTrue(false);
            heavyHitterehaviour.GetComponent<strg_steerinAgent>().setSteering(
                      strg_steerinAgent.SteeringOptions.Persue, new() { enemy });

            heavyHitterehaviour.GetComponent<FireMissile>().Fire();

            return NodeState.SUCCESS;
        }
    }

    internal class DefaultSeeting : Node {
        private EntityBehaviour entityBehaviour;

        public DefaultSeeting(EntityBehaviour entityBehaviour)
        {
            this.entityBehaviour = entityBehaviour;
        }

        public override NodeState Evaluate()
        {
            strg_steerinAgent agent = entityBehaviour.GetComponent<strg_steerinAgent>();
            //Assert.IsNotNull(agent);

            agent.setSteering(strg_steerinAgent.SteeringOptions.Other, null);

            return NodeState.SUCCESS;
        }
    }
}