﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TTNhom
{
    public partial class TimKiemForm : Form
    {
        DBAccess access = new DBAccess();
        DataTable table;

        private static string strConn = "Data Source=DESKTOP-1NPLUNJ;Initial Catalog=TTCSDL;Integrated Security=True";
        private static SqlConnection conn = new SqlConnection(DBAccess.strConn);
        private static SqlDataAdapter adt = new SqlDataAdapter();
        private static SqlCommand cmd = new SqlCommand();

        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        public TimKiemForm()
        {
            InitializeComponent();
        }

        private void TimKiemForm_Load(object sender, EventArgs e)
        {

        }

        public void createConn()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.ConnectionString = strConn;
                    conn.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void GetData(string query, DataGridView grid, DataTable table)
        {
            access.createConn();
            adt = new SqlDataAdapter(query, conn);
            adt.Fill(table);
            grid.DataSource = table;
            conn.Close();
        }

        private void LoaiHinhcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            table = new DataTable();
            string category_key = LoaiHinhcomboBox.Text.Trim();
            switch (category_key)
            {
                case ("Mặt Hàng"):
                        GetData("SELECT * FROM MatHang_View", dataGridView1, table);
                    break;
                case ("Loại Hàng"):
                        GetData("SELECT * FROM LoaiHang", dataGridView1, table);
                    break;
                case ("Quầy"):
                        GetData("SELECT * FROM Quay", dataGridView1, table);
                    break;
                case ("Nhà Cung Cấp"):
                        GetData("SELECT * FROM NhaCungCap", dataGridView1, table);
                    break;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            table = new DataTable();
            string key = txtTimKiem.Text.Trim();
            string category_key = LoaiHinhcomboBox.Text.Trim();

            switch (category_key)
            {
                case ("Mặt Hàng"):
                    if (key.Equals(""))
                    {
                        GetData("SELECT * FROM MatHang_View", dataGridView1, table);
                    }
                    else
                    {
                        if (IsNumber(key))
                        {
                            string queryID = "SELECT * FROM MatHang_View WHERE MaMatHang = '" + key + "' ";
                            GetData(queryID, dataGridView1, table);
                        }
                        string query = 
                            "SELECT * FROM MatHang_View WHERE TenMatHang LIKE N'%" + key + "%' " +
                            " OR TenLoaiHang LIKE N'%" + key + "%'" +
                            " OR TenQuay LIKE N'%" + key + "%'";
                        GetData(query, dataGridView1, table);
                    }
                    break;
                case ("Loại Hàng"):
                    if (key.Equals(""))
                    {
                        GetData("SELECT * FROM LoaiHang", dataGridView1, table);
                    }
                    else
                    {
                        if (IsNumber(key))
                        {
                            string queryID = "SELECT * FROM LoaiHang WHERE MaLoaiHang = '" + key + "' ";
                            GetData(queryID, dataGridView1, table);
                        }
                        string query = 
                            "SELECT * FROM LoaiHang WHERE TenLoaiHang LIKE N'%" + key + "%'";
                        GetData(query, dataGridView1, table);
                    }
                    break;
                case ("Quầy"):
                    if (key.Equals(""))
                    {
                        GetData("SELECT * FROM Quay", dataGridView1, table);
                    }
                    else
                    {
                        if (IsNumber(key))
                        {
                            string queryID = "SELECT * FROM Quay WHERE MaQuay = '" + key + "' ";
                            GetData(queryID, dataGridView1, table);
                        }
                        string query = 
                            "SELECT * FROM Quay WHERE TenQuay LIKE N'%" + key + "%'" +
                            " OR MaNhanVien LIKE = '" + key + "'";
                        GetData(query, dataGridView1, table);
                    }
                    break;
                case ("Nhà Cung Cấp"):
                    if (key.Equals(""))
                    {
                        GetData("SELECT * FROM NhaCungCap", dataGridView1, table);
                    }
                    else
                    {
                        if (IsNumber(key))
                        {
                            string queryID = "SELECT * FROM NhaCungCap WHERE MaNCC = '" + key + "' ";
                            GetData(queryID, dataGridView1, table);
                        }
                        string query = "SELECT * FROM NhaCungCap WHERE TenNCC LIKE N'%" + key + "%'" +
                            " OR Phuong LIKE N'%" + key + "%'" +
                            " OR Quan LIKE N'%" + key + "%'" +
                            " OR ThanhPho LIKE N'%" + key + "%'";
                        GetData(query, dataGridView1, table);
                    }
                    break;
            }
        }
    }
}
