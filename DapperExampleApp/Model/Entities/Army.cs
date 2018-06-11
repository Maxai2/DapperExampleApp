using System.Collections.Generic;
using System.Text;

namespace DapperExampleApp
{
    public class Army
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Tagline { get; set; }
        public ICollection<Unit> Units { get; set; }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            res.Append($"[{Id}] {Title} <{Tagline}>: \n");
            foreach (var unit in Units)
            {
                res.Append($"\t{unit}\n");
            }
            return res.ToString();
        }
    }
}
