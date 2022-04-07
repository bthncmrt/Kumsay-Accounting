using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muhasebe
{
    public class GenisKapsamTicaretDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=ETrade;integrated security=true");

        public List<GenisKapsamTicaret> GetAllGenisKapsamTicaret()
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Select * from GenisKapsamTicarets", _connection);
            SqlDataReader reader = command.ExecuteReader();
            List<GenisKapsamTicaret> genisKapsamTicarets = new List<GenisKapsamTicaret>();
            while (reader.Read())
            {
                GenisKapsamTicaret genisKapsamTicaret = new GenisKapsamTicaret
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Ürün = reader["Ürün"].ToString(),
                    Adet = Convert.ToInt32(reader["Adet"]),
                    Fiyat = Convert.ToDouble(reader["Fiyat"]),
                };
                genisKapsamTicarets.Add(genisKapsamTicaret);
            }
            reader.Close();
            _connection.Close();
            return genisKapsamTicarets;
        }
        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void Add(GenisKapsamTicaret genisKapsamTicaret)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert into Gıdas values(@Ürün,@Adet,@Fiyat)", _connection);
            command.Parameters.AddWithValue("@Ürün", genisKapsamTicaret.Ürün);
            command.Parameters.AddWithValue("@Adet", genisKapsamTicaret.Adet);
            command.Parameters.AddWithValue("@Fiyat", genisKapsamTicaret.Fiyat);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void Update(GenisKapsamTicaret genisKapsamTicaret)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Update GenisKapsamTicarets set Ürün=@Ürün, Fiyat=@Fiyat,Adet=@Adet where Id=@Id", _connection);
            command.Parameters.AddWithValue("@Ürün", genisKapsamTicaret.Ürün);
            command.Parameters.AddWithValue("@Adet", genisKapsamTicaret.Adet);
            command.Parameters.AddWithValue("@Fiyat", genisKapsamTicaret.Fiyat);
            command.Parameters.AddWithValue("@Id", genisKapsamTicaret.Id);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void Delete(int id)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Delete from GenisKapsamTicarets where Id=@id", _connection);

            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            _connection.Close();
        }


    }
}
