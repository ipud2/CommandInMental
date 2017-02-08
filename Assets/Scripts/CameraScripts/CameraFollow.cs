using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSTank
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform targetTransform;

        private Vector3 offset;

        // Use this for initialization
        void Start()
        {
            offset = transform.position + targetTransform.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = targetTransform.position + offset;
        }
    }
}