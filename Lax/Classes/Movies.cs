using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lax.Classes
{
    public class Movie : IComparable
    {
        public int Id { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public int Runtime { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public string ReleaseDate { get; set; }

        public int CompareTo(object obj)
        {
            return Title.CompareTo((obj as Movie).Title);
        }
    }
}
