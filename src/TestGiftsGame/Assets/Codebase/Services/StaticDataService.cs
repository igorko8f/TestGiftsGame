﻿using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Craft;
using Codebase.Level;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Services
{
    public class StaticDataService : IStaticDataService, IDisposable
    {
        private readonly IProjectResourcesProvider _resourcesProvider;
        private readonly bool _initialized = false;

        public BoxCraftingRecipes CraftingRecipes => _craftingRecipes;
        public PriceList PriceList => _priceList;
        public CraftingSlot[] CraftingSlots => _craftingSlots;
        public int TotalLevelsCount => _totalLevelsCount;

        private List<LevelConfiguration> _levelConfigurations = new();
        private BoxCraftingRecipes _craftingRecipes;
        private PriceList _priceList;
        private CraftingSlot[] _craftingSlots;
        private int _totalLevelsCount;

        public StaticDataService(IProjectResourcesProvider resourcesProvider)
        {
            _resourcesProvider = resourcesProvider;
            _initialized = CollectData();

            if (_initialized == false)
            {
                throw new NullReferenceException("Resources wasn't load!");
            }
        }

        public LevelConfiguration GetConfigForLevel(int level)
        {
            if (_initialized == false) return null;
                
            var levelConfig = _levelConfigurations
                .FirstOrDefault(x => x.LevelNumber == level);

            if (levelConfig is not null) return levelConfig;
            
            Debug.LogError($"There is no config for level with index {level}!");
            return null;
        }
            
        private bool CollectData()
        {
            _levelConfigurations = _resourcesProvider
                .LoadResources<LevelConfiguration>()
                .ToList();
            
            if (_levelConfigurations.Any() == false) return false;

            _totalLevelsCount = _levelConfigurations.Count - 1;
            
            _craftingRecipes = _resourcesProvider
                .LoadResources<BoxCraftingRecipes>()
                .FirstOrDefault();

            _priceList = _resourcesProvider
                .LoadResources<PriceList>()
                .FirstOrDefault();

            _craftingSlots = _resourcesProvider
                .LoadResources<CraftingSlot>()
                .ToArray();

            return _craftingRecipes is not null;
        }

        public void Dispose()
        {
           _levelConfigurations.Clear();
           _craftingRecipes = null;
        }
    }
}