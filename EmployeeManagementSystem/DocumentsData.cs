using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagementSystem
{
    class DocumentsData
    {
        public string EmployeeID { set; get; } // 0
        public string Name { set; get; } // 1
        public string Gender { set; get; } // 2
        public string Contact { set; get; } // 3
        public string Position { set; get; } // 4
        public double Salary { set; get; } // 5

        SqlConnection connect = new SqlConnection(@"Server=localhost;Database=EmployeeDB;Integrated Security=True;");

        public List<DocumentsData> salaryEmployeeListData()
        {
            List<DocumentsData> listdata = new List<DocumentsData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM employees WHERE status = 'Active' " +
                        "AND delete_date IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentsData sd = new DocumentsData();
                            sd.EmployeeID = reader["employee_id"].ToString();
                            sd.Name = reader["full_name"].ToString();
                            sd.Gender = reader["gender"].ToString();
                            sd.Contact = reader["contact_number"].ToString();
                            sd.Position = reader["position"].ToString();
                            sd.Salary = Convert.ToDouble(reader["salary"]);

                            listdata.Add(sd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listdata;
        }
    }
}
