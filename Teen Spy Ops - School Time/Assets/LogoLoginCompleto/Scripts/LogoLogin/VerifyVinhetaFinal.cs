using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyVinhetaFinal : MonoBehaviour
{
    private bool logoFinalizou = false;
    public XuxaApiController xuxaApi;

    void Update()
    {
        if (!logoFinalizou)
        {
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FIM"))
            {
                xuxaApi.GetToken();
                logoFinalizou = true;
                Destroy(gameObject);
            }
        }
    }
}
