using UnityEngine;

[ExecuteInEditMode]
public class SizeFitter : MonoBehaviour
{
    private void OnEnable()
    {
        CheckForChanges();
        enabled = false;
    }

    private void CheckForChanges()
    {
        var children = transform.GetComponentInChildren<RectTransform>();

        float maxX;
        float maxY;
        var minX = maxX = transform.localPosition.x;
        var minY = maxY = transform.localPosition.y;

        foreach (RectTransform child in children)
        {
            var scale = child.sizeDelta;

            var tempMinX = child.localPosition.x - scale.x / 2;
            var tempMaxX = child.localPosition.x + scale.x / 2;
            var tempMinY = child.localPosition.y - scale.y / 2;
            var tempMaxY = child.localPosition.y + scale.y / 2;

            if (tempMinX < minX)
                minX = tempMinX;
            if (tempMaxX > maxX)
                maxX = tempMaxX;

            if (tempMinY < minY)
                minY = tempMinY;
            if (tempMaxY > maxY)
                maxY = tempMaxY;
        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(maxX - minX, maxY - minY);
    }
}