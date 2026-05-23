namespace AzureFable.Models
{
    internal class Wall : Cell
    {
        public Wall(int x, int y) : base(x, y)
        {
            ImagePath = "/Assets/Wall.png";
        }

        public override bool CanEnter(Unit? unit)
        {
            return false;
        }
    }
}
