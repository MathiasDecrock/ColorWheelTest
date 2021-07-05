using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerExampleScript : MonoBehaviour
{
    private static string name;
    private static Renderer r;
    void Update()
    {
        name = gameObject.name;
        r = GetComponent<Renderer>();
        r.sharedMaterial = r.material;
    }
    public static void ChooseColorButtonClick()
    {
        
        ColorPicker.Create(r.sharedMaterial.color, $"Choose the {name}'s color!", SetColor, ColorFinished, true);
    }
    private static void SetColor(Color currentColor)
    {
        r.sharedMaterial.color = currentColor;
    }

    private static void ColorFinished(Color finishedColor)
    {
        Debug.Log("You chose the color " + ColorUtility.ToHtmlStringRGBA(finishedColor));
    }
}
