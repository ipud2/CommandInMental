using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NSTank
{

    public class TestRect : MonoBehaviour
    {
        public Vector2 sizeDelta;

        private Image image;

        private void Start()
        {
            image = GetComponent<Image>();
            sizeDelta = image.rectTransform.sizeDelta;
        }

        /// <sizeDelta>
        /// If the anchors are together, sizeDelta is the same as size.
        /// If the anchors are in each of the four corners of the parent,
        /// the sizeDelta is how much bigger or smaller the rectangle is compared to its parent.
        /// </sizeDelta>

        private void Update()
        {
            image.rectTransform.sizeDelta = sizeDelta;
        }
    }
}