using System.Collections.Generic;

namespace Client.Infrastructure.AssetManagement
{
    public static class AssetPath
    {
        public const string HudPath = "Hud/Hud";
        public const string UpgradeItem = "Hud/UpgradeItem";
        public const string SoonItem = "Hud/SoonItem";

        public const string Restaraunt = "Prefabs/Location/Restaraunt";

        public const string ChefTable = "Prefabs/Location/ChefTable";
        public const string Chef = "Prefabs/ChefCharacter";

        public const string StorageCrate = "Prefabs/Location/LoaderWorkPlace";
        public const string Loader = "Prefabs/LoaderCharacter";

        public const string OrderPlace = "Prefabs/Location/CashierTable";
        public const string Cashier = "Prefabs/CashierCharacter";

        public const string NavMeshSurface = "Prefabs/Infrastructure/NavMesh";

        public static readonly List<string> Customers = new List<string>()
        {
            "Prefabs/Customers/Customer1",
            "Prefabs/Customers/Customer2",
            "Prefabs/Customers/Customer3",
            "Prefabs/Customers/Customer4",
            "Prefabs/Customers/Customer5",
            "Prefabs/Customers/Customer6",
            "Prefabs/Customers/Customer7",
            "Prefabs/Customers/Customer8",
        };


    }
}