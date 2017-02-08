using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSTank
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private bool alive = true;
        public float lifeTime = 3f;
        public float impuseForce = 30;
        //private int teamId = 0;
        //private int senderId = 0;
        private Rigidbody rb;
        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(transform.forward*impuseForce,ForceMode.Impulse);
            StartCoroutine(Living());
        }
        IEnumerator Living()
        {
            float alredayLivingTime = 0.0f;
            while (alive)
            {
                alredayLivingTime += Time.deltaTime;
                if (alredayLivingTime >= lifeTime)
                {
                    alive = false;
                }
                yield return null;
            }
            Destroy(gameObject);
        }
        private void OnCollisionEnter(Collision other)
        {
            alive = false;
        }
    }
}