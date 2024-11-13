using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPiece : MonoBehaviour
{

    [SerializeField]SlidingPuzzle puzzle;

    private void OnMouseDown() {
        puzzle.SwapPiece(this.transform);
    }
}
