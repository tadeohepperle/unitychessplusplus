using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceRepresentation : MonoBehaviour
{

    public ChessPiece chessPiece;
    // Start is called before the first frame update

    private void Start()
    {

    }

    public void OnClicked()
    {
        if (moveup == 0)
        {
            StartCoroutine(Jump());
        }
    }

    public void OnHover()
    {

        if (tilt == 0)
        {
            tilt = 1;
            StartCoroutine(Tilt());
        }
    }


    void Update()
    {
        if (moveup != 0)
        {
            this.transform.position = this.transform.position + Vector3.up * moveup * Time.deltaTime;
        }
        if (tilt != 0)
        {
            this.transform.Rotate(Vector3.up * tilt * Time.deltaTime * 10);
        }
    }

    int moveup = 0;
    int tilt = 0;

    IEnumerator Jump()
    {
        moveup = 1;
        yield return new WaitForSeconds(0.3f);
        moveup = -1;
        yield return new WaitForSeconds(0.3f);
        moveup = 0;


        yield return null;
    }

    IEnumerator Tilt()
    {
        tilt = 1;
        yield return new WaitForSeconds(0.7f);
        tilt = -1;
        yield return new WaitForSeconds(0.7f);
        tilt = 0;


        yield return null;
    }


}
