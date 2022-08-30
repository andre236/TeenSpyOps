using UnityEngine;
using Controllers;

public class TestAudio : MonoBehaviour
{

    public AudioController AudioController;

    #region Unity Functions
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            AudioController.PlayAudio(AudioType.ST_01);
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            AudioController.StopAudio(AudioType.ST_01);

        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            AudioController.RestartAudio(AudioType.ST_01);

        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            AudioController.PlayAudio(AudioType.SFX_01);

        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            AudioController.StopAudio(AudioType.SFX_01);

        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            AudioController.RestartAudio(AudioType.SFX_01);

        }


    }

#endif
    #endregion
}
