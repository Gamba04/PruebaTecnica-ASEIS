using UnityEngine;

public class BoxGraphics : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private new MeshRenderer renderer;

    private readonly int mainTextureID = Shader.PropertyToID("_MainTex");

    #region Public Methods

    public void SetImage(Texture2D image)
    {
        MaterialPropertyBlock properties = new MaterialPropertyBlock();

        properties.SetTexture(mainTextureID, image);
        renderer.SetPropertyBlock(properties);
    }

    #endregion

}