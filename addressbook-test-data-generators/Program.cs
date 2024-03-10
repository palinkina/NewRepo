﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {//

        static void Main(string[] args)
        {
            
            int count = Convert.ToInt32(args[0]);
            string filename = args[1];
            string format = args[2];

            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(15))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }
            List<ContactData> contacts = new List<ContactData>();
            for (int y = 0; y < count; y++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(15), TestBase.GenerateRandomString(15),"")
                {
                    Address = TestBase.GenerateRandomString(30),
                    HomePhone = TestBase.GenerateRandomString(30),
                    MobilePhone = TestBase.GenerateRandomString(30),
                    WorkPhone = TestBase.GenerateRandomString(30),
                    Email = TestBase.GenerateRandomString(30),
                    Email2 = TestBase.GenerateRandomString(30),
                    Email3 = TestBase.GenerateRandomString(30)
                });
            }
            if (format == "excel")
            {
                    writeGroupsToExcelFile(groups, filename);
            }
            else
            {
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    writeGroupsToCsvFile(groups, writer);
                }
                else if (format == "xml")
                {
                    if (filename == "groups.xml")
                    {
                        writeGroupsToXmlFile(groups, writer);
                    }
                    else if (filename == "contacts.xml")
                    {
                         writeContactsToXmlFile(contacts, writer);
                    }

                }
                else if (format == "json")
                {
                    if (filename == "groups.json")
                    {
                        writeGroupsToJsonFile(groups, writer);
                    }
                    else if (filename == "contacts.json")
                    {
                          writeContactsToJsonFile(contacts, writer);
                    }

                }
                else
                {
                    Console.Out.Write("Unrecognized format" + format);
                }
                writer.Close();

            }
        }
        private static void writeContactsToJsonFile(object contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
        private static void writeContactsToXmlFile(object contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }
        static void writeGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = (Excel.Worksheet)wb.ActiveSheet;
            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }
        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }
        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }
        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
    }
}


