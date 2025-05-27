using TMPro;
using UnityEngine;

public class PlayTestInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField numberInput;

    private void Start()
    {
        numberInput.contentType = TMP_InputField.ContentType.IntegerNumber;
        numberInput.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string value)
    {
        if (int.TryParse(value, out int result))
        {
            PlayingLevel.Level = result;
        }
    }
}
