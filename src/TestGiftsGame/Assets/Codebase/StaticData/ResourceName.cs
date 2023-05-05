using System;

namespace Codebase.StaticData
{
    public class ResourceName
    {
        public Type Type;
        public string Location;

        public ResourceName(Type type, string location)
        {
            Type = type;
            Location = location;
        }
    }
}