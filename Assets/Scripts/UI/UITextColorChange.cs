using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextColorChange : MonoBehaviour
{
    [SerializeField] private Color32[] _colors;
    [SerializeField] private float _frameTime;
    private TextMeshProUGUI _text;
    private WaitForSeconds _wait;
    private bool wasDisabled;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        if (!_text) gameObject.AddComponent<TextMeshProUGUI>();

        _wait = new WaitForSeconds(_frameTime);
        _text.enableWordWrapping = true;
    }

    private void Start()
    {
            StartCoroutine(AnimateColor());
    }

    private void OnDisable()
    {
        wasDisabled = true;
    }

    private void Update() 
    {
        if (gameObject.activeInHierarchy && wasDisabled)
        {
            StartCoroutine(AnimateColor());
            wasDisabled = false;
        }
    }

    private IEnumerator AnimateColor()
    {
        _text.ForceMeshUpdate();

        int currentCharacter = 0;
        int currentColor = 0;

        Color32[] newColors = _text.mesh.colors32;
        Color32 c0 = _text.color;

        Color32 c1 = c0;

        while (true)
        {
            int characterCount = _text.textInfo.characterCount;

            // If No Characters then just yield and wait for some text to be added
            if (characterCount == 0)
            {
                yield return new WaitForSeconds(0.25f);
                continue;
            }

            // Get the index of the material used by the current character.
            int materialIndex = _text.textInfo.characterInfo[currentCharacter].materialReferenceIndex;

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            newColors = _text.textInfo.meshInfo[materialIndex].colors32;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = _text.textInfo.characterInfo[currentCharacter].vertexIndex;

            // Only change the vertex color if the text element is visible.
            if (_text.textInfo.characterInfo[currentCharacter].isVisible)
            {
                c0 = _colors[currentColor];

                newColors[vertexIndex + 0] = c0;
                newColors[vertexIndex + 1] = c0;
                newColors[vertexIndex + 2] = c0;
                newColors[vertexIndex + 3] = c0;

                // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
                _text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                // This last process could be done to only update the vertex data that has changed as opposed to all of the vertex data but it would require extra steps and knowing what type of renderer is used.
                // These extra steps would be a performance optimization but it is unlikely that such optimization will be necessary.
            }

            currentCharacter = (currentCharacter + 1) % characterCount;
            currentColor = (currentColor + 1) % _colors.Length;
            yield return _wait;
        }
    }
}