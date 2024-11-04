using UnityEngine;
using UnityEngine.UI;

public class BackgroundImage : MonoBehaviour
{
    public Sprite backgroundImage;

    void Start()
    {

        GameObject canvasGO = new GameObject("BackgroundCanvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = -1;


        GameObject imageGO = new GameObject("BackgroundImage");
        imageGO.transform.parent = canvasGO.transform;
        Image image = imageGO.AddComponent<Image>();
        image.sprite = backgroundImage;

        RectTransform rectTransform = image.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);
    }
}
