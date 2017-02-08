using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSTank
{
    [RequireComponent(typeof(Rigidbody))]
    public class Tank_Green : BTank
    {
        public VirtualJoystic vj;

        public Transform muzzleTransform; //the position to generate bullet(aka muzzlePos in tank_green's )
        public Transform dustTransform; //the position of dustFX
        public GameObject dustGoPrefab;
        public GameObject bulletGoPrefab;
        public GameObject bulletWithSmokeGoPrefab_41; //for ability1
        public GameObject bulletWithSmokeGoPrefab_43;
        private Rigidbody _rb;
        private bool _isAbility3Used;     //used for player's moving to interrupt ability3's shooting animation

        // Use this for initialization
        void Start()
        {
            OnBirth();
        }

        protected override void OnBirth()
        {
            _rb = GetComponent<Rigidbody>();

            //reset ability2's cool down time
            _properity.ability2CoolDownTime = 5.0f;
            _properity.ability3CoolDownTime = 6.0f;
            _isAbility3Used = false;
        }

        protected override void Living()
        {
            HandleInput();
            AbilityCoolDown();
        }

        void Update()
        {
            Living();
        }

        protected override void HandleInput()
        {   if(vj!=null)
            if (vj.hasInput() && CanMove)
            {
                if (_isAbility3Used)
                {
                    _properity.isInterrupted = true;
                }
                Move();
            }
        }

        protected override void Move()
        {
            transform.LookAt(transform.position +
                             new Vector3(vj.InputVector.x, transform.position.y, vj.InputVector.y));
            _rb.MovePosition(transform.position + transform.forward * Time.deltaTime * _properity.speed);
        }

        public override void Fire()
        {
            Instantiate(bulletGoPrefab, muzzleTransform.position, transform.rotation);
        }

        public override void UseAbility1()
        {
            if (CanUseAbility1)
            {
                _properity.isTriggerAbility1 = true;
                Instantiate(bulletWithSmokeGoPrefab_41, muzzleTransform.position, transform.rotation);
            }
        }

        public override void UseAbility2()
        {
            if (CanUseAbility2)
            {
                _properity.isTriggerAbility2 = true;
                StartCoroutine(A2_SpeedUp());
            }
        }

        public override void UseAbility3()
        {
            if (CanUseAbility3)
            {
                _properity.isTriggerAbility3 = true;
                StartCoroutine(A3_RotatingShoot());
            }
        }

        //speed up for 3seconds
        private IEnumerator A2_SpeedUp()
        {
            float speedUpTime = 5.0f;
            float sp = _properity.speed;
            _properity.speed *= 2.0f;
            bool isDone = false;
            float elapse = 0.0f;

            //FX
            //Instantiate(dustGoPrefab,Quaternion.identity, dustTransform.position,transform);
            Instantiate(dustGoPrefab, dustTransform.position, Quaternion.identity, dustTransform);
            while (!isDone)
            {
                elapse += Time.deltaTime;
                if (elapse >= speedUpTime)
                {
                    isDone = true;
                }
                yield return null;
            }
            _properity.speed = sp;
        }

        //ability3's animating shooting is ability exclusive and this ability can be interrupted by the player's move or other tank's ability
        private IEnumerator A3_RotatingShoot()
        {
            _properity.isAbilityLocked = true;
            _isAbility3Used = true;
            float animTime = 1.0f;
            float elapse = 0.0f;
            bool isDone = false;
            float rotSpeed = 360.0f / animTime;
            //Quaternion originalRotation = transform.rotation;
            while (!isDone)
            {
                elapse += Time.deltaTime;
                if (elapse >= animTime || IsInterrupted)
                {
                    isDone = true;
                }
                transform.Rotate(Vector3.up,rotSpeed*Time.deltaTime);
                Instantiate(bulletWithSmokeGoPrefab_43, muzzleTransform.position, transform.rotation);
                yield return null;
            }
            if (IsInterrupted)
            {
                _properity.isInterrupted = false;
            }
            _isAbility3Used = false;
            //transform.rotation = originalRotation;
            _properity.isAbilityLocked = false;
        }
    }
}