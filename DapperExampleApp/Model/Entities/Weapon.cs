namespace DapperExampleApp
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public WeaponType Type { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name}, {Damage} ({Type})";
        }
    }
}
