using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lax.Classes
{
    public static class SQLToolbox
    {
        //Connection string used for connecting
        const string connectionString = "Data Source=10.0.4.114;Initial Catalog=master;User ID=Logon;Password=Passw0rd1";

        #region CreateMovie
        /*
         * Function used for populating rows in our db
         * Takes in movie object and pulls properties out of it and into the SQL query
         */
        public static void CreateMovie(Movie m)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.MovieTable VALUES ('{m.Thumbnail}','{m.Title}',{m.Rating.ToString().Replace(',', '.')},'{m.Genre}',{m.Runtime},{m.Price.ToString().Replace(',', '.')},'{m.ReleaseDate}')", connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region ReadMovieData
        /*
         * Function used for reading all rows and columns into a list
         * returns a list
         */
        public static List<Movie> GetMovieData()
        {
            List<Movie> movies = new List<Movie>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.MovieTable", connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Movie movie = new Movie();
                        movie.Id = reader.GetInt32(0);
                        movie.Thumbnail = reader.GetString(1);
                        movie.Title = reader.GetString(2);
                        movie.Rating = (double)reader.GetDecimal(3);
                        movie.Genre = reader.GetString(4);
                        movie.Runtime = reader.GetInt32(5);
                        movie.Price = (double)reader.GetDecimal(6);
                        movie.ReleaseDate = reader.GetString(7);
                        movies.Add(movie);
                    }
                    reader.Close();
                }
            }
            return movies;
        }
        #endregion

        #region UpdateMovie
        /*
         * Function used for updating rows and columns relevant for the specific object that it takes in
         */
        public static void UpdateMovie(Movie m)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using (SqlCommand cmd = new SqlCommand($"UPDATE dbo.MovieTable SET Thumbnail = '{m.Thumbnail}',Title = '{m.Title}',Rating = cast({m.Rating.ToString().Replace(',','.')} as decimal),Genre = '{m.Genre}',Runtime = {m.Runtime},Price = cast({m.Price.ToString().Replace(',', '.')} as decimal),ReleaseDate = '{m.ReleaseDate}' WHERE ID = {m.Id}", connection))
                using (SqlCommand cmd = new SqlCommand($"UPDATE dbo.MovieTable SET Thumbnail = '{m.Thumbnail}',Title = '{m.Title}',Rating = {m.Rating.ToString().Replace(',', '.')},Genre = '{m.Genre}',Runtime = {m.Runtime},Price = {m.Price.ToString().Replace(',', '.')},ReleaseDate = '{m.ReleaseDate}' WHERE ID = {m.Id}", connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region DeleteMovie
        /*
         * Function used for removing a movie where the id is equal to the movie object it takes in
         */
        public static void RemoveMovie(Movie m)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.MovieTable WHERE ID = {m.Id}", connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion
        
    }
}
