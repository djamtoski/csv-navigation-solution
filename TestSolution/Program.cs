using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSolution
{
    class Program
    {
        static List<MenuItem> menuItems = new List<MenuItem>();
        static void Main(string[] args)
        {
            GetData();
            SortData();
            ShowMenu();
        }

        public static void GetData()
        {
            using (TextFieldParser parser = new TextFieldParser(@"c:\test\Navigation.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    MenuItem item = new MenuItem();
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        string[] arrayofStrs = field.Split(';');
                        int ID = 0;
                        if(int.TryParse(arrayofStrs[0], out ID))
                        {
                            item.ID = ID;
                        }
                        else
                        {
                            break;
                        }
                        item.menuName = arrayofStrs[1];
                        item.parentId = arrayofStrs[2] == "NULL" ? 0 : Convert.ToInt32(arrayofStrs[2]);
                        item.isHidden = arrayofStrs[3] == "False" ? false : true;
                        item.linkURL = arrayofStrs[4];
                        menuItems.Add(item);
                    }
                }
            }
        }

        public static void SortData()
        {
            menuItems.Sort((x, y) => string.Compare(x.menuName, y.menuName));
            List<MenuItem> sortedMenu = new List<MenuItem>();
            List<MenuItem> childList = new List<MenuItem>();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (!sortedMenu.Contains(menuItems[i]))
                {
                    if (menuItems[i].parentId == 0)
                    {
                        sortedMenu.Add(menuItems[i]);
                    }
                    else
                    {
                        childList.Add(menuItems[i]);
                    }
                }
            }
            for (int i = 0; i < sortedMenu.Count; i++)
            {
                for (int j = 0; j < childList.Count; j++)
                {
                    if(childList[j].parentId == sortedMenu[i].ID)
                    {
                        if (!sortedMenu[i].childItems.Contains(childList[j]))
                            sortedMenu[i].childItems.Add(childList[j]);
                    }
                    else
                    {
                        foreach(MenuItem child in sortedMenu[i].childItems)
                        {
                            if(child.ID == childList[j].parentId)
                            {
                                child.childItems.Add(childList[j]);
                            }
                        }
                    }
                }
            }
            menuItems = sortedMenu;
        }

        public static void ShowMenu()
        {
            foreach(MenuItem item in menuItems)
            {
                if (item.isHidden)
                {
                    continue;
                }
                Console.WriteLine("." + item.menuName);
                if (item.childItems.Count > 0)
                {
                    foreach (MenuItem child in item.childItems)
                    {
                        if (child.isHidden)
                        {
                            continue;
                        }
                        Console.WriteLine("...." + child.menuName);

                        if(child.childItems.Count > 0)
                        {

                            foreach (MenuItem grandChild in child.childItems)
                            {
                                if (grandChild.isHidden)
                                {
                                    continue;
                                }
                                Console.WriteLine("......" + grandChild.menuName);
                            }
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
