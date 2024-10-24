using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttzcks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float timesSinceLastAttck = 0;

        // Update is called once per frame
        private void Update()
        {
            timesSinceLastAttck += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (target != null && !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timesSinceLastAttck > timeBetweenAttzcks)
            {
                // This will trigger the Hit() event.
                GetComponent<Animator>().SetTrigger("attack");
                timesSinceLastAttck = 0;
            }
        }
        //Animtion Event
        void Hit()
        {
            target.TakeDamage(weaponDamage);
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

    }
}