using System.Collections.Generic;
using ParkingTask;
using ParkingTask.Enums;

namespace ParkingTaskTests
{
    public class UtilityMethods
    {
        internal static List<JumboParkingSpace> CreateJumboParkingSpaces(int vacant, int occupied, ref int id)
        {
            var jumboSpaces = new List<JumboParkingSpace>();
            for (int i = 0; i < vacant; i++)
            {
                jumboSpaces.Add(new JumboParkingSpace
                {
                    SpaceId = id,
                    SpaceStatus = SpaceStatus.Vacant
                });
                id++;
            }

            for (int i = 0; i < occupied; i++)
            {
                jumboSpaces.Add(new JumboParkingSpace
                {
                    SpaceId = id, 
                    SpaceStatus = SpaceStatus.Occupied
                });
                id++;
            }

            return jumboSpaces;
        }
        internal static List<JetParkingSpace> CreateJetParkingSpaces(int vacant, int occupied, ref int id)
        {
            var jetSpaces = new List<JetParkingSpace>();
            for (int i = 0; i < vacant; i++)
            {
                jetSpaces.Add(new JetParkingSpace
                {
                    SpaceId = id,
                    SpaceStatus = SpaceStatus.Vacant
                    
                });
                id++;
            }

            for (int i = 0; i < occupied; i++)
            {
                jetSpaces.Add(new JetParkingSpace
                {
                    SpaceId = id,
                    SpaceStatus = SpaceStatus.Occupied
                });
                id++;
            }

            return jetSpaces;
        }
        internal static List<PropParkingSpace> CreatePropParkingSpaces(int vacant, int occupied, ref int id)
        {
            
            var propSpaces = new List<PropParkingSpace>();
            for (int i = 0; i < vacant; i++)
            {
                propSpaces.Add(new PropParkingSpace
                {
                    SpaceId = id,
                    SpaceStatus = SpaceStatus.Vacant

                });
                id++;
            }

            for (int i = 0; i < occupied; i++)
            {
                propSpaces.Add(new PropParkingSpace
                {
                    SpaceId = id,
                    SpaceStatus = SpaceStatus.Occupied
                });
                id++;
            }

            return propSpaces;
        }
    }
}
