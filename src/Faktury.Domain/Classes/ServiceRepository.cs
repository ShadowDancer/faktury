using System.Collections.Generic;

namespace Faktury.Domain.Classes
{
    public class ServiceRepository
    {
        private int _highestServiceId;
        private readonly List<Service> _services = new List<Service>();
        public IEnumerable<Service> Services => _services;

        public void AddService(Service service)
        {
            _services.Add(service);
            if (service.Id > _highestServiceId)
            {
                _highestServiceId = service.Id;
            }
        }

        public Service FindService(int id)
        {
            return _services.Find(n => n.Id == id);
        }

        public void RemoveService(Service serviceToRemove)
        {
            _services.Remove(serviceToRemove);
        }

        public void ClearServices()
        {
            _services.Clear();
        }

        public int NewServiceId() => ++_highestServiceId;
    }
}