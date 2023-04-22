using ContentRating2.Models;
using System.Data.SqlClient;

namespace ContentRating2.Repositories
{
    public class FilmsRepository
    {
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CinemaService;Integrated Security=True";

            public bool CreateFilms(FilmsM Film)
            {
                string insertQuery = "INSERT INTO [Movies] " + "([Id], [Name], [Date],[Genre]" +
                    "VALUES (@Id, @Name, @Date,@Genre)";


                SqlCommand command = new SqlCommand(insertQuery);
                command.Parameters.AddWithValue("@Id", Film.Id);
                command.Parameters.AddWithValue("@Name", Film.Name);
                command.Parameters.AddWithValue("@Date", Film.Date);
                command.Parameters.AddWithValue("@Genre", Film.Genre);


                return  ExecuteNonQuery(command);
            }

            public bool UpdateFilms(FilmsM Film)
            {
                string insertQuery = "UPDATE [Films] SET " +
                        "[Id] = @Id, [Name] = @Name, [Date] = @Date," +
                        "[Genre] = @Genre" +
                        " WHERE ID = @ID";

                SqlCommand command = new SqlCommand(insertQuery);
                command.Parameters.AddWithValue("@Id", Film.Id);
                command.Parameters.AddWithValue("@Name", Film.Name);
                command.Parameters.AddWithValue("@Date", Film.Date);
                command.Parameters.AddWithValue("@Genre", Film.Genre);

                return ExecuteNonQuery(command);
            }
            public bool DeleteFilms(int FilmsId)
            {
                string deleteQuery = "DELETE FROM [Films] WHERE ID = @ID";
                SqlCommand command = new SqlCommand(deleteQuery);
                command.Parameters.AddWithValue("@ID", FilmsId);
                return ExecuteNonQuery(command);
            }

            public IEnumerable<FilmsM> GetFilms()
            {
                List<FilmsM> films = new List<FilmsM>();

                SqlConnection connection = new SqlConnection(connectionString);

                try
                {
                    SqlCommand command = new SqlCommand(@"SELECT [ID],[Id], [Description] FROM [CinemaService].[dbo].[Movies]");
                    command.Connection = connection;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            films.Add(new FilmsM
                            {
                                ID = (int)reader["ID"],
                                Id = (int)reader["Id"],
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return films;
                }
                finally
                {
                    connection.Close();
                }

                return films;
            }
            public FilmsM GetFilmsById(int filmId)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                string sql = "SELECT * FROM [Films] WHERE [ID] = @ID";
                SqlCommand command = new SqlCommand(sql);
                command.Parameters.AddWithValue("@Id", filmId);
                command.Connection = connection;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new FilmsM
                        {
                            ID = (int)reader["ID"],
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Date = (DateTime)reader["Date"],
                            Genre = (string)reader["Genre"],

                        };

                    }
                }
                return null;
            }


            public bool ExecuteNonQuery(SqlCommand command)
            {
                SqlConnection connection = new SqlConnection(connectionString);

                try
                {
                    command.Connection = connection;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }

                return true;
            }
    }
}
