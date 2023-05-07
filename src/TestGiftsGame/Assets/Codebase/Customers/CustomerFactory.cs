using System.Collections.Generic;
using System.Linq;
using Codebase.Craft;
using Codebase.Customers.Orders;
using Codebase.Gifts;
using Codebase.Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase.Customers
{
    public class CustomerFactory : ICustomerFactory
    {
        private readonly BoxCraftingRecipes _craftingRecipes;
        private readonly LevelConfiguration _levelConfiguration;
        private readonly CustomerView[] _customerViews;
        private readonly CustomerSpawnPoint[] _spawnPoints;

        public CustomerFactory(
            BoxCraftingRecipes craftingRecipes,
            LevelConfiguration levelConfiguration,
            CustomerView[] customerViews,
            CustomerSpawnPoint[] spawnPoints)
        {
            _craftingRecipes = craftingRecipes;
            _levelConfiguration = levelConfiguration;
            _customerViews = customerViews;
            _spawnPoints = spawnPoints;
        }

        public CustomerPresenter CreateCustomer()
        {
            var giftsCount = Random.Range(1, _levelConfiguration.Complexity + 1);

            var spawnPoint = GetEmptySpawnPoint();
            if (spawnPoint is null) return null;
            
            var customerView = GenerateCustomerView(spawnPoint);
            if (customerView is null) return null;
            
            var customerOrder = new Order(GenerateCustomerOrder(giftsCount));
            if (customerOrder.GiftsInOrder is null)
            {
                Debug.LogError("Cannot create order, please check Box Recipes! and Level Configuration!");
                return null;
            }

            var customer = new Customer(customerOrder);
            return new CustomerPresenter(customerView, customer, spawnPoint);
        }

        private CustomerSpawnPoint GetEmptySpawnPoint()
        {
            return _spawnPoints.FirstOrDefault(x => x.IsEmpty);
        }

        private CustomerView GenerateCustomerView(CustomerSpawnPoint spawnPoint)
        {
            var selectedView = _customerViews[Random.Range(0, _customerViews.Length)];
            
            spawnPoint.SetEmptyState(false);
            return Object.Instantiate(selectedView, spawnPoint.transform);
        }

        private List<Gift> GenerateCustomerOrder(int giftsCount)
        {
            var gifts = new List<Gift>();
            for (int i = 0; i < giftsCount; i++)
            {
                var gift = GenerateCustomerOrder();
                if (gift is null) continue;
                
                gifts.Add(gift);
            }

            return gifts.Any() ? gifts : null;
        }
        
        private Gift GenerateCustomerOrder()
        {
            var box = _levelConfiguration.AvailableBoxes[Random.Range(0, _levelConfiguration.AvailableBoxes.Length)];
            var availableRecipe = GetAvailableGiftPartsForBox(box);

            if (availableRecipe is null) return null;
            
            var gift = new Gift();
            foreach (var part in availableRecipe.GiftParts)
                gift.ApplyGiftPart(part);

            return gift;
        }

        private CraftingRecipe GetAvailableGiftPartsForBox(Box box)
        {
            var availableParts = _levelConfiguration.AvailableBoxes
                .Select(x => x as GiftPart)
                .Concat(_levelConfiguration.AvailableBows)
                .Concat(_levelConfiguration.AvailableDesigns)
                .ToArray();
            
            var availableRecipes = _craftingRecipes.GetAvailableRecipes(box, availableParts);
            return availableRecipes.Any() ? availableRecipes[Random.Range(0, availableRecipes.Count)] : null;
        }
    }
}