using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class ResourceParameters
    {
        private const int MaxSize = 9999;
        public int Page { get; set; } = 1;
        private int _size = MaxSize;
        public int Size
        {
            get { return _size; }
            set { _size = (value > MaxSize) ? MaxSize : value; }
        }
       // public string SortName { get; set; }
        public int SortBy { get; set; }
        public string Fields { get; set; }
        internal double GetTotalPages(int count)
        {
            return Math.Ceiling(count / (double)Size);
        }
    }
}
