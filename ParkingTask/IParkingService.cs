using System.Collections.Generic;
using ParkingTask.Enums;

namespace ParkingTask
{
    public interface IParkingService
    {
        List<PlaneParkingSpace> GetAllParkingSpaces();
        List<PlaneParkingSpace> GetAllVacantParkingSpaces();
        List<PlaneParkingSpace> GetAllVacantParkingSpacesForType(PlaneType planeType);
        List<PlaneParkingSpace> GetAllPossibleVacantParkingSpaces(PlaneType planeType);
        PlaneParkingSpace GetFirstPossiblePlaneParkingSpace(PlaneType planeType);
        void ParkPlaneBySpaceId(int id);
        void UnParkPlaneBySpaceId(int id);
        void ParkPlaneInFirstSlot(PlaneType planeType);
    }
}
