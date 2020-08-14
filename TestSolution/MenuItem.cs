using System;
using System.Collections.Generic;
using System.Text;

namespace TestSolution
{
    public class MenuItem
    {
        public MenuItem()
        {
            childItems = new List<MenuItem>();
        }
        public int ID { get; set; }
        public string menuName { get; set; }
        public int parentId { get; set; }
        public bool isHidden { get; set; }
        public string linkURL { get; set; }

        public List<MenuItem> childItems { get; set; }
    }
}
