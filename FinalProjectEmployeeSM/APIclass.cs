using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Azure.Core;
using Microsoft.Data.SqlClient;

namespace FinalProjectEmployeeSM
{
    internal class APIclass
    {
        private string connectionStringZin = "_Put-Your-Connection-String_";


        string getAdminQuery = "SELECT * FROM adminAcc WHERE username = @usernameTem";

        public string checkAuth(string username, string password)
        {
            if (username == null || password == null)
            {
                return "Please put the username and password correctly to continue";
            }
            using (SqlConnection connection = new SqlConnection(connectionStringZin))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception err)
                {
                    return err.Message;
                }
                using (SqlCommand command = new SqlCommand(getAdminQuery, connection))
                {
                    command.Parameters.AddWithValue("@usernameTem", username.ToLower());

                    var reader = command.ExecuteReader();
                    reader.Read();

                    if (!reader.HasRows)
                    {
                        return "User not Foundssss";
                    }
                    //retrieve data from database response

                    string passwordFromDB = reader.GetString(2);


                    //Validation the data
                    if (password.Trim() != passwordFromDB.Trim())
                    {
                        return "Login Failed, Please try insert again";
                    }
                }


                return null;
            }
        }


        public DataTable getDataForDataGrid()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionStringZin))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT Employees.EmployeeID, FirstName, LastName, DOB, Address, SalaryAmount " +
                        "                                       FROM Employees inner join Salaries " +
                        "                                       ON Employees.EmployeeID = Salaries.EmployeeID", connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    return null;
                }
            }
            return dt;
        }


        public List<string> getDeptData()
        {
            List<string> departmentNameList = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionStringZin))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Departments", connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var deptName = reader.GetString(1);
                                    departmentNameList.Add(deptName);
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        return null;
                    }


                }
            }
            return departmentNameList;
        }

        public bool PostEmployeeRequest(string firstname, string lastname, string dob, string address, int deptId, int salaryAmount)
        {
            using (SqlConnection connection = new SqlConnection(connectionStringZin))
            {


                //insert into Employees (FirstName, LastName, DOB, Address, DepartmentID)
                //values('mat', 'jame', '2003-10-01', 'Chak Angre Krom', 2)
                SqlTransaction transaction;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                }
                catch
                {
                    return false;
                }

                using (SqlCommand command = new SqlCommand("INSERT INTO Employees (FirstName, LastName, DOB, Address, DepartmentID) " +
                    "\r\n VALUES (@firstname, @lastname, @dob, @address, @deptId)", connection, transaction))
                {
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@dob", dob);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@deptId", deptId.ToString());
                    int affectedRow = command.ExecuteNonQuery();

                    if (affectedRow == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    int employeeId;

                    using (SqlCommand commandGetEmployee = new SqlCommand("SELECT EmployeeID from Employees " +
                    "                                               WHERE FirstName = @firstName and LastName = @lastName", connection, transaction))
                    {
                        commandGetEmployee.Parameters.AddWithValue("@firstName", firstname);
                        commandGetEmployee.Parameters.AddWithValue("@lastName", lastname);

                        SqlDataReader reader = commandGetEmployee.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            employeeId = reader.GetInt32(0);
                            reader.Close();
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    //return true;
                    using (SqlCommand commandInsertSalary = new SqlCommand("INSERT INTO Salaries (EmployeeID, SalaryAmount) " +
                                                                            "VALUES (@employeeID, @salaryAmount)", connection, transaction))
                    {
                        commandInsertSalary.Parameters.AddWithValue("@employeeID", employeeId.ToString());
                        commandInsertSalary.Parameters.AddWithValue("@salaryAmount", salaryAmount.ToString());

                        int affectedRowSalary = commandInsertSalary.ExecuteNonQuery();

                        if (affectedRowSalary == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        transaction.Commit();
                        return true;
                    }
                }
            }
        }

        public List<Dictionary<string, string>> getEmployeeByDeptId(string deptId)
        {
            using (SqlConnection connection = new SqlConnection(connectionStringZin))
            {
                try
                {
                    connection.Open();
                }
                catch { return null; }

                List<Dictionary<string, string>> queriedEmployeeList = new List<Dictionary<string, string>>();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Employees WHERE DepartmentID = @deptIds", connection))
                {
                    command.Parameters.AddWithValue("@deptIds", deptId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Dictionary<string, string> prepDic = new Dictionary<string, string>();
                            prepDic["EmployeeID"] = reader.GetInt32(0).ToString();
                            prepDic["FirstName"] = reader.GetString(1);
                            prepDic["LastName"] = reader.GetString(2);
                            queriedEmployeeList.Add(prepDic);
                        }
                    }
                    return queriedEmployeeList;
                }
            }
        }

        public Dictionary<string, string> getSingleEmployee(string employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionStringZin))
            {
                try
                {
                    connection.Open();
                }
                catch { return null; };

                SqlTransaction transaction = connection.BeginTransaction();

                Dictionary<string, string> employeeDict = new Dictionary<string, string>();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Employees WHERE EmployeeID = @empId", connection, transaction))
                {
                    command.Parameters.AddWithValue("@empId", employeeId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employeeDict["FirstName"] = reader.GetString(1);
                            employeeDict["LastName"] = reader.GetString(2);
                            employeeDict["DOB"] = reader.GetDateTime(3).ToString();
                            employeeDict["Address"] = reader.GetString(4);
                        }
                        reader.Close();
                    }
                    else
                    {
                        transaction.Rollback();
                        return null;
                    }
                }

                using (SqlCommand salaryCommand = new SqlCommand("SELECT SalaryAmount FROM Salaries WHERE EmployeeID = @empIds", connection, transaction))
                {
                    salaryCommand.Parameters.AddWithValue("@empIds", employeeId);

                    SqlDataReader salaryReader = salaryCommand.ExecuteReader();

                    if (salaryReader.HasRows)
                    {
                        while (salaryReader.Read()) {
                            employeeDict["Salary"] = salaryReader.GetDecimal(0).ToString();
                        }
                        salaryReader.Close();
                    }
                    else
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
                    transaction.Commit();
                    return employeeDict;
            }

        }
    
        public bool updateEmployee(Dictionary<string, string> employeeDetail)
        {
            using (SqlConnection connection = new SqlConnection(connectionStringZin))
            {
                try
                {
                    connection.Open();
                }
                catch { return false; }

                SqlTransaction sqlTransaction = connection.BeginTransaction();  

                using (SqlCommand command = new SqlCommand("UPDATE Employees " +
                    "                                       SET FirstName = @FirstNames, " +
                    "                                       LastName = @LastNames, " +
                    "                                       DOB = @DOBs, " +
                    "                                       Address = @Addresses WHERE EmployeeID = @empId", connection, sqlTransaction))
                {
                    command.Parameters.AddWithValue("@FirstNames", employeeDetail["FirstName"]);
                    command.Parameters.AddWithValue("@LastNames", employeeDetail["LastName"]);
                    command.Parameters.AddWithValue("@DOBs", employeeDetail["DOB"]);
                    command.Parameters.AddWithValue("@Addresses", employeeDetail["Address"]);
                    command.Parameters.AddWithValue("@empId", employeeDetail["EmployeeID"]);
                    
                    int affectedrow = command.ExecuteNonQuery();
                    if(affectedrow == 0)
                    {
                        sqlTransaction.Rollback();
                        return false;
                    }
                }

                //UPDATE Salaries
                //Set SalaryAmount = 1;
                using (SqlCommand updateSalaryCommand = new SqlCommand("UPDATE Salaries SET SalaryAmount = @salaryAmounts " +
                    "                                                   WHERE EmployeeID = @empIds", connection, sqlTransaction))
                {
                    updateSalaryCommand.Parameters.AddWithValue("@salaryAmounts", employeeDetail["Salary"]);
                    updateSalaryCommand.Parameters.AddWithValue("@empIds", employeeDetail["EmployeeID"]);

                    int affectedRow = updateSalaryCommand.ExecuteNonQuery();

                    if (affectedRow == 0)
                    {
                        sqlTransaction.Rollback();
                        return false;
                    }
                }

                sqlTransaction.Commit();
                return true;

            }
        }
    
        public bool deleteEmployee(Dictionary<string, string> employeeDetail)
        {
            using (SqlConnection connection = new SqlConnection(connectionStringZin))
            {
                try
                {
                    connection.Open();
                }
                catch
                {
                    return false;
                }

                SqlTransaction transaction = connection.BeginTransaction();

                using (SqlCommand deleteSalaryCommand = new SqlCommand("DELETE FROM Salaries WHERE EmployeeID = @empIds", connection, transaction))
                {
                    deleteSalaryCommand.Parameters.AddWithValue("@empIds", employeeDetail["EmployeeID"]);

                    int affectedRowSalary = deleteSalaryCommand.ExecuteNonQuery();
                    if (affectedRowSalary != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                using (SqlCommand deleteEmployeeCommand = new SqlCommand("DELETE FROM Employees WHERE EmployeeID = @empId", connection, transaction))
                {
                    deleteEmployeeCommand.Parameters.AddWithValue("@empId", employeeDetail["EmployeeID"]);

                    int affectedRowEmployee = deleteEmployeeCommand.ExecuteNonQuery();
                    if (affectedRowEmployee != 1) {
                        transaction.Rollback();
                        return false;
                    }
                }

                transaction.Commit();
                return true;
            }
            
            
        }
        
        public Dictionary<string, string> getReport()
        {
            Dictionary<string, string> reportDetail = new Dictionary<string, string>();
            using (SqlConnection connection = new SqlConnection(connectionStringZin))
            {
                try
                {
                    connection.Open();
                }
                catch { return null; }

                using (SqlCommand totalDeptCommand = new SqlCommand("SELECT COUNT(DepartmentID) FROM Departments", connection))
                {
                    SqlDataReader deptReader = totalDeptCommand.ExecuteReader();
                    if (deptReader.HasRows)
                    {
                        while (deptReader.Read())
                        {
                            reportDetail["totalDept"] = deptReader.GetInt32(0).ToString();
                        }
                        deptReader.Close();
                    }
                    else
                    {
                        deptReader.Close();
                        return null;
                    }

                }

                using (SqlCommand totalDeptCommand = new SqlCommand("SELECT COUNT(EmployeeID) from Employees", connection))
                {
                    SqlDataReader totalEmpReader = totalDeptCommand.ExecuteReader();
                    if (totalEmpReader.HasRows)
                    {
                        while (totalEmpReader.Read())
                        {
                            reportDetail["totalEmp"] = totalEmpReader.GetInt32(0).ToString();
                        }
                        totalEmpReader.Close();
                    }
                    else
                    {
                        totalEmpReader.Close();
                        return null;
                    }
                }

                using (SqlCommand totalSalaryCommand = new SqlCommand("SELECT SUM(SalaryAmount) FROM Salaries", connection))
                {
                    SqlDataReader totalSalaryReader = totalSalaryCommand.ExecuteReader();
                    if (totalSalaryReader.HasRows)
                    {
                        while (totalSalaryReader.Read())
                        {
                            reportDetail["totalSalary"] = totalSalaryReader.GetDecimal(0).ToString();
                        }
                        totalSalaryReader.Close();
                    }
                    else
                    {
                        totalSalaryReader.Close();
                        return null;
                    }
                }

                return reportDetail;
            }
        }
    
        public bool clearData()
        {

            using (SqlConnection connection = new SqlConnection(connectionStringZin))
            {
                try
                {
                    connection.Open();
                }
                catch { return false; }

                SqlTransaction transaction = connection.BeginTransaction();

                using (SqlCommand clearSalaryCommand = new SqlCommand("DELETE FROM Salaries", connection, transaction))
                {
                    int affectedRowSalary = clearSalaryCommand.ExecuteNonQuery();
                    if (affectedRowSalary == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                
                using (SqlCommand clearEmpCommand = new SqlCommand("DELETE FROM Employees", connection, transaction))
                {
                    int affectedRowEmp = clearEmpCommand.ExecuteNonQuery();
                    if (affectedRowEmp == 0) {
                        transaction.Rollback();
                        return false;
                    }
                }

                transaction.Commit();
                return true;
            }
        }

    }

    

    }
