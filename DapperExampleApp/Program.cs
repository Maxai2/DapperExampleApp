namespace DapperExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dp = new Dappository();

            //var wts = dp.GetWeaponTypes();

            //foreach (var item in wts)
            //{
            //    System.Console.WriteLine($"[{item.Id}] {item.Title}");
            //}

            //System.Console.WriteLine(dp.GetWeaponTypeById(1));

            //dp.AddWeaponType(new WeaponType() { Title = "Катюша" });

            //System.Console.WriteLine(dp.GetWeaponTypeById(7));

            //System.Console.WriteLine();
             
            //foreach (var item in dp.GetWeapons())
            //{
            //    System.Console.WriteLine(item); 
            //}

            //System.Console.WriteLine();

            //foreach (var item in dp.GetUnits())
            //{
            //    System.Console.WriteLine(item);
            //}


            System.Console.WriteLine();

            foreach (var item in dp.GetArmies())
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
