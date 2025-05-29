using Fusion;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        NetworkInputData inputData = new NetworkInputData
        {
            moveDirection = input.normalized
        };

        // // Gửi input vào mỗi frame
        // if (NetworkRunner.Instance != null)
        // {
        //     NetworkRunner.Instance.SetInput(inputData);
        // }
    }
}

public struct NetworkInputData : INetworkInput
{
    public Vector2 moveDirection;
}