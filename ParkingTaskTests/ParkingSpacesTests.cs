using System.Collections.Generic;
using System.Linq;
using ParkingTask;
using ParkingTask.Enums;
using Xunit;

namespace ParkingTaskTests
{

    public class ParkingSpacesTests
    {
        private List<PlaneParkingSpace> CreateParking(int jumboV, int jumboO, int jetV, int jetO, int propV, int propO)
        {
            int id = 1;
            var jumboSpaces = UtilityMethods.CreateJumboParkingSpaces(jumboV, jumboO, ref id);
            var jetSpaces = UtilityMethods.CreateJetParkingSpaces(jetV, jetO, ref id);
            var propSpace = UtilityMethods.CreatePropParkingSpaces(propV, propO, ref id);

            var planeParkingSpaces = new List<PlaneParkingSpace>();
            planeParkingSpaces.AddRange(jumboSpaces);
            planeParkingSpaces.AddRange(jetSpaces);
            planeParkingSpaces.AddRange(propSpace);
            return planeParkingSpaces;
        }

        [Fact]
        public void GetAllParkingSpaces()
        {

            var parkingService = new ParkingService(CreateParking(20, 5, 40, 10, 1, 24));
            var result = parkingService.GetAllParkingSpaces();
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(20, 5, 40, 10, 1, 24, 61)]
        [InlineData(5, 20, 3, 47, 12, 13, 20)]
        public void GetAllVacantSpaces(int jumboV, int jumboO, int jetV, int jetO, int propV, int propO, int expected)
        {
            var parkingService = new ParkingService(CreateParking(jumboV, jumboO, jetV, jetO, propV, propO));
            var result = parkingService.GetAllVacantParkingSpaces();
            Assert.Equal(expected, result.Count);
        }

        [Theory]
        [InlineData(20, 5, 40, 10, 1, 24, 60, PlaneType.Jet, PlaneType.Jet)]
        [InlineData(5, 20, 3, 47, 12, 13, 5, PlaneType.Jumbo, PlaneType.Jumbo)]
        [InlineData(5, 20, 3, 47, 12, 13, 20, PlaneType.Prop, PlaneType.Prop)]
        [InlineData(5, 20, 0, 50, 0, 25, 5, PlaneType.Prop, PlaneType.Jumbo)]
        [InlineData(5, 20, 0, 50, 12, 13, 5, PlaneType.Jet, PlaneType.Jumbo)]
        public void GetAllPossibleVacantParkingSpaces(int jumboV, int jumboO, int jetV, int jetO, int propV, int propO, int expected, PlaneType planeType, PlaneType expectedPlaneType)
        {
            var parkingService = new ParkingService(CreateParking(jumboV, jumboO, jetV, jetO, propV, propO));
            var result = parkingService.GetAllPossibleVacantParkingSpaces(planeType);
            Assert.Equal(expected, result.Count());
            Assert.Equal(expectedPlaneType, result.First().PlaneType);

        }

        [Theory]
        [InlineData(20, 5, 40, 10, 1, 24, 40, PlaneType.Jet)]
        [InlineData(5, 20, 3, 47, 12, 13, 5, PlaneType.Jumbo)]
        [InlineData(5, 20, 3, 47, 12, 13, 12, PlaneType.Prop)]
        [InlineData(5, 20, 0, 50, 0, 25, 0, PlaneType.Prop)]
        [InlineData(5, 20, 0, 50, 12, 13, 0, PlaneType.Jet)]
        public void GetAllVacantParkingSpacesByType(int jumboV, int jumboO, int jetV, int jetO, int propV, int propO, int expected, PlaneType planeType)
        {
            var parkingService = new ParkingService(CreateParking(jumboV, jumboO, jetV, jetO, propV, propO));
            var result = parkingService.GetAllVacantParkingSpacesForType(planeType);
            Assert.Equal(expected, result.Count());

        }

        [Theory]
        [InlineData(0, 25, 10, 40, 12, 13, PlaneType.Jumbo)]
        [InlineData(0, 25, 0, 50, 12, 13, PlaneType.Jet)]
        [InlineData(0, 25, 0, 50, 0, 25, PlaneType.Prop)]
        public void GetAllPossibleVacantParkingSpacesReturnsEmpty(int jumboV, int jumboO, int jetV, int jetO, int propV,
            int propO, PlaneType planeType)
        {
            var parkingService = new ParkingService(CreateParking(jumboV, jumboO, jetV, jetO, propV, propO));
            var result = parkingService.GetAllPossibleVacantParkingSpaces(planeType);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(20, 5, 40, 10, 1, 24, 60, PlaneType.Jet, PlaneType.Jet)]
        [InlineData(5, 20, 3, 47, 12, 13, 5, PlaneType.Jumbo, PlaneType.Jumbo)]
        [InlineData(5, 20, 3, 47, 12, 13, 20, PlaneType.Prop, PlaneType.Prop)]
        [InlineData(5, 20, 0, 50, 0, 25, 5, PlaneType.Prop, PlaneType.Jumbo)]
        [InlineData(5, 20, 0, 50, 12, 13, 5, PlaneType.Jet, PlaneType.Jumbo)]
        public void TestParking(int jumboV, int jumboO, int jetV, int jetO, int propV,
            int propO, int expected, PlaneType planeType, PlaneType expectedPlaneType)
        {
            var parkingService = new ParkingService(CreateParking(jumboV, jumboO, jetV, jetO, propV, propO));

            var spaces = parkingService.GetAllPossibleVacantParkingSpaces(planeType);
            var space = parkingService.GetFirstPossiblePlaneParkingSpace(planeType);

            parkingService.ParkPlaneBySpaceId(space.SpaceId);
            
            var result = parkingService.GetAllParkingSpaces().First(x => x.SpaceId == space.SpaceId);

            Assert.Equal(expected, spaces.Count());
            Assert.Equal(expectedPlaneType, result.PlaneType);
            Assert.Equal(SpaceStatus.Occupied, result.SpaceStatus);

        }
        
        [Theory]
        [InlineData(20, 5, 40, 10, 1, 24, PlaneType.Jet)]
        [InlineData(5, 20, 3, 47, 12, 13, PlaneType.Jumbo)]
        [InlineData(5, 20, 3, 47, 12, 13, PlaneType.Prop)]
        [InlineData(5, 20, 0, 50, 0, 25,  PlaneType.Prop)]
        [InlineData(5, 20, 0, 50, 12, 13, PlaneType.Jet)]
        public void TestUnParking(int jumboV, int jumboO, int jetV, int jetO, int propV,
            int propO, PlaneType planeType)
        {
            var parkingService = new ParkingService(CreateParking(jumboV, jumboO, jetV, jetO, propV, propO));

            var spaces = parkingService.GetAllPossibleVacantParkingSpaces(planeType);
            var space = parkingService.GetFirstPossiblePlaneParkingSpace(planeType);

            space.SpaceStatus = SpaceStatus.Vacant;

            Assert.Throws<ParkingSpaceException>(() => parkingService.UnParkPlaneBySpaceId(space.SpaceId));

        }


        [Theory]
        [InlineData(0, 25, 10, 40, 12, 13, PlaneType.Jumbo)]
        [InlineData(0, 25, 0, 50, 12, 13, PlaneType.Jet)]
        [InlineData(0, 25, 0, 50, 0, 25, PlaneType.Prop)]
        public void AssertExceptionThrownWhenNoSpaceFound(int jumboV, int jumboO, int jetV, int jetO, int propV,
            int propO, PlaneType planeType)
        {
            var parkingService = new ParkingService(CreateParking(jumboV, jumboO, jetV, jetO, propV, propO));
            Assert.Throws<ParkingSpaceException>(() => parkingService.ParkPlaneInFirstSlot(planeType));

        }

        [Theory]
        [InlineData(20, 5, 40, 10, 1, 24, PlaneType.Jet)]
        [InlineData(5, 20, 3, 47, 12, 13, PlaneType.Jumbo)]
        [InlineData(5, 20, 3, 47, 12, 13, PlaneType.Prop)]
        [InlineData(5, 20, 0, 50, 0, 25, PlaneType.Prop)]
        [InlineData(5, 20, 0, 50, 12, 13, PlaneType.Jet)]
        public void AssertExceptionThrownWhenSpaceAlreadyOccupied(int jumboV, int jumboO, int jetV, int jetO, int propV,
            int propO, PlaneType planeType)
        {
            var parkingService = new ParkingService(CreateParking(jumboV, jumboO, jetV, jetO, propV, propO));

            var space = parkingService.GetFirstPossiblePlaneParkingSpace(planeType);
            
            space.SpaceStatus = SpaceStatus.Occupied;

            Assert.Throws<ParkingSpaceException>(() => parkingService.ParkPlaneBySpaceId(space.SpaceId));

        }
    }
}
