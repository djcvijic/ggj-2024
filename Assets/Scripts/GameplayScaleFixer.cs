using System.Collections;
using UnityEngine;

public class GameplayScaleFixer : MonoBehaviour
{
    private const float ScaleBuffer = 0.01f;

    [SerializeField] private Vector2Int referenceResolution = new(1080, 1920);

    private Vector3 _defaultScale;

    private void Start()
    {
        _defaultScale = transform.localScale;
        FixScale();
        StartCoroutine(FixScaleCoroutine());
    }

    private IEnumerator FixScaleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            FixScale();
        }
    }

    private void FixScale()
    {
        var widthScale = (float)referenceResolution.x / Screen.width;
        var heightScale = (float)referenceResolution.y / Screen.height;
        var localScale = Mathf.Min(widthScale, heightScale) * (1 + ScaleBuffer);
        transform.localScale = localScale * _defaultScale;
    }
}
