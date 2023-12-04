using Manager;
using UnityEngine;
using UnityEngine.UI;

public class FollowCursorUI : MonoBehaviour
{
    private Image _cursorEffectImage;
    private GameManager _gameManager;
   

    private void Awake()
    {
        _cursorEffectImage = GetComponent<Image>();
        _gameManager = FindObjectOfType<GameManager>();   
    }

    private void Update()
    {
        if (_gameManager.CurrentSkill == SkillState.XRay)
            _cursorEffectImage.enabled = true;
        else
        {
            _cursorEffectImage.enabled = false;
            return;
        }

        if (_gameManager.CurrentDistance == XRayDistance.Third)
            transform.localScale = new Vector2(2.98f, 2.98f);
        else if(_gameManager.CurrentDistance == XRayDistance.Second)
            transform.localScale = new Vector2(2f, 2f);
        else
            transform.localScale = new Vector2(1f, 1f);


        transform.position = Input.mousePosition;


    }

}
