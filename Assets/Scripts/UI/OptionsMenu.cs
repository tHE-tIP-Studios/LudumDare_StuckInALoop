using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _options;
    private ComeIn _animation;
    private Selectable[] _mainMenuSelectables;
    private Selectable[] _optionsSelectables;

    private void Awake() 
    {
        _mainMenuSelectables = _mainMenu.GetComponentsInChildren<Selectable>();
        _optionsSelectables = _options.GetComponentsInChildren<Selectable>();
        _animation = GetComponent<ComeIn>();
        _animation.onOpen.AddListener(DisableMenu);
        _animation.onClose.AddListener(EnableMainMenu);
    }

    private void Start() 
    {
        EnableMainMenu();
    }
    private void DisableMenu()
    {
        foreach(Selectable s in _mainMenuSelectables)
        {
            s.interactable = false;
        }

        foreach(Selectable s in _optionsSelectables)
        {
            s.interactable = true;
        }
    }

    private void EnableMainMenu()
    {
        foreach(Selectable s in _mainMenuSelectables)
        {
            s.interactable = true;
        }

        foreach(Selectable s in _optionsSelectables)
        {
            s.interactable = false;
        }
    }
}
