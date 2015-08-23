using System.Collections;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public abstract class DeriveComparer<T> : Comparer<T>
    {
    }

    public abstract class MyComparer<T> : IComparer, IComparer<T>
    {
        protected MyComparer() { }

        public abstract int Compare(T x, T y);
        public abstract int Compare(object x, object y);
    }

    class Employee
    {
        public int id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("Id = {0}, Name = {1}", id, Name);
        }
    }

    class EmployeeByIdComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return x.id.CompareTo(y.id);
        }
    }
}
