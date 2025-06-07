using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public static class RankingFeature
{
    private const string leaderboardId = "123";

    public static async Task InitServices()
    {
        await UnityServices.InitializeAsync();
    }
    
    public static async Task SignInAnonymously()
    {
        try
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.Log("Not signed in, signing in...");
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
            
                // Chỉ submit score cho user thực sự mới
                bool isNewUser = await IsNewUser();
                if (isNewUser)
                {
                    Debug.Log("New user detected, submitting initial score...");
                    SubmitScore(SaveGame.SaveTalentPoint.Coin); // Thêm await
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Sign in failed: " + e.Message);
        }
    }
    
    public static async Task<LeaderboardScoresPage> CreateRankBoard()
    {
        var scores = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId, new GetScoresOptions { Limit = 1000 });

        return scores;
        
    }
    
    public static void SubmitScore(int score)
    {
        try
        {
            LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
            Debug.Log("Score submitted!");
        }
        catch (Exception e)
        {
            Debug.LogError("Submit failed: " + e.Message);
        }
    }

    public static async Task<bool> IsNewUser()
    {
        try
        {
            var currentPlayerId = AuthenticationService.Instance.PlayerId;
            var scores =
                await LeaderboardsService.Instance.GetScoresAsync(leaderboardId, new GetScoresOptions { Limit = 1000 });

            foreach (var entry in scores.Results)
            {
                if (entry.PlayerId == currentPlayerId)
                {
                    Debug.Log("User found in leaderboard, not a new user");
                    return false;
                }
            }

            Debug.Log("User not found in leaderboard, this is a new user");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to check if user is new: " + e.Message);
            return true;
        }
    }
}
