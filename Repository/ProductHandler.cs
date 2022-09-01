using APICODERHOUSE.Models;
using Microsoft.Data.SqlClient;

namespace APICODERHOUSE.Repository
{
    public class ProductHandler : DBHandler
    {
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        public static List<Product> GetProductos()
        {
            List<Product> result = new List<Product>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto", sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Product producto = new Product();

                                producto.id = Convert.ToInt32(dataReader["Id"]);
                                producto.description = dataReader["Descripciones"].ToString();
                                producto.price = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.priceSell = Convert.ToInt32(dataReader["Costo"]);
                                producto.stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.idUser = Convert.ToInt32(dataReader["IdUsuario"]);

                                result.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return result;
        }
        public static List<SoldProduct> GetSoldProducts(int id)
        {
            List<SoldProduct> result = new List<SoldProduct>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string querySelect = "SELECT * FROM ProductoVendido AS pv INNER JOIN Producto AS p ON p.Id = pv.IdProducto WHERE IdUsuario = @idUsuario";
                SqlParameter idUserParameter = new SqlParameter("@idUsuario", System.Data.SqlDbType.BigInt)
                {
                    Value = id
                };
                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add(idUserParameter);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                SoldProduct productoVendido = new SoldProduct();

                                productoVendido.id = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.description = dataReader["Descripciones"].ToString();
                                productoVendido.stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.idUser = Convert.ToInt32(dataReader["IdUsuario"]);
                                productoVendido.idProduct = Convert.ToInt32(dataReader["IdProducto"]);

                                result.Add(productoVendido);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return result;
        }
        public static bool DeleteProduct(int id)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Producto] WHERE Id = @idProducto " +
                    "DELETE FROM [SistemaGestion].[dbo].[ProductoVendido] WHERE IdProducto = @idProducto " +
                    "DELETE FROM[SistemaGestion].[dbo].[Venta] WHERE IdProducto = @idProducto";
                    SqlParameter idParameter = new SqlParameter("@IdProducto", System.Data.SqlDbType.BigInt)
                    {
                        Value = id
                    };
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
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
        public static bool UpdateDescription(Product product)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Producto]" +
                        "SET Descripciones = @nombre WHERE Id = @idProducto";

                    SqlParameter idParameter = new SqlParameter("@idProducto", System.Data.SqlDbType.BigInt)
                    {
                        Value = product.id
                    };
                    SqlParameter nameParameter = new SqlParameter("@nombre", System.Data.SqlDbType.Char)
                    {
                        Value = product.description
                    };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(nameParameter);
                        sqlCommand.Parameters.Add(idParameter);
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
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
        public static bool CreateProduct(Product product)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryCreate = "INSERT INTO [SistemaGestion].[dbo].[Producto] " +
                        "(Descripciones, PrecioVenta, Stock, Costo, IdUsuario) " +
                        "VALUES (@descripciones, @precioVenta, @stock, @costo, @idUsuario) " +
                        "SELECT SCOPE_IDENTITY()";

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
                        int idProduct = Convert.ToInt32(sqlCommand.ExecuteScalar());

                        bool confirmSoldProduct = CreateSoldProduct(idProduct);
                        if (confirmSoldProduct == true)
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
        public static bool CreateSoldProduct(int idProduct)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryCreate = "INSERT INTO [SistemaGestion].[dbo].[ProductoVendido] " +
                        "(Stock, IdProducto) " +
                        "VALUES (0, @idProducto)";

                    SqlParameter idProductParameter = new SqlParameter("@idProducto", System.Data.SqlDbType.BigInt)
                    {
                        Value = idProduct
                    };

                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idProductParameter);
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result = true;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
