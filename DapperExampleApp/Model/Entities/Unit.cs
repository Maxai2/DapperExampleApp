namespace DapperExampleApp
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public Weapon Weapon { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name}, {Health} ({Weapon})";
        }
    }
}
