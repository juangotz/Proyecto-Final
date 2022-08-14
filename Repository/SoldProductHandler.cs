using Microsoft.Data.SqlClient;

public class SoldProductHandler : DbHandler
{

    SqlConnection sqlConnection = new SqlConnection(ConnectionString);

    public List<SoldProduct> GetSoldProduct()
    {
        List<SoldProduct> soldProducts = new List<SoldProduct>();
        using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        {
            using (SqlCommand sqlCmd = new SqlCommand("SELECT * FROM ProductoVendido", sqlConnection))
            {
                sqlConnection.Open();

                using (SqlDataReader dataReader = sqlCmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            SoldProduct soldProduct = new SoldProduct();
                            soldProduct.id = Convert.ToInt32(dataReader["Id"]);
                            soldProduct.stock = Convert.ToInt32(dataReader["Stock"]);
                            soldProduct.idProduct = Convert.ToInt32(dataReader["IdProducto"]);
                            soldProduct.idSell = Convert.ToInt32(dataReader["IdVenta"]);
                        }
                    }
                }
                sqlConnection.Close();
            }

        }
        return soldProducts;
    }
    public void UpdateSoldProduct(SoldProduct soldProduct)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "UPDATE [SistemaGestion].[dbo].[ProductoVendido] " +
                    "SET  Stock = @stock, IdProducto = @idProducto, IdVenta = @idVenta " +
                    "WHERE Id = @idProductoVendido";

                SqlParameter idParameter = new SqlParameter("idProductoVendido", System.Data.SqlDbType.BigInt)
                {
                    Value = soldProduct.id
                };
                SqlParameter stockParameter = new SqlParameter("@stock", System.Data.SqlDbType.BigInt)
                {
                    Value = soldProduct.stock
                };
                SqlParameter idProductParameter = new SqlParameter("@idProducto", System.Data.SqlDbType.BigInt)
                {
                    Value = soldProduct.idProduct
                };
                SqlParameter idSellParameter = new SqlParameter("@idVenta", System.Data.SqlDbType.BigInt)
                {
                    Value = soldProduct.idSell
                };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idProductParameter);
                    sqlCommand.Parameters.Add(idSellParameter);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void CreateSoldProduct(SoldProduct soldProduct)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryCreate = "INSERT INTO [SistemaGestion].[dbo].[ProductoVendido] " +
                    "(Stock, IdProducto, IdVenta) " +
                    "VALUES (@stock, @idProducto, @idVenta)";

                SqlParameter stockParameter = new SqlParameter("@descripciones", System.Data.SqlDbType.BigInt)
                {
                    Value = soldProduct.stock
                };
                SqlParameter idProductParameter = new SqlParameter("@precioVenta", System.Data.SqlDbType.BigInt)
                {
                    Value = soldProduct.idProduct
                };
                SqlParameter idSellParameter = new SqlParameter("@stock", System.Data.SqlDbType.BigInt)
                {
                    Value = soldProduct.idSell
                };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idProductParameter);
                    sqlCommand.Parameters.Add(idSellParameter);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void DeleteSoldProduct(SoldProduct soldProduct)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {


                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[ProductoVendido] WHERE Id = @idProductoVendido";
                SqlParameter sqlParameter = new SqlParameter("IdProductoVendido", System.Data.SqlDbType.BigInt)
                {
                    Value = soldProduct.id
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