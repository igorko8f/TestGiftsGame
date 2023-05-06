using System;
using Codebase.StaticData;

namespace Codebase.Services
{
    public interface ISaveLoadService: IDisposable
    {
        PlayerStateSave LoadPlayerState();
        void SavePlayerState(int resourcesCount, int lastLevelIndex);
    }
}