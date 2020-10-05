using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTextColor : MonoBehaviour
{
    [SerializeField] private Color _selected;
    [SerializeField] private Color _deselected;
    private TextMeshProUGUI _text;

    private void Awake() 
    {
        _text =GetComponent<TextMeshProUGUI>();
    }

    public void SetSelected()
    {
        _text.color = _selected;
    }

    public void SetDeselected()
    {
        _text.color = _deselected;
    }
}
