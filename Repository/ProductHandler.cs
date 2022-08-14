using Microsoft.Data.SqlClient;

public class ProductHandler : DbHandler
{

    SqlConnection sqlConnection = new SqlConnection(ConnectionString);

    public List<Product> GetProduct()
    {
        List<Product> productos = new List<Product>();
        using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        {
            using (SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Producto", sqlConnection))
            {
                sqlConnection.Open();

                using (SqlDataReader dataReader = sqlCmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Product product = new Product();
                            product.id = Convert.ToInt32(dataReader["Id"]);
                            product.description = dataReader["Descripciones"].ToString();
                            product.idUser = Convert.ToInt32(dataReader["IdUsuario"]);
                            product.price = Convert.ToInt32(dataReader["Precio"]);
                            product.stock = Convert.ToInt32(dataReader["Stock"]);
                            product.priceSell = Convert.ToInt32(dataReader["PrecioVenta"]);
                        }
                    }
                }


                sqlConnection.Close();

            }

        }
        return productos;
    }
    public void UpdateProduct(Product product)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryCreate = "UPDATE [SistemaGestion].[dbo].[Producto] " +
                    "SET Descripciones = @descripciones, Precio/Venta = @precioVenta, Stock = @stock, " +
                    "Costo = @costo, IdUsuario = @idUsuario WHERE Id = @IdProducto";

                SqlParameter idParameter = new SqlParameter("IdProducto", System.Data.SqlDbType.BigInt)
                {
                    Value = product.id
                };
                SqlParameter descriptionParameter = new SqlParameter("@descripciones", System.Data.SqlDbType.Char)
                {
                    Value = product.description
                };
                SqlParameter priceParameter = new SqlParameter("@precioVenta", System.Data.SqlDbType.BigInt)
                {
                    Value = product.price
                };
                SqlParameter stockParameter = new SqlParameter("@stock", System.Data.SqlDbType.BigInt)
                {
                    Value = product.stock
                };
                SqlParameter costParameter = new SqlParameter("@costo", System.Data.SqlDbType.BigInt)
                {
                    Value = product.priceSell
                };
                SqlParameter idUserParameter = new SqlParameter("@idUsuario", System.Data.SqlDbType.BigInt)
                {
                    Value = product.idUser
                };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(descriptionParameter);
                    sqlCommand.Parameters.Add(priceParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(costParameter);
                    sqlCommand.Parameters.Add(idUserParameter);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void CreateProduct(Product product)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryCreate = "INSERT INTO [SistemaGestion].[dbo].[Producto] " +
                    "('Descripciones', 'Precio/Venta', 'Stock', 'Costo', 'IdUsuario') " +
                    "VALUES (@descripciones, @precioVenta, @stock, @costo, @idUsuario)";

                SqlParameter descriptionParameter = new SqlParameter("@descripciones", System.Data.SqlDbType.Char)
                {
                    Value = product.description
                };
                SqlParameter priceParameter = new SqlParameter("@precioVenta", System.Data.SqlDbType.BigInt)
                {
                    Value = product.price
                };
                SqlParameter stockParameter = new SqlParameter("@stock", System.Data.SqlDbType.BigInt)
                {
                    Value = product.stock
                };
                SqlParameter costParameter = new SqlParameter("@costo", System.Data.SqlDbType.BigInt)
                {
                    Value = product.priceSell
                };
                SqlParameter idUserParameter = new SqlParameter("@idUsuario", System.Data.SqlDbType.BigInt)
                {
                    Value = product.idUser
                };
                
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descriptionParameter);
                    sqlCommand.Parameters.Add(priceParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(costParameter);
                    sqlCommand.Parameters.Add(idUserParameter);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void DeleteProduct(Product product)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Producto] WHERE Id = @idProducto";
                SqlParameter idParameter = new SqlParameter("IdProducto", System.Data.SqlDbType.BigInt)
                {
                    Value = product.id
                };
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
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