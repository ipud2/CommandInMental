using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace NSTank
{
    public class VirtualJoystic : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler
    {
        private Image _bgImage;//background image
        private Image _btImage;//button/joystick image

        private Vector2 _inputVector = Vector2.zero;

        public Vector2 InputVector
        {
            get { return _inputVector; }
        }

        public bool hasInput()
        {
            return _inputVector != Vector2.zero;
        }

        public float vjOffScale = 0.33f;

        private void Start()
        {
            _bgImage = GetComponent<Image>();
            _btImage = transform.GetChild(0).GetComponent<Image>();
            //_bgImage.rectTransform.position = new Vector3(-Screen.width*0.5f*screenOffsetOfVirtualJoystic.x,-Screen.height*0.5f*screenOffsetOfVirtualJoystic.y,0);
            float posx = -Screen.width * vjOffScale;
            float posy = -Screen.height * vjOffScale;
            //_bgImage.rectTransform.position = new Vector3(posx,posy,0);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_bgImage.rectTransform, eventData.position,
                eventData.pressEventCamera, out pos))
            {
                print(pos);
                pos.x = (pos.x / _bgImage.rectTransform.sizeDelta.x);
                pos.y = (pos.y / _bgImage.rectTransform.sizeDelta.y);
                _inputVector = new Vector2(pos.x,pos.y);
                _inputVector.Normalize();
                pos = _inputVector * _bgImage.rectTransform.sizeDelta.x * 0.5f*0.618f;
                _btImage.rectTransform.anchoredPosition = pos;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _btImage.rectTransform.anchoredPosition = Vector2.zero;
            _inputVector = Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }
    }
}