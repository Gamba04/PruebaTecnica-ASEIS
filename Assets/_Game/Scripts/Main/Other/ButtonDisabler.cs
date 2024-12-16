using UnityEngine;

public class ButtonDisabler : MonoBehaviour
{
    [ReadOnly(true), SerializeField]
    private AdvancedButton button;

    [Space]
    [SerializeField]
    private bool startDisabled = true;
    [SerializeField]
    private uint targetGraphic;
    [SerializeField]
    private Color disabledColor = Color.white;

    private Color defaultColor;

    private AdvancedButton.TargetGraphic TargetGraphic => button.GetTargetGraphic((int)targetGraphic);

    private Color ButtonColor
    {
        get => TargetGraphic.disselected.color;
        set => TargetGraphic.disselected.color = value;
    }

    #region Init

    private void Awake()
    {
        InitColor();
        InitDisabled();
    }

    private void InitColor()
    {
        defaultColor = ButtonColor;
    }

    private void InitDisabled()
    {
        SetDisabled(startDisabled);
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Public Methods

    public void SetDisabled(bool disabled)
    {
        button.interactable = !disabled;

        ButtonColor = disabled ? disabledColor : defaultColor;

        button.RefreshState();
    }

    #endregion

    // ----------------------------------------------------------------------------------------------------------------------------

    #region Editor

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (button == null)
        {
            button = GetComponent<AdvancedButton>();
        }
    }

#endif

    #endregion

}