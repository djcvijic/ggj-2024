using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    [SerializeField] private int rowNumber;
    [SerializeField] private int seatNumber;

    public Vector3 GetPosition
    {
        get { return transform.position; }
    }

    public int GetRowNumber
    {
        get { return rowNumber; }
    }

    public int GetSeatNumber
    {
        get { return seatNumber; }
    }
}
