using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class StretchOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector2 expandScale = new Vector2(1, 1);
    [SerializeField] private float duration;
    private Vector2 initialSize;
    private RectTransform rectTrans;

    private float progress = 0;

    private Tween Enter;
    private Tween Exit;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        initialSize = GetComponent<RectTransform>().rect.size;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Enter = DOTween.To(() => progress, x => progress = x, 1, duration);
        Enter.onUpdate =
            () =>
            {
                rectTrans.sizeDelta = new Vector2(
                    initialSize.x * Mathf.Lerp(1f, expandScale.x, progress),
                    initialSize.y * Mathf.Lerp(1f, expandScale.y, progress));
            };
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Exit = DOTween.To(() => progress, x => progress = x, 0, duration);
        Exit.onUpdate =
            () =>
            {
                rectTrans.sizeDelta = new Vector2(
                    initialSize.x * Mathf.Lerp(1f, expandScale.x, progress),
                    initialSize.y * Mathf.Lerp(1f, expandScale.y, progress));
            };
    }

    private void OnDestroy()
    {
        Exit.Kill();
        Enter.Kill();
    }
}