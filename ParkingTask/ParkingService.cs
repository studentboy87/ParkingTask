using System.Collections.Generic;
using System.Linq;
using ParkingTask.Enums;

namespace ParkingTask
{
    public class ParkingService : IParkingService
    {
        private readonly List<PlaneParkingSpace> _parkingSpaces;

        public ParkingService()
        {
            
        }

        //needs to consume a list of Parking spaces - to be replaced with the appropriate data source e.g. SQL with EF Core
        public ParkingService(List<PlaneParkingSpace> parkingSpaces)
        {
            _parkingSpaces = parkingSpaces;
        }

        public List<PlaneParkingSpace> GetAllParkingSpaces()
        {
            return _parkingSpaces;
        }

        public List<PlaneParkingSpace> GetAllVacantParkingSpaces()
        {
            var spaces =  _parkingSpaces?.Where(x => x.SpaceStatus == SpaceStatus.Vacant).ToList();
            if (spaces == null)
            {
                throw new ParkingSpaceException("There are no available parking spaces");
            }

            return spaces;
        }

        public List<PlaneParkingSpace> GetAllVacantParkingSpacesForType(PlaneType planeType)
        {
            var spaces = _parkingSpaces.Where(x => x.PlaneType == planeType && x.SpaceStatus == SpaceStatus.Vacant).ToList();
            if (spaces == null)
            {
                throw new ParkingSpaceException("There are no available parking spaces");
            }

            return spaces;
        }

        public List<PlaneParkingSpace> GetAllPossibleVacantParkingSpaces(PlaneType planeType)
        {
            var spaces = _parkingSpaces.Where(x => x.SpaceStatus == SpaceStatus.Vacant
                                                   && planeType <= x.PlaneType)
                .OrderBy(x => (int)x.PlaneType)
                .ThenBy(x => x.SpaceId).ToList();
            if (spaces == null)
            {
                throw new ParkingSpaceException("There are no available parking spaces");
            }


            return spaces;
        }

        public PlaneParkingSpace GetFirstPossiblePlaneParkingSpace(PlaneType planeType)
        {
            var space = _parkingSpaces.OrderBy(x => (int)x.PlaneType)
                .ThenBy(x => x.SpaceId)
                .FirstOrDefault(x => x.SpaceStatus == SpaceStatus.Vacant && planeType <= x.PlaneType);
            if (space == null)
            {
                throw new ParkingSpaceException("There are no available parking spaces");
            }

            return space;

        }

        public void ParkPlaneBySpaceId(int id)
        {
            var parkingSpot = _parkingSpaces.FirstOrDefault(x => x.SpaceId == id && x.SpaceStatus != SpaceStatus.Occupied);
            if (parkingSpot == null)
            {
                throw new ParkingSpaceException("This parking space is no longer available");
            }
            parkingSpot?.UpdateSpaceStatus(SpaceStatus.Occupied);
        }

        public void UnParkPlaneBySpaceId(int id)
        {
            var parkingSpot = _parkingSpaces.FirstOrDefault(x => x.SpaceId == id && x.SpaceStatus != SpaceStatus.Vacant);
            if (parkingSpot == null)
            {
                throw new ParkingSpaceException("This parking space is no longer blocked");
            }
            parkingSpot?.UpdateSpaceStatus(SpaceStatus.Vacant);
        }

        public void ParkPlaneInFirstSlot(PlaneType planeType)
        {
            var parkingSpot = GetFirstPossiblePlaneParkingSpace(planeType);
            if (parkingSpot == null)
            {
                throw new ParkingSpaceException("There are no available parking spaces");
            }
            parkingSpot?.UpdateSpaceStatus(SpaceStatus.Occupied);
        }
    }
}
