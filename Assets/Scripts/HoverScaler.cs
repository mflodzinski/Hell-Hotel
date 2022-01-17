using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class HoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float toScaleMultiply;
        [SerializeField] private float duration;
        [SerializeField] private Ease easeType;

        private Vector3 startScale;

        public void Awake()
        {
            startScale = transform.localScale;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(toScaleMultiply * startScale, duration).SetEase(easeType);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(startScale, duration).SetEase(easeType);
        }
    }
}