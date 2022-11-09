using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SwitchButtonText : MonoBehaviour
{
    [SerializeField] private SwitchCameraPosition _switchCamera;

    private void Start()
    {
        _switchCamera.CameraSwitchPositionEvent += OnCameraSwitch;
    }

    private void OnCameraSwitch(string buttonText)
    {
        TMP_Text textObject = GetComponent<Button>().GetComponentInChildren<TMP_Text>();

        if (buttonText != null) textObject.text = buttonText;
    }
}
