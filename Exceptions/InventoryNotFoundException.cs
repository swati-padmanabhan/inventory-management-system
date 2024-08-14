namespace InventoryManagement.Exceptions
{
    internal class InventoryNotFoundException : Exception
    {
        public InventoryNotFoundException(string message) : base(message) { }

    }
}
