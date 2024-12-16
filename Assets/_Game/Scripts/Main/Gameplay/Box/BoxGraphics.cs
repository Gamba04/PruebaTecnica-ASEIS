using System;
using UnityEngine;

public class BoxGraphics : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private new MeshRenderer renderer;
    [SerializeField]
    private Animator anim;

    [Header("Settings")]
    [SerializeField]
    [Range(0, 1)]
    private float targetFactor = 1;
    [SerializeField]
    private Transition textureTransition;

    public event Action onComplete;

    private MaterialPropertyBlock properties;

    private readonly int textureID = Shader.PropertyToID("_OverrideTexture");
    private readonly int factorID = Shader.PropertyToID("_Factor");

    private readonly int revealID = Animator.StringToHash("Reveal");

    #region Init

    public void Init()
    {
        InitProperties();
    }

    private void InitProperties()
    {
        properties = new MaterialPropertyBlock();

        OnTextureTransition(0);
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Update

    private void Update()
    {
        textureTransition.UpdateTransition(OnTextureTransition);
    }

    private void OnTextureTransition(float value)
    {
        properties.SetFloat(factorID, value);

        ApplyProperties();
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Public Methods

    public void SetImage(Texture2D image)
    {
        properties.SetTexture(textureID, image);

        ApplyProperties();
    }

    public void Reveal()
    {
        anim.SetTrigger(revealID);

        textureTransition.StartTransition(targetFactor);
    }

    public void OnAnimComplete()
    {
        onComplete?.Invoke();
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Other

    private void ApplyProperties() => renderer.SetPropertyBlock(properties);

    #endregion

}