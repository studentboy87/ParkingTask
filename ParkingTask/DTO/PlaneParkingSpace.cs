using ParkingTask.Enums;

namespace ParkingTask
{
    public class PlaneParkingSpace
    {
        public int SpaceId { get; set; }
        public SpaceStatus SpaceStatus { get; set; }
        public virtual PlaneType PlaneType { get; set; }
        public void UpdateSpaceStatus(SpaceStatus status)
        {
            SpaceStatus = status;
        }
    }
}
