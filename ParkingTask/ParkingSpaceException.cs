using System;

namespace ParkingTask
{
    public class ParkingSpaceException :Exception
    {
        public ParkingSpaceException()
        {
            
        }

        public ParkingSpaceException(string message) : base(message)
        {

        }

        public ParkingSpaceException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
