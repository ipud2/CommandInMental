using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSTank
{
//Tank's base calss
    public class BTank:MonoBehaviour
    {
        #region PROPERITY
        [SerializeField]
        public class Properity
        {
            //ID
            public int id; //the tank's id

            public string name; //
            public int teamNumber; //

            //life
            public float healthPoint;

            //Ability
            public float attackPoint; //

            public float defensePercent; //zero to one ::=> how much percent can the tank reduce the taking attackDamage

            public float fireInterval; //the interval between two fires
            public float fireDistance; //how much distance can the bullet reaches
            public float speed; //

            public float ability1CoolDownTime;
            public float ability2CoolDownTime;
            public float ability3CoolDownTime;

            public bool isAbilityLocked;     //the boolean is used to identify whether the tank is using an ability which is exclusive

            //Ability cool down marks
            public float fireEllapse;
            public float ability1Elapse;
            public float ability2Elapse;
            public float ability3Elapse;

            public bool isTriggerFire;
            public bool  isTriggerAbility1;
            public bool  isTriggerAbility2;
            public bool  isTriggerAbility3;
            public bool canMove;             //whether the tank can move
            public bool isInterrupted;       //ability's animation can be interrupted by the player or other tanks's ability
            //score
            public int killNumber;
            public int deadNumber;
            //Constructor
            public Properity()
            {
                int id          = 0;
                name            = "tank";
                teamNumber      = 0;
                healthPoint     = 100;
                attackPoint     = 10;
                defensePercent  = 0.0f;
                fireDistance    = 5.0f;
                fireInterval    = 0.5f;
                speed           = 10;
                killNumber      = 0;
                deadNumber      = 0;

                ability1CoolDownTime = 2.0f;
                ability2CoolDownTime = 2.0f;
                ability3CoolDownTime = 2.0f;

                fireEllapse     = 0.0f;
                ability1Elapse  = 0.0f;
                ability2Elapse  = 0.0f;
                ability3Elapse  = 0.0f;

                isTriggerFire     = false;
                isTriggerAbility1 = false;
                isTriggerAbility2 = false;
                isTriggerAbility3 = false;

                isAbilityLocked   = false;

                canMove = true;
                isInterrupted = false;
            }
        }
        #endregion

        protected Properity _properity;
        protected Damage _damage;

        //abilities mark's wrapper
        public bool CanUseAbility1
        {
            get { return !_properity.isTriggerAbility1 && !_properity.isAbilityLocked; }
        }
        public bool CanUseAbility2
        {
            get { return !_properity.isTriggerAbility2 && !_properity.isAbilityLocked; }
        }
        public bool CanUseAbility3
        {
            get { return !_properity.isTriggerAbility3 && !_properity.isAbilityLocked; }
        }
        public bool CanMove
        {
            get { return _properity.canMove; }
        }
        public bool IsInterrupted
        {
            get { return _properity.isInterrupted; }
        }
        public BTank()
        {
            _properity = new Properity();
            _damage = new Damage();
        }

        public virtual void Fire()
        {

        }

        public virtual void UseAbility1()
        {

        }

        public virtual void UseAbility2()
        {

        }

        public virtual void UseAbility3()
        {

        }

        protected virtual void Move()
        {

        }

        protected virtual void HandleInput()
        {

        }

        protected virtual Damage CreateDamage()
        {
            _damage.damageSenderId = _properity.id;
            _damage.damageSenderTeamNumber = _properity.teamNumber;
            _damage.attackPoint = _properity.attackPoint;
            return _damage;
        }

        protected virtual void TakeDamage(Damage damage)
        {
            _properity.healthPoint -= damage.attackPoint * (1 - _properity.defensePercent);
        }

        protected virtual void OnDeath()
        {

        }

        protected virtual void OnBirth()
        {

        }

        protected virtual void Living()
        {

        }
        /// <summary>
        /// Do not need to override this functuion
        /// </summary>
        protected virtual void AbilityCoolDown()
        {
            if (_properity.isTriggerAbility1)
            {
                _properity.ability1Elapse += Time.deltaTime;
                if (_properity.ability1Elapse > _properity.ability1CoolDownTime)
                {
                    _properity.ability1Elapse = 0.0f;
                    _properity.isTriggerAbility1 = false;
                }
            }
            if (_properity.isTriggerAbility2)
            {
                _properity.ability2Elapse += Time.deltaTime;
                if (_properity.ability2Elapse > _properity.ability2CoolDownTime)
                {
                    _properity.ability2Elapse = 0.0f;
                    _properity.isTriggerAbility2 = false;
                }
            }

            if (_properity.isTriggerAbility3)
            {
                _properity.ability3Elapse += Time.deltaTime;
                if (_properity.ability3Elapse > _properity.ability3CoolDownTime)
                {
                    _properity.ability3Elapse = 0.0f;
                    _properity.isTriggerAbility3 = false;
                }
            }
        }

        //buff and debuff
        //public virtual void TakeBuff(Buff buff){}

    }
}