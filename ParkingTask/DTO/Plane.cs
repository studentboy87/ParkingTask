using ParkingTask.Enums;

namespace ParkingTask
{
    public abstract class Plane
    {
        private readonly PlaneType _planeType;

        protected Plane(PlaneType planeType)
        {
            _planeType = planeType;
        }


    }
}
