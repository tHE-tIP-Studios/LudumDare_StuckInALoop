using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _credits;
    [SerializeField] private AudioClip _music;
    private ComeIn _animation;
    private Selectable[] _mainMenuSelectables;
    private Selectable[] _optionsSelectables;
    private Selectable[] _creditsSelectables;
    private MusicManager _musicManager;

    private void Awake() 
    {
        _mainMenuSelectables = _mainMenu.GetComponentsInChildren<Selectable>();
        _optionsSelectables = _options.GetComponentsInChildren<Selectable>();
        _creditsSelectables = _credits.GetComponentsInChildren<Selectable>();
        _animation = GetComponent<ComeIn>();
        _animation.onOpen.AddListener(DisableMenu);
        _animation.onClose.AddListener(EnableMainMenu);
        _musicManager = FindObjectOfType<MusicManager>();
    }

    private void Start() 
    {
        EnableMainMenu();
        _musicManager.CurrentMusic = _music;
        _musicManager.Play(1f);
        
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

    public void EnableCredits()
    {
        foreach(Selectable s in _optionsSelectables)
        {
            s.interactable = false;
        }
        
        foreach(Selectable s in _creditsSelectables)
        {
            s.interactable = true;
        }
    }

    public void DisableCredits()
    {

        foreach(Selectable s in _optionsSelectables)
        {
            s.interactable = true;
        }

        foreach(Selectable s in _creditsSelectables)
        {
            s.interactable = false;
        }
    }
}
