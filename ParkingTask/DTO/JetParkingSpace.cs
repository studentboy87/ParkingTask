using ParkingTask.Enums;

namespace ParkingTask
{
    public class JetParkingSpace : PlaneParkingSpace
    {
        public override PlaneType PlaneType => PlaneType.Jet;
    }
}
