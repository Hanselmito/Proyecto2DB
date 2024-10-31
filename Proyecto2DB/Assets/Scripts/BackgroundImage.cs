using UnityEngine;
using UnityEngine.UI;

public class BackgroundImage : MonoBehaviour
{
    public Sprite backgroundImage; // Asigna esta imagen desde el inspector

    void Start()
    {
        // Crear un nuevo Canvas
        GameObject canvasGO = new GameObject("BackgroundCanvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = -1; // Asegurarse de que está detrás de todo

        // Añadir un componente Image al Canvas
        GameObject imageGO = new GameObject("BackgroundImage");
        imageGO.transform.parent = canvasGO.transform;
        Image image = imageGO.AddComponent<Image>();
        image.sprite = backgroundImage;

        // Ajustar el tamaño del Image para que cubra toda la pantalla
        RectTransform rectTransform = image.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);
    }
}
