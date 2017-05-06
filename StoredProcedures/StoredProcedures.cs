using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public class StoredProcedures
{
    [SqlProcedure]
    public static int GetEmployeeCount()
    {
        int iRows;
        var conn = new SqlConnection("Context Connection=true");
        conn.Open();
        var sqlCmd = conn.CreateCommand();
        sqlCmd.CommandText = "select count(*) as 'Employee Count'" + "from employee";
        iRows = (int) sqlCmd.ExecuteScalar();
        conn.Close();
        return iRows;
    }

    public static void Modify_Budget()
    {
        var context = SqlContext.TriggerContext;
        if (context.IsUpdatedColumn(2))
        {
            float budget_old;
            float budget_new;
            string project_number;
            SqlConnection conn = new SqlConnection("context connection = true");
            conn.Open();
            SqlCommand and = conn.CreateCommand();
            and.CommandText = "SELECT budget FROM DELETED";
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT budget FROM INSERTED";
            budget_old = (float) Convert.ToDouble(cmd.ExecuteScalar());
            budget_new = (float) Convert.ToDouble(cmd.ExecuteScalar());
            cmd.CommandText = "SELECT project_no FROM DELETED";
            project_number = Convert.ToString(cmd.ExecuteScalar());
            cmd.CommandText = @"INSERT INTO audit_budget VALUES(@project_number, USER_NAME(), GETDATE(), @budget_old, @budget_new)";
            cmd.Parameters.AddWithValue("@project_number", project_number);
            cmd.Parameters.AddWithValue("@budget_old", budget_old);
            cmd.Parameters.AddWithValue("@budget_new", budget_new);
            cmd.ExecuteNonQuery();
        }
    }
}

public class budgetPercent
{
    private const float percent = 10;

    public static SqlDouble computeBudget(float budget)
    {
        float budgetNew;
        budgetNew = budget * percent;
        return budgetNew;
    }
}

