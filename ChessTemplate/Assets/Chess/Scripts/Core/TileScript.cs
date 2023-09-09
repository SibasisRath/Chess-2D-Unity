using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] Vector2 size;
    [SerializeField] float rotation;
    [SerializeField] LayerMask targetLayer;

    private bool _isEmpty;
    private GameObject piece;

    public bool IsEmpty { get => _isEmpty; private set => _isEmpty = value; }
    public GameObject Piece { get => piece; private set => piece = value; }

    public void Update()
    {
        GetSituatedPieceInfo();
    }
    public void GetSituatedPieceInfo()
    {
        Vector2 boxPosition = transform.position;
        Collider2D collider = Physics2D.OverlapBox(boxPosition, size, rotation, targetLayer);

        if (collider != null)
        {
            IsEmpty = false;
            Piece = collider.gameObject;
        }
        else
        {
            IsEmpty = true;
            Piece = null; // No piece found, so set it to null.
        }

        //Debug.Log(Piece);
    }
}