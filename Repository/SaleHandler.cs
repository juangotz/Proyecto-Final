using APICODERHOUSE.Models;
using Microsoft.Data.SqlClient;

namespace APICODERHOUSE.Repository
{
    public class SaleHandler : DBHandler
    {
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        public static List<SaleAndInfo> GetSale()
        {
            List<SaleAndInfo> sales = new List<SaleAndInfo>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string querySelect = "SELECT * FROM Venta AS v INNER JOIN ProductoVendido AS pv ON V.IdProducto = pv.IdProducto";
                using (SqlCommand sqlCmd = new SqlCommand(querySelect, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCmd.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                SaleAndInfo sale = new SaleAndInfo();
                                sale.id = Convert.ToInt32(dataReader["Id"]);
                                sale.comment = dataReader["Comentarios"].ToString();
                                sale.idProduct = Convert.ToInt32(dataReader["IdProducto"]);
                                sale.stock = Convert.ToInt32(dataReader[""]);

                                sales.Add(sale);
                            }
                        }
                    }
                    sqlConnection.Close();
                }

            }
            return sales;
        }
        public static bool CreateSale(Sale sale)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryCreate = "INSERT INTO [SistemaGestion].[dbo].[Venta] " +
                        "(Comentarios, IdProducto) " +
                        "VALUES (@comentarios, @idProducto)";

                    SqlParameter commentParameter = new SqlParameter("@comentarios", System.Data.SqlDbType.Char)
                    {
                        Value = sale.comment
                    };
                    SqlParameter idProductParameter = new SqlParameter("@idProducto", System.Data.SqlDbType.BigInt)
                    {
                        Value = sale.idProduct
                    };
                    sqlConnection.Open();
                    bool confirmProduct = UpdateStockFromCreate(sale.idProduct);
                    if (confirmProduct == true)
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                        {
                            sqlCommand.Parameters.Add(commentParameter);
                            sqlCommand.Parameters.Add(idProductParameter);
                            int rowsAffected = sqlCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                result = true;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("The product that was chose for purchase does not exist.");
                    }
                    sqlConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        public static bool UpdateStockFromCreate(int id)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = "UPDATE Producto SET Stock = Stock-1 WHERE Id = @IdProducto " +
                        "UPDATE ProductoVendido SET Stock = Stock+1 WHERE IdProducto = @IdProducto";

                    SqlParameter idParameter = new SqlParameter("idProducto", System.Data.SqlDbType.BigInt)
                    {
                        Value = id
                    };
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 1)
                        {
                            result = true;
                        }
                    }

                    sqlConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        public static bool DeleteSale(int id)
        {
            int idProduct = 0;
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string querySelect = "SELECT IdProducto FROM [SistemaGestion].[dbo].[Venta] " + 
                        "WHERE Id = @idVenta";
                    string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Venta] " +
                        "WHERE Id = @idVentaa";
                    SqlParameter idParameter1 = new SqlParameter("idVenta", System.Data.SqlDbType.BigInt)
                    {
                        Value = id
                    };
                    SqlParameter idParameter2 = new SqlParameter("idVentaa", System.Data.SqlDbType.BigInt)
                    {
                        Value = id
                    };
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter1);
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    idProduct = Convert.ToInt32(dataReader["IdProducto"]);
                                }
                            }
                        }
                    }
                    bool confirmDelete = UpdateStockFromDelete(idProduct);
                    if (confirmDelete == true)
                    {
                        using (SqlCommand sqlCommand2 = new SqlCommand(queryDelete, sqlConnection))
                        {
                            sqlCommand2.Parameters.Add(idParameter2);
                            int rowsAffected = sqlCommand2.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                result = true;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("An Error has ocurred while trying to delete the sale");
                    }
                    sqlConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        public static bool UpdateStockFromDelete(int id)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = "UPDATE Producto SET Stock = Stock+1 WHERE Id = @IdProducto " +
                        "UPDATE ProductoVendido SET Stock = Stock-1 WHERE IdProducto = @IdProducto";

                    SqlParameter idParameter = new SqlParameter("idProducto", System.Data.SqlDbType.BigInt)
                    {
                        Value = id
                    };
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 1)
                        {
                            result = true;
                        }
                    }

                    sqlConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
    }
}
