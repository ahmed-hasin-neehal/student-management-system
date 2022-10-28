using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_RECORDS
{
    class Program
    {
        struct student
        {
            public string stnumber;
            public string stname;
            public string firstname;
            public string middlename;
            public string lastname;
            public string Batch;
            public string Department;
            public string Degree;
        };

        static void Main(string[] args)
        {
            const int MAX = 20;
            try
            {
                student[] st = new student[MAX];
                int itemcount = 0;
                int choice;
                string confirm;

                do
                {
                    displaymenu(st, itemcount);
                    Console.WriteLine();
                    Console.Write("Enter your choice(1-3):");
                    choice = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            add(st, ref itemcount);
                            break;
                        case 2:
                            find(st, itemcount);
                            break;
                        case 3:
                            delete(st, ref itemcount);
                            break;
                        default:
                            Console.WriteLine("invalid");
                            break;
                    }

                    Console.Write("Press y or Y to continue:");
                    confirm = Console.ReadLine().ToString();
                    Console.Clear();
                } while (confirm == "y" || confirm == "Y");
            }
            catch (FormatException) { Console.WriteLine("Invalid input"); }
            Console.ReadLine();
        }

        static void displaymenu(student[] st, int itemcount)
        {

            Console.WriteLine("======================================================\n                         MENU                         \n======================================================");
            Console.WriteLine(" 1.Add New Student");
            Console.WriteLine(" 2.View Student Details");
            Console.WriteLine(" 3.Delete Student");
            Console.WriteLine("******************************************************\n");
            viewall(st, itemcount);
        }

        //method add student
        static void add(student[] st, ref int itemcount)
        {

        Again:
            Console.WriteLine("======================================================\n            ADDING STUDENTS DATA/INFORMATION                         \n======================================================");
            Console.Write("Enter student's ID:");
            st[itemcount].stnumber = Console.ReadLine().ToString();

            if (search(st, st[itemcount].stnumber, itemcount) != -1)
            {
                Console.Clear();
                Console.WriteLine("The ID Number you Enter already exists.");
                goto Again;

            }


            Console.Write("Enter student's FIRST NAME:");
            st[itemcount].firstname = Console.ReadLine().ToString();
            Console.Write("Enter student's MIDDLE NAME:");
            st[itemcount].middlename = Console.ReadLine().ToString();
            Console.Write("Enter student's LAST NAME:");
            st[itemcount].lastname = Console.ReadLine().ToString();
            Console.Write("Enter student's Joining Batch:");
            st[itemcount].Batch = Console.ReadLine().ToString();
            Console.Write("Enter student's Department:");
            st[itemcount].Department = Console.ReadLine().ToString();
            Console.Write("Enter student's Degree:");
            st[itemcount].Degree = Console.ReadLine().ToString();
            st[itemcount].stname = st[itemcount].firstname + " " + st[itemcount].middlename + " " + st[itemcount].lastname;
            ++itemcount;

        }

        //Method Delete Student
        static void delete(student[] st, ref int itemcount)
        {
            string id;
            int index;
            if (itemcount > 0)
            {
                Console.WriteLine("======================================================\n         DELETING STUDENTS DATA/INFORMATION                         \n======================================================");
                Console.Write("Enter student's ID:");
                id = Console.ReadLine();
                index = search(st, id.ToString(), itemcount);

                if ((index != -1) && (itemcount != 0))
                {
                    if (index == (itemcount - 1))
                    {

                        clean(st, index);
                        --itemcount;

                        Console.WriteLine("The record was deleted.");
                    }
                    else
                    {
                        for (int i = index; i < itemcount - 1; i++)
                        {
                            st[i] = st[i + 1];
                            clean(st, itemcount);
                            --itemcount;
                        }
                    }
                }
                else Console.WriteLine("The record doesn't exist.Check the ID and try again.");
            }
            else Console.WriteLine("No record to delete");
        }

        //Method View All Students
        static void viewall(student[] st, int itemcount)
        {

            int i = 0;
            Console.WriteLine("====================================================================================================================\n                                                STUDENTS DATA/INFORMATION          \n====================================================================================================================");
            Console.WriteLine();
            Console.WriteLine("{0,-5}\t{1,-30}{2,-15}\t{3,-10}\t{4,-10}", "ID", "NAME", "BATCH", "DEPARTMENT", "DEGREE");
            Console.WriteLine("====================================================================================================================");

            while (i < itemcount)
            {

                if (st[i].stnumber != null)
                {
                    Console.Write("{0,-5}\t{1,-30}{2,-15}\t{3,-10}\t{4,-10}", st[i].stnumber, st[i].stname, st[i].Batch, st[i].Department, st[i].Degree);
                    Console.WriteLine("\n--------------------------------------------------------------------------------------------------------------------\n");
                }

                i = i + 1;
            }
        }

        //Method View Student
        static void find(student[] st, int itemcount)
        {
            Console.WriteLine("======================================================\n                     SEARCH STUDENTS                         \n======================================================");
            string id;
            Console.Write("Enter student's ID:");
            id = Console.ReadLine();

            int index = search(st, id.ToString(), itemcount);
            if (index != -1)
            {
                Console.WriteLine("Student Name: " + st[index].stname);
                Console.WriteLine("Student ID: " + st[index].stnumber);
                Console.WriteLine("Joining Batch: " + st[index].Batch);
                Console.WriteLine("Department: " + st[index].Department);
                Console.WriteLine("Degree: " + st[index].Degree);
                Console.WriteLine();

            }
            else Console.WriteLine("The record doesn't exits.");
        }

        //Method Search for Duplicates
        static int search(student[] st, string id, int itemcount)
        {
            int found = -1;
            for (int i = 0; i < itemcount && found == -1; i++)
            {

                if (st[i].stnumber == id) found = i;

                else found = -1;
            }

            return found;

        }

        //Method Delete Student
        static void clean(student[] st, int index)
        {
            st[index].stnumber = null;
            st[index].firstname = null;
            st[index].middlename = null;
            st[index].lastname = null;
            st[index].stname = null;
            st[index].Batch = null;
            st[index].Department = null;
            st[index].Degree = null;
        }
    }
}
