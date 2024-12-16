using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageSelector : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Text text;

    private string path;

    public event Action onPathApplied;

    #region Events

    public void OnBtnOpen()
    {
        string path = OpenFile();

        if (path != null) ApplyPath(path);
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Public Methods

    public Texture2D GetImage()
    {
        if (File.Exists(path))
        {
            byte[] data = File.ReadAllBytes(path);

            Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);

            texture.LoadImage(data);

            return texture;
        }

        return null;
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Other

    private string OpenFile()
    {
        string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        return WindowsFileDialogs.OpenFile("Select Image", initialDirectory, new WindowsFileDialogs.Filter("Image Files", "*.jpg", "*.jpeg", "*.png"));
    }

    private void ApplyPath(string path)
    {
        this.path = path;

        text.text = path;

        onPathApplied?.Invoke();
    }

    #endregion

}