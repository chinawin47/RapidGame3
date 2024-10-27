using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefad = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponRange = 2f;

        public void Spawn(Transform handTransfor, Animator animator)
        {
            Instantiate(weaponPrefad, handTransfor);
            animator.runtimeAnimatorController = animatorOverride;
        }

        public float GetDamage() 
        { 
            return weaponDamage;
        }

        public float GetRange() 
        { 
            return weaponRange;
        }
    }
}
