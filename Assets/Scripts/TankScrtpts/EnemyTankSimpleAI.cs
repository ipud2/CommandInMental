using System.Collections;
using System.Collections.Generic;
using NSTank;
using UnityEngine;

namespace NSTank
{
    public class EnemyTankSimpleAI : MonoBehaviour
    {
        public float speed = 20f;
        private Tank_Green tg;
        private WaitForSeconds wait;
        private float thinkTime =3.0f;
        private Rigidbody rb;
        private float eyeDistance = 1.0f;
        private void Start()
        {
            tg = GetComponent<Tank_Green>();
            rb = GetComponent<Rigidbody>();
            StartCoroutine(Think());
            wait = new WaitForSeconds(thinkTime);
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            rb.MovePosition(transform.position+transform.forward * speed * Time.deltaTime);
            //turn around
            Ray ray = new Ray(transform.position,transform.forward);
            if (Physics.Raycast(ray,eyeDistance))
            {
                print("eye");
                transform.Rotate(Vector3.up,Random.Range(180.0f,360.0f));
            }
        }

        //all about the probability
        private IEnumerator Think()
        {
            while (gameObject.active)
            {
                //rotate
                transform.Rotate(Vector3.up,Random.Range(0,360));
                if (Random.value < 0.5)
                {
                    tg.Fire();
                }
                else
                {
                    tg.UseAbility1();
                    tg.UseAbility2();
                }
                yield return wait;
            }
        }
    }
}