using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform bar;


    void Start()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetColor(string colorHex)
    {
        Color colorRGB = GetColorFromString(colorHex);
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = colorRGB;
    }

    private int HexToDec(string hex)
    {
        int dec = System.Convert.ToInt32(hex, 16);
        return dec;
    }

    private float HexToFloatNormalized(string hex)
    {
        return HexToDec(hex) / 255f;
    }

    private Color GetColorFromString(string hexString)
    {
        float r = HexToFloatNormalized(hexString.Substring(0, 2));
        float g = HexToFloatNormalized(hexString.Substring(2, 2));
        float b = HexToFloatNormalized(hexString.Substring(4, 2));
        return new Color(r, g, b);
    }

    public void MaintainRotation(float angle)
    {
        //transform.Rotate(new Vector3(0, 0, -angle));
        transform.eulerAngles = new Vector3(0, 0, -angle);
    }
}
