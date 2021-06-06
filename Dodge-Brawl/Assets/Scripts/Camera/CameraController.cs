using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// das ist nur dazu da, dass die Kamera die ganze Zeit der Spielerposition in 2D folgt, aber immer den gleichen Abstand zum Bild (also zur Ebene) hat
public class CameraController : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
