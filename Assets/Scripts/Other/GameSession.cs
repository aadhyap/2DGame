using System.Collections.Generic;
using UnityEngine;

public static class GameSession
{
    public static Vector3? checkpointPosition = null;
    public static HashSet<string> befriendedEnemyIds = new HashSet<string>();

    public static void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }

    public static void MarkEnemyAsFriend(string enemyId)
    {
        if (!string.IsNullOrEmpty(enemyId))
        {
            befriendedEnemyIds.Add(enemyId);
        }
    }

    public static bool IsEnemyFriend(string enemyId)
    {
        if (string.IsNullOrEmpty(enemyId))
            return false;

        return befriendedEnemyIds.Contains(enemyId);
    }

    public static void ResetProgress()
    {
        checkpointPosition = null;
        befriendedEnemyIds.Clear();
    }
}