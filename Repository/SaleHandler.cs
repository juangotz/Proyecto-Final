using Microsoft.Data.SqlClient;

public class SaleHandler : DbHandler
{

    SqlConnection sqlConnection = new SqlConnection(ConnectionString);

    public List<Sale> GetSale()
    {
        List<Sale> sales = new List<Sale>();
        using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        {
            using (SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Venta", sqlConnection))
            {
                sqlConnection.Open();

                using (SqlDataReader dataReader = sqlCmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Sale sale = new Sale();
                            sale.id = Convert.ToInt32(dataReader["Id"]);
                            sale.comment = dataReader["Comentarios"].ToString();
                        }
                    }
                }
                sqlConnection.Close();
            }

        }
        return sales;
    }

    public void UpdateSoldProduct(Sale sale)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryCreate = "UPDATE [SistemaGestion].[dbo].[Venta] " +
                    "SET Comentarios = @comentarios WHERE Id = @idVenta";

                SqlParameter idParameter = new SqlParameter("@idVenta", System.Data.SqlDbType.BigInt)
                {
                    Value = sale.id
                };
                SqlParameter commentParameter = new SqlParameter("@comentarios", System.Data.SqlDbType.Char)
                {
                    Value = sale.comment
                };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(commentParameter);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void CreateSoldProduct(Sale sale)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryCreate = "INSERT INTO [SistemaGestion].[dbo].[Venta] " +
                    "(Comentarios) " +
                    "VALUES (@comentarios)";

                SqlParameter commentParameter = new SqlParameter("@comentarios", System.Data.SqlDbType.Char)
                {
                    Value = sale.comment
                };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(commentParameter);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void DeleteSale(Sale sale)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {


                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Venta] WHERE Id = @idVenta";
                SqlParameter sqlParameter = new SqlParameter("IdProductoVendido", System.Data.SqlDbType.BigInt)
                {
                    Value = sale.id
                };
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    sqlCommand.ExecuteScalar();
                }
                sqlConnection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}