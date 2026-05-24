namespace AzureFable.Models
{
    internal class Floor : Cell
    {
        public Floor(int x, int y) : base(x, y)
        {
            Name = "Floor";
            ImagePath = "/Assets/Floor.png";
        }

        public override bool CanEnter(Unit? unit)
        {
            return true;
        }
    }
}
