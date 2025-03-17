using System;
using daoexample.DTOs;
using daoexample.Persistence.DAO;
using Microsoft.Data.SqlClient;

/// <summary>
/// Summary description for Class1
/// </summary>
public class AddressDAO
{
    private readonly string _connectionString;

    public AddressDAO(string connectionsString)
    {
        this._connectionString = connectionsString;
    }
    public void AddAddress(AddressDTO adress)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Address (Street, City, PostalCode) VALUES (@Street, @City, @PostalCode)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", adress.Street);
                command.Parameters.AddWithValue("@Surname", adress.City);
                command.Parameters.AddWithValue("@PostalCode", adress.PostalCode);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
    public void AddAddressess(List<AddressDTO> addresses)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var address in addresses)
                    {
                        string query = "INSERT INTO Address (Street, City, PostalCode) VALUES (@Street, @City, @PostalCode)";
                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Name", address.Street);
                            command.Parameters.AddWithValue("@Surname", address.City);
                            command.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    Console.WriteLine("Tots les adresses s'han afegit correctament.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error en inserir adresses: {ex.Message}");
                }
            }
        }
    }
    public void DeleteAddress(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM Address WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
    public IEnumerable<AddressDTO> GetAllAddresses()
    {
        List<AddressDTO> addresses = new List<AddressDTO>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT Id, Street, City, PostalCode FROM Address";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AddressDTO address = new AddressDTO
                        {
                            Id = reader.GetInt32(0),
                            Street = reader.GetString(1),
                            City = reader.GetString(2),
                            PostalCode = reader.GetString(3)
                        };
                        addresses.Add(address);
                    }
                }
            }
        }
        return addresses;
    }
    public AddressDTO GetAddresstByID(int id)
    {
        AddressDTO address = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT Id, Street, City, PostalCode FROM Address WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        address = new AddressDTO
                        {
                            Id = reader.GetInt32(0),
                            Street = reader.GetString(1),
                            City = reader.GetString(2),
                            PostalCode = reader.GetString(3)
                        };
                    }
                }
            }
        }
        return address;
    }
    public void UpdateAddress(AddressDTO address)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Address SET Street = @Street, City = @City, PostalCode= @PostalCode  WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Street", address.Street);
                command.Parameters.AddWithValue("@City", address.City);
                command.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                command.Parameters.AddWithValue("@Id", address.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
