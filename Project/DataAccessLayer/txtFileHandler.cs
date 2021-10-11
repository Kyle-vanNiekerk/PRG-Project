﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Project.BusinessLogicLayer;

namespace Project.DataAccessLayer
{
    class txtFileHandler
    {
        //public const string path = @"F:\PRG Project\Project\DataLayer\Employees.txt"; //Absolute Path to txt data file
        //public const string path = @".\Project\DataLayer\Employees.txt"; //Finds Path + File, but does not work

        static string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); //Relative Path to txt data file
        string path = dir + @"..\..\..\..\DataLayer\Employees.txt";

        public List<Employee> EmpList = new List<Employee>();   //For using in program
        public List<string> readList = new List<string> ();     //For Saving to file, updated alongside EmpList

        //Add Employee to List
        public void addEmployee(string name, string pass)
        {
            readList.Add(new Employee(name, pass).ToString());
            EmpList.Add(new Employee(name, pass));
        }
        
        //Write contents from a list to a txt file
        public void writeFile()
        {
            StreamWriter myWriter = new StreamWriter(path, true); //append, not overwrite
            foreach (var employee in EmpList)
            {
                myWriter.WriteLine(employee.ToString());
            }
            myWriter.Close();
        }

        //Reading from txt File to a list
        public List<Employee> readFile()
        {
            FileStream myStream = new FileStream(path, FileMode.OpenOrCreate);
            StreamReader myReader = new StreamReader(myStream);

            string line, name, pass;

            while ((line = myReader.ReadLine()) != null)
            {
                name = line.Split(new Char[] { ',' })[0];
                pass = line.Substring(line.LastIndexOf(',') + 1);
                EmpList.Add(new Employee(name, pass));
                readList.Add(new Employee(name,pass).ToString());
            }
            myReader.Close();
            myStream.Close();
            
            return EmpList;
        }

        public int login(string name, string pass)
        {
            //bool flagValid = false;
            bool flagName = false;
            bool flagPass = false;

            string testName, testPass;

            for (int i =0; i < EmpList.Count; i++)
            {
                testName = EmpList[i].Username;
                testPass = EmpList[i].Password;
                if (testName == name)
                {
                    flagName = true;
                    if (testPass == pass)
                    {
                        flagPass = true;
                        //flagValid = true;
                        return 1;
                    }
                }
            }
            if (flagPass == true && flagName == true)
            {
                return 1;
            }
            else
            if ((flagName == true) && (flagPass == false))
            {
                return 2;
            }
            else
            if ((flagName == false) && (flagPass == true))
            {
                return 3;
            }
            else
                return 4;
        }
    }


}
