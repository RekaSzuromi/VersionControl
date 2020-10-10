﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data.Entity.Migrations.Model;
using Microsoft.Office.Interop.Excel;

namespace UserMaintenance
{

    public partial class Form1 : Form
    {
        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül

        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            lblFullName.Text = Resource1.FullName; // label1
            //lblFirstName.Text = Resource1.FirstName; // label2
            btnAdd.Text = Resource1.Add; // button1
            button2.Text = Resource1.Write_to_file;

            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";
            LoadData();
            CreateExcel();
            //CreateTable();

        }
        private void LoadData()
        {
            Flats = context.Flat.ToList();
        }
        private void CreateExcel()
        {

            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application();

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                // Új munkalap
                xlSheet = xlWB.ActiveSheet;

                // Tábla létrehozása
                CreateTable();
                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;

            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {

                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }

        }
        string[] headers;
        
        private void CreateTable()
        {


            //string[] headers = new string[] 
            headers = new string[]
            {
             "Kód",
             "Eladó",
             "Oldal",
             "Kerület",
             "Lift",
             "Szobák száma",
             "Alapterület (m2)",
             "Ár (mFt)",
             "Négyzetméter ár (Ft/m2)"
            };
            for (int i = 0; i < headers.Length; i++) 
            {
                xlSheet.Cells[1, i+1] = headers[i];

            }

            
            object[,] values = new object[Flats.Count, headers.Length];

            int counter = 0;
            foreach (Flat f in Flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;
                string x;
                if (f.Elevator)
                {
                    x = "Van";
                }
                else 
                {
                    x = "Nincs";
                
                }
                values[counter, 4] = x;
                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = "=" + GetCell(counter + 2, 8) + "/" + GetCell(counter + 2, 7) + "*1000000";
                counter++;

                xlSheet.get_Range(
                    GetCell(2, 1),
                    GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;


                FormatTable();


            }

            string GetCell(int x, int y)
            {
                string ExcelCoordinate = "";
                int dividend = y;
                int modulo;

                while (dividend > 0)
                {
                    modulo = (dividend - 1) % 26;
                    ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                    dividend = (int)((dividend - modulo) / 26);
                }
                ExcelCoordinate += x.ToString();

                return ExcelCoordinate;
            }
            void FormatTable() 
            {
                Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
                headerRange.Font.Bold = true;
                headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                headerRange.EntireColumn.AutoFit();
                headerRange.RowHeight = 40;
                headerRange.Interior.Color = Color.LightBlue;
                headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

                int lastRowID = xlSheet.UsedRange.Rows.Count;
                int lastColumnID = xlSheet.UsedRange.Columns.Count;

                Excel.Range columnRange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, 1));
                columnRange.Interior.Color = Color.LightGoldenrodYellow;
                columnRange.Font.Bold = true;
                Excel.Range lastcolumnRange = xlSheet.get_Range(GetCell(1, lastColumnID), GetCell(lastRowID, lastColumnID));
                lastcolumnRange.Interior.Color = Color.LightGreen;
                lastcolumnRange.NumberFormat = "#\\ ##0.00";
                Excel.Range entireRange = xlSheet.get_Range(GetCell(1, 1), GetCell(lastRowID, lastColumnID));
                entireRange.BorderAround2(Excel.XlLineStyle.xlContinuous, XlBorderWeight.xlMedium);



            }

            
        }
        
        
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = txtFullName.Text,
                //FirstName = txtFirstName.Text
            };
            users.Add(u);
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save a file";
            saveFileDialog1.ShowDialog();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            listUsers.Items.RemoveAt(listUsers.Items.Count - 1);


        }
    }
}