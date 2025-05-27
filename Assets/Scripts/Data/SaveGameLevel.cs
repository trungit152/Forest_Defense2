using UnityEngine;

public class SaveGameLevel
{
    const string CURRENT_LEVEL_KEY = "CURRENT_LEVEL";
    const string MAX_LEVEL_KEY = "MAX_LEVEL";
    const string LEVEL_UNLOCKED_KEY = "LEVEL_UNLOCKED";

    public int currentLevel;
    public int maxLevel;
    public int levelUnlocked;

    public SaveGameLevel()
    {
        if (ES3.KeyExists(CURRENT_LEVEL_KEY))
            currentLevel = ES3.Load<int>(CURRENT_LEVEL_KEY);
        else
        {
            currentLevel = 1;
        }

        if (ES3.KeyExists(MAX_LEVEL_KEY))
            maxLevel = ES3.Load<int>(MAX_LEVEL_KEY);
        else
        {
            maxLevel = DataWave.GetLevelCount();
        }

        if (ES3.KeyExists(LEVEL_UNLOCKED_KEY))
            levelUnlocked = ES3.Load<int>(LEVEL_UNLOCKED_KEY);
        else
        {
            levelUnlocked = maxLevel;
        }
    }

    public void SaveGameLevels()
    {
        ES3.Save(CURRENT_LEVEL_KEY, currentLevel);
        ES3.Save(MAX_LEVEL_KEY, maxLevel);
        ES3.Save(LEVEL_UNLOCKED_KEY, levelUnlocked);
    }

    public void LoadGameLevels()
    {
        if (ES3.KeyExists(CURRENT_LEVEL_KEY))
            currentLevel = ES3.Load<int>(CURRENT_LEVEL_KEY);

        if (ES3.KeyExists(MAX_LEVEL_KEY))
            maxLevel = ES3.Load<int>(MAX_LEVEL_KEY);

        if (ES3.KeyExists(LEVEL_UNLOCKED_KEY))
            levelUnlocked = ES3.Load<int>(LEVEL_UNLOCKED_KEY);
    }
}
