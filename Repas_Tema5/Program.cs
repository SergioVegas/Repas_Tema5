
using daoexample.DTOs;
using daoexample.Persistence.DAO;
using daoexample.Persistence.Mapping;
using daoexample.Persistence.Utils;

namespace daoexample
{
    public class Program
    {
        public static void Main()
        {
           //IAddressDAO addressDAP = new AddressDAO(SQLServerUtils.OpenConnection());
            AddressDTO newAddress = new AddressDTO();

            AddressDTO newAddress1 = new AddressDTO
            {
                Street = "Carrer montjuic,12",
                City = "Badalona",
                PostalCode = "08020"
            };
        }
    }
}
