using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PaintableTexture : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] RawImage img;
    [SerializeField] Texture2D texture;
    [SerializeField] int brushWidth = 10;
    private Color[] circle;
    private Color[] blank;
    private Vector3 bottomLeft;
    private Vector3 totalScale = new Vector3(1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = new Vector3(rectTransform.position.x, rectTransform.position.y, 0);
        Transform t = transform;
        while (t != null)
        {
            totalScale = new Vector3(totalScale.x / t.localScale.x, totalScale.y / t.localScale.y, totalScale.z / t.localScale.z);
            t = t.parent;
        }
        Debug.Log(totalScale);
        int squaredWidth = brushWidth * brushWidth;
        circle = new Color[squaredWidth];
        int center = brushWidth / 2;
        for (int i = 0; i < brushWidth; i++)
        {
            for(int j = 0; j < brushWidth; j++)
            {
                circle.SetValue(Color.black, j + i*brushWidth);
            }
        }
        int textureSize = texture.width * texture.height;
        blank = new Color[textureSize];
        for(int i = 0; i < textureSize; i++)
        {
            blank.SetValue(Color.white, i);
        }
        ClearTexture();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Vector3.Scale(Input.mousePosition - bottomLeft, totalScale);
            if (0 <= mousePos.x - brushWidth / 2 && rectTransform.rect.width > mousePos.x + brushWidth / 2
                && 0 <= mousePos.y - brushWidth / 2 && rectTransform.rect.height > mousePos.y + brushWidth / 2)
            {
                texture.SetPixels((int)mousePos.x - brushWidth / 2, (int)mousePos.y - brushWidth / 2, brushWidth, brushWidth, circle);
                texture.Apply();
            }
        }
    }

    void OnDestroy()
    {
        ClearTexture();
    }

    public void ClearTexture()
    {
        texture.SetPixels(blank);
        texture.Apply();
        img.texture = texture;
    }
}
