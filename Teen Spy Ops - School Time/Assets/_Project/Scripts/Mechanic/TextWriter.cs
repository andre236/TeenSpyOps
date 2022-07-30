using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{

    private Text _uiText;
    private string _textToWrite;
    private int _characterIndex;
    private float _timePerCharacter;
    private float _timer;

    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter)
    {
        _uiText = uiText;
        _textToWrite = textToWrite;
        _timePerCharacter = timePerCharacter;
        _characterIndex = 0;
    }

    private void Update()
    {
        if (_uiText != null)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer += _timePerCharacter;
                _characterIndex++;
                _uiText.text = _textToWrite.Substring(0, _characterIndex);
            }
        }
    }
}
