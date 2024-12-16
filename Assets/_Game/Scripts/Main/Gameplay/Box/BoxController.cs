using UnityEngine;

public class BoxController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private BoxGraphics box;

    public void SetImage(Texture2D image)
    {
        box.SetImage(image);
    }
}