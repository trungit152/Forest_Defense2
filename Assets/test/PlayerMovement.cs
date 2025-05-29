using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed = 5f;

    public override void FixedUpdateNetwork()
    {
        // Chỉ thực hiện di chuyển nếu client này có quyền điều khiển
        if (!HasInputAuthority)
        {
            return;
        }
        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Runner.DeltaTime* speed;
        
        
        if(move != Vector3.zero)
        {
            transform.position += move;
        }
    }
}