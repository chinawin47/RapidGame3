using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour , IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttzcks = 1f;
        
        Transform target;
        float timesSinceLastAttck = 0;

        // Update is called once per frame
        private void Update()
        {
            timesSinceLastAttck += Time.deltaTime;

            if (target == null) return;
            if (target != null && !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timesSinceLastAttck > timeBetweenAttzcks)
            {
              GetComponent<Animator>().SetTrigger("attack");
                timesSinceLastAttck = 0;
            } 
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }
        
        public void Cancel()
        { 
            target = null; 
        }

        //Animtion Event
        void Hit()
        {

        }
    }
}
