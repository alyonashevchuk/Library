using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Book
    {
        private string author, name;
        private int year;
        private int? pages;
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Year
        {
            get { return year; }
            set
            {
                if (value > DateTime.Now.Year)
                    throw new Exception("Wrong date");
                year = value;
            }
        }
        public int? Pages
        {
            get { return pages; }
            set { pages = value; }
        }
        public Book()
        {
            Author = "<No author>";
            Name = "<No name>";
            Year = 0;
            Pages = null;
        }
        public Book(string a, string n, int y, int p)
        {
            Author = a;
            Name = n;
            Year = y;
            Pages = p;
        }
        public Book(string a, string n, int y)
        {
            Author = a;
            Name = n;
            Year = y;
        }
        public override string ToString()
        {
            string s = String.Format("\t\"" + Name + "\" " + Author + "\nYear: " + Year + "\nPages: " + pages);
            return s;
        }
        public static bool operator >(Book A, Book B)
        {
            return A.Name.CompareTo(B.Name) > 0;
        }
        public static bool operator <(Book A, Book B)
        {
            return B.Author.CompareTo(A.Author) < 0;
        }
    }
    class Library : IEnumerable
    {
        private Book[] list = { };
        public Library() { }
        public Library(int size)
        {
            list = new Book[size];
        }
        public Book this[int id]
        {
            get
            {
                if (id >= list.Length || id < 0)
                    throw new IndexOutOfRangeException("No");
                else return (Book)list[id];
            }
            set { list[id] = (Book)value; }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
        private int FindAuthor(string s)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i].Author == s) return i;
            return -1;
        }
        private int FindName(string s)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i].Name == s) return i;
            return -1;
        }
        private int FindYear(int n)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i].Year == n) return i;
            return -1;
        }
        private int FindPages(int n)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i].Pages == n) return i;
            return -1;
        }
        public Book this[int ch, string str]
        {
            get
            {
                int i = 0;
                if (ch == 1) i = FindAuthor(str);
                if (ch == 2) i = FindName(str);
                if (i == -1)
                {
                    Console.WriteLine("Not found");
                    return null;
                }
                return list[i];
            }
        }
        public Book this[int ch, int n]
        {
            get
            {
                int i = 0;
                if (ch == 1) i = FindYear(n);
                if (ch == 2) i = FindPages(n);
                if (i == -1)
                {
                    Console.WriteLine("Not found");
                    return null;
                }
                return list[i];
            }
        }
        public void add(Book b)
        {
            Array.Resize<Book>(ref list, list.Length + 1);
            list[list.Length - 1] = b;
        }
        public void delete(string str)
        {
            int index = FindName(str);
            Book[] tmp = new Book[list.Length - 1];
            for(int i=0; i<list.Length; i++)
            {
                if (i < index)
                    tmp[i] = list[i];
                else if (i > index)
                    tmp[i - 1] = list[i];
            }
            Array.Resize<Book>(ref list, list.Length - 1);
            Array.Copy(tmp, list, tmp.Length);
        }
        public void sortYear()
        {
            Book temp;
            for (int i = 0; i < list.Length - 1; i++)
            {
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[i].Year > list[j].Year)
                    {
                        temp = list[j];
                        list[j] = list[i];
                        list[i] = temp;
                    }
                }
            }
        }
        public void sortName()
        {
            Book temp;
            for (int i = 0; i < list.Length - 1; i++)
            {
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[i] > list[j])
                    {
                        temp = list[j];
                        list[j] = list[i];
                        list[i] = temp;
                    }
                }
            }
        }
        public void sortAuthor()
        {
            Book temp;
            for (int i = 0; i < list.Length - 1; i++)
            {
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[i] < list[j])
                    {
                        temp = list[j];
                        list[j] = list[i];
                        list[i] = temp;
                    }
                }
            }
        }
        public void sortPages()
        {
            Book temp;
            for (int i = 0; i < list.Length - 1; i++)
            {
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[i].Pages > list[j].Pages)
                    {
                        temp = list[j];
                        list[j] = list[i];
                        list[i] = temp;
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string ch, name, a;
            int y, p;
            Library Biblio = new Library();
            while (true)
            {
                Console.WriteLine("1 - Add new book\n2 - Delete book\n3 - Show books\n4 - Search by title");
                Console.WriteLine("5 - Search by author\n6 - Search by year\n7 - Search by pages");
                Console.WriteLine("8 - Sort by title\n9 - Sort by year\n10 - Sort by pages\n11 - Sort by author\n0 - Exit");
                ch = Console.ReadLine();
                if (ch == "0") break;
                switch (ch)
                {
                    case "1":
                        Console.Write("Name: ");
                        name = Console.ReadLine();
                        Console.Write("Author: ");
                        a = Console.ReadLine();
                        Console.Write("Year: ");
                        y = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Pages: ");
                        p = Convert.ToInt32(Console.ReadLine());
                        Book tmp = new Book(a, name, y, p);
                        Biblio.add(tmp);
                        break;
                    case "2":
                        Console.Write("Enter title: ");
                        Biblio.delete(Console.ReadLine());
                        break;
                    case "3":
                        foreach (Book i in Biblio)
                        {
                            Console.WriteLine(i);
                        }
                        break;
                    case "4":
                        Console.Write("Enter title: ");
                        Console.WriteLine(Biblio[2, Console.ReadLine()]);
                        break;
                    case "5":
                        Console.Write("Enter author: ");
                        Console.WriteLine(Biblio[1, Console.ReadLine()]);
                        break;
                    case "6":
                        Console.Write("Enter year: ");
                        Console.WriteLine(Biblio[1, Convert.ToInt32(Console.ReadLine())]);
                        break;
                    case "7":
                        Console.Write("Enter pages: ");
                        Console.WriteLine(Biblio[2, Convert.ToInt32(Console.ReadLine())]);
                        break;
                    case "8":
                        Biblio.sortName();
                        break;
                    case "9":
                        Biblio.sortYear();
                        break;
                    case "10":
                        Biblio.sortPages();
                        break;
                    case "11":
                        Biblio.sortAuthor();
                        break;
                }
            }
        }
    }
}