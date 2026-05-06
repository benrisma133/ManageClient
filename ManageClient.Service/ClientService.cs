using ManageClient.Repository;
using ManageClient.Repository.Models;

namespace ManageClient.Service;

public class ClientService
{
    public enum enMode { AddNew, Update }
    public enMode Mode { get; private set; } = enMode.AddNew;

    private bool _AddNew()
    {
        // Check duplicates before insert
        if (ClientRepository.IsClientExistByPhone(Client.PhoneNumber))
            return false;

        if (ClientRepository.IsClientExistByEmail(Client.Email))
            return false;

        int newId = ClientRepository.Add(Client);

        if (newId > 0)
        {
            Client.Id = newId;
            Mode = enMode.Update; // switch to update mode after successful add
            return true;
        }

        return false;
    }

    private bool _Update()
    {
        // Check duplicates excluding current client
        if (ClientRepository.IsClientExistByPhone(Client.PhoneNumber, Client.Id))
            return false;

        if (ClientRepository.IsClientExistByEmail(Client.Email, Client.Id))
            return false;

        return ClientRepository.Update(Client);
    }

    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                return _AddNew();

            case enMode.Update:
                return _Update();

            default:
                return false;
        }
    }

    public bool Delete()
    {
        return ClientRepository.Delete(Client.Id);
    }

    public List<Client> GetAll()
    {
        return ClientRepository.GetAll();
    }

    public Client? GetClientById(int id)
    {
        return ClientRepository.GetClientById(id);
    }

    // Load existing client for editing → mode becomes Update
    public static ClientService Load(int id)
    {
        var client = ClientRepository.GetClientById(id);
        if (client == null)
            throw new Exception($"Client with ID {id} not found.");

        return new ClientService
        {
            Client = client,
            Mode = enMode.Update
        };
    }

    public Client Client { get; set; } = new Client();
}