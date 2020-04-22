using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public sealed class ComboboxUI : MonoBehaviour, IControlOptions
{

    private Text _text;
    private Dropdown _dropdown;

    public List<Dropdown.OptionData> GetOptions
    {
        get
        {
            if (!_dropdown)
            {
                _dropdown = transform.GetComponentInChildren<Dropdown>();
            }
            return _dropdown.options;
        }
    }
    public Dropdown GetControl
    {
        get
        {
            if (!_dropdown)
            {
                _dropdown = transform.GetComponentInChildren<Dropdown>();
            }
            return _dropdown;
        }
    }
    public void SetInteractable(bool value)
    {
        GetControl.interactable = value;
    }
    public GameObject Instance => gameObject;
    public Selectable Control => GetControl;
}
