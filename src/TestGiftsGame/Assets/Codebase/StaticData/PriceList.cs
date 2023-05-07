using System.Collections.Generic;
using System.Linq;
using Codebase.Services;
using UnityEngine;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "PriceList", menuName = "StaticData/Prices/PriceList")]
    public class PriceList : ScriptableObject, IResource
    {
        public List<PriceByOrders> Prices;

        public int GetRewardByOrderCount(int completedOrders)
        {
            if (Prices.Any() == false) return 0;
            
            var matchedPrice = Prices.FirstOrDefault(x => x.OrdersCount == completedOrders);
            if (matchedPrice is null) return 0;

            return matchedPrice.Price;
        }
    }
}