﻿using System;
using Codebase.Services;

namespace Codebase.StaticData
{
    public static class ResourceNames
    {
        //Declare resources 
        private static ResourceName[] resources =
        {
        };

        public static string GetLocation<TResource>() where TResource : IResource
        {
            var location = string.Empty;
            foreach (var resource in resources)
            {
                if (resource.Type == typeof(TResource))
                    location = resource.Location;
            }

            if (string.IsNullOrEmpty(location))
                throw new NullReferenceException($"The is no path for resource with type {typeof(TResource)}.");

            return location;
        }
    }
}