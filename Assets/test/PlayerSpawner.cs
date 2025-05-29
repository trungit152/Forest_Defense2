using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (Runner.IsServer) // Host/server sẽ là người xử lý spawn cho mọi player
        {
            Debug.Log($"Spawning player for: {player}");
            Runner.Spawn(PlayerPrefab, GetSpawnPosition(player), Quaternion.identity, player);
        }
    }

    // Tùy chọn: Hàm lấy vị trí spawn cho từng player
    private Vector3 GetSpawnPosition(PlayerRef player)
    {
        // Ví dụ: spawn cách nhau theo index
        int index = player.RawEncoded;
        return new Vector3(index * 2f, 0, 0);
    }
}