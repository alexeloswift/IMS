namespace MyWinFormsApp
{
    public class Inhouse(int partID, string name, decimal price, int inStock, int min, int max, int machineID) : Part(partID, name, inStock, price, min, max)
    {
        public int MachineID { get; set; } = machineID;
    }
}