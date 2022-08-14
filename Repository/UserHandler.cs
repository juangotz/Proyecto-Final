using Microsoft.Data.SqlClient;

public class UserHandler : DbHandler
{

    SqlConnection sqlConnection = new SqlConnection(ConnectionString);

    public List<User> GetUsers()
    {
        List<User> usuarios = new List<User>();
        using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        {
            using (SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Usuario", sqlConnection))
            {
                sqlConnection.Open();

                using (SqlDataReader dataReader = sqlCmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            User user = new User();
                            user.id = Convert.ToInt32(dataReader["Id"]);
                            user.name = dataReader["Nombre"].ToString();
                            user.surname = dataReader["Apellido"].ToString();
                            user.userName = dataReader["NombreUsuario"].ToString();
                            user.email = dataReader["Email"].ToString();
                            user.password = dataReader["Contraseña"].ToString();
                        }
                    }
                }


                sqlConnection.Close();

            }

        }
        return usuarios;
    }

    public void DeleteUser(User user)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Usuario] WHERE Id = @IdUsuario";
                SqlParameter sqlParameter = new SqlParameter("IdUsuario", System.Data.SqlDbType.BigInt)
                {
                    Value = user.id
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

    public void UpdateUser(User user)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Usuario]"+
                    "SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, "+
                    "Contraseña = @contraseña, Mail = @mail WHERE Id = @idUsuario";

                SqlParameter idParameter = new SqlParameter("idUsuario", System.Data.SqlDbType.BigInt)
                {
                    Value = user.id
                };
                SqlParameter nameParameter = new SqlParameter("nombre", System.Data.SqlDbType.Char)
                {
                    Value = user.name
                };
                SqlParameter surnameParameter = new SqlParameter("apellido", System.Data.SqlDbType.Char)
                {
                    Value = user.surname
                };
                SqlParameter userParameter = new SqlParameter("nombreUsuario", System.Data.SqlDbType.Char)
                {
                    Value = user.userName
                };
                SqlParameter mailParameter = new SqlParameter("mail", System.Data.SqlDbType.Char)
                {
                    Value = user.email
                };
                SqlParameter passwordParameter = new SqlParameter("@contraseña", System.Data.SqlDbType.Char)
                {
                    Value = user.password
                };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nameParameter);
                    sqlCommand.Parameters.Add(surnameParameter);
                    sqlCommand.Parameters.Add(userParameter);
                    sqlCommand.Parameters.Add(mailParameter);
                    sqlCommand.Parameters.Add(passwordParameter);
                    sqlCommand.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void CreateUser(User user)
    {
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryCreate = "INSERT INTO [SistemaGestion].[dbo].[Usuario]"
                    + "(Nombre, Apellido, NombreUsuario, Contraseña, Mail) "
                    + "VALUES ('@nombre', '@apellido', '@nombreUsuario', '@contraseña', '@mail')";
                SqlParameter nameParameter = new SqlParameter("nombre", System.Data.SqlDbType.Char)
                {
                    Value = user.name
                };
                SqlParameter surnameParameter = new SqlParameter("apellido", System.Data.SqlDbType.Char)
                {
                    Value = user.surname
                };
                SqlParameter userParameter = new SqlParameter("nombreUsuario", System.Data.SqlDbType.Char)
                {
                    Value = user.userName
                };
                SqlParameter mailParameter = new SqlParameter("mail", System.Data.SqlDbType.Char)
                {
                    Value = user.email
                };
                SqlParameter passwordParameter = new SqlParameter("@contraseña", System.Data.SqlDbType.Char)
                {
                    Value = user.password
                };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nameParameter);
                    sqlCommand.Parameters.Add(surnameParameter);
                    sqlCommand.Parameters.Add(userParameter);
                    sqlCommand.Parameters.Add(mailParameter);
                    sqlCommand.Parameters.Add(passwordParameter);
                    sqlCommand.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static List<User> LoginMethodPart1(string username, string password)
    {
        List <User> result = new List<User>();
        using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        {
            using (SqlCommand sqlCmd = new SqlCommand("SELECT * FROM [SistemaGestion].[dbo].[Usuario] " +
                "WHERE NombreUsuario = @nombreUsuario AND Contraseña = Contraseña", sqlConnection))
            {
                SqlParameter userParameter = new SqlParameter("nombreUsuario", System.Data.SqlDbType.Char)
                {
                    Value = username
                };
                SqlParameter passwordParameter = new SqlParameter("@contraseña", System.Data.SqlDbType.Char)
                {
                    Value = password
                };
                sqlConnection.Open();

                using (SqlDataReader dataReader = sqlCmd.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            User userLogin = new User();
                            userLogin.id = Convert.ToInt32(dataReader["Id"]);
                            userLogin.name = dataReader["Nombre"].ToString();
                            userLogin.surname = dataReader["Apellido"].ToString();
                            userLogin.userName = dataReader["NombreUsuario"].ToString();
                            userLogin.email = dataReader["Email"].ToString();
                            userLogin.password = dataReader["Contraseña"].ToString();
                        }
                    }
                }
                sqlConnection.Close();
            }
        }
        return result;
    }
}
