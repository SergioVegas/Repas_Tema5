using daoexample.DTOs;

namespace daoexample.Persistence.DAO
{
    public interface IAddressDAO
    {
        public AddressDTO GetAddresstByID(int id);
        public IEnumerable<AddressDTO> GetAllAddresses();
        public void AddAddressess(List<AddressDTO> addresses);
        public void AddAddress(AddressDTO address);
        public void UpdateAddress(AddressDTO address);
        public void DeleteAddress(int id);
    }
}
