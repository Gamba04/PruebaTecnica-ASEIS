using System;
using UnityEngine;

public class BoxGraphics : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private new MeshRenderer renderer;
    [SerializeField]
    private Animator anim;

    public event Action onComplete;

    private readonly int mainTextureID = Shader.PropertyToID("_MainTex");
    private readonly int revealID = Animator.StringToHash("Reveal");

    #region Public Methods

    public void SetImage(Texture2D image)
    {
        MaterialPropertyBlock properties = new MaterialPropertyBlock();

        properties.SetTexture(mainTextureID, image);
        renderer.SetPropertyBlock(properties);
    }

    public void Reveal()
    {
        anim.SetTrigger(revealID);
    }

    public void OnAnimComplete()
    {
        onComplete?.Invoke();
    }

    #endregion

}