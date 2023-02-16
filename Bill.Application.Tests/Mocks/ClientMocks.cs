using Bill.Domain.Clients;

namespace Bill.Application.Tests.Mocks
{
    public static class ClientMocks
    {
        public static List<Client> GetClientList()
        {
            List<Client> clients = new List<Client>
            {
                new Client
                {
                    Id = "1",
                    FirstName = "Sarah",
                    LastName = "Vohn",
                    Email = "sarah.vohn@gmail.com",
                    PersonalIdentificationNumber = "2970402769965",
                    IsActive = false
                },
                new Client
                {
                    Id = "2",
                    FirstName = "Thomas",
                    LastName = "Wagen",
                    Email = "thomas.wagen@gmail.com",
                    PersonalIdentificationNumber = "1970423469965",
                    IsActive = false
                },
                new Client
                {
                    Id = "3",
                    FirstName = "Karlos",
                    LastName = "Anthonius",
                    Email = "kharlos.anthonius@gmail.com",
                    PersonalIdentificationNumber = "1820373469120",
                    IsActive = false
                }
            };
            return clients;
        }
    }
}