using UnityEngine;

public class Pawn : Unit
{
    [SerializeField] private Transform _goldMine;
    [SerializeField] private Transform _townHall;

    private void FixedUpdate()
    {
        ResourseTransfer();
    }

    private void ResourseTransfer()
    {
        if (Mathf.Abs(_goldMine.position.x - transform.position.x) > 0.3f)
            Move(_goldMine.position);
        else
            Move(_townHall.position);
    }
}