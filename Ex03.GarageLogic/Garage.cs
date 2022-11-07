using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private static Dictionary<Vehicle, GarageVehicleDetails> s_Vehicles = new Dictionary<Vehicle, GarageVehicleDetails>();

        public static void AddVehicleToGarage(Vehicle i_VehicleToInsert, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            GarageVehicleDetails details = new GarageVehicleDetails(i_OwnerName, i_OwnerPhoneNumber);
            s_Vehicles.Add(i_VehicleToInsert, details);
        }

        public static Vehicle InitializeNewVehicle(eVehicleType i_NewVehicleTypeToCreat, ref Dictionary<string, Func<string, bool>> io_questionsNeeded) // 1
        {
            Vehicle newVehicle = null;

            switch (i_NewVehicleTypeToCreat)
            {
                case eVehicleType.FuelCar:
                    newVehicle = new Car(eEnergyType.Fuel);
                    io_questionsNeeded = (newVehicle as Car).GetQuestionsNeededToInitialize();
                    break;
                case eVehicleType.ElectricCar:
                    newVehicle = new Car(eEnergyType.Electric);
                    io_questionsNeeded = (newVehicle as Car).GetQuestionsNeededToInitialize();
                    break;
                case eVehicleType.FuelMotorcycle:
                    newVehicle = new Motorcycle(eEnergyType.Fuel);
                    io_questionsNeeded = (newVehicle as Motorcycle).GetQuestionsNeededToInitialize();
                    break;
                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new Motorcycle(eEnergyType.Electric);
                    io_questionsNeeded = (newVehicle as Motorcycle).GetQuestionsNeededToInitialize();
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck();
                    io_questionsNeeded = (newVehicle as Truck).GetQuestionsNeededToInitialize();
                    break;
            }

            return newVehicle;
        }

        public static void UpdateDetails(Vehicle io_VehicleToUpdate, int questionNumber, string i_AnswerToUpdate) // 1
        {
            io_VehicleToUpdate.UpdateDetails(questionNumber, i_AnswerToUpdate);
        }

        public static Vehicle GetVehicleByLicenseNumber(string i_LicenseNumber)
        {
            Vehicle vehicleByLicenseNumberToReturn = null;

            foreach (Vehicle vehicle in s_Vehicles.Keys)
            {
                if (vehicle.LicenseNumber == i_LicenseNumber)
                {
                    vehicleByLicenseNumberToReturn = vehicle;
                    break;
                }
            }

            if (vehicleByLicenseNumberToReturn == null)
            {
                throw new ArgumentException("There is no such vehicle with this license number!");
            }

            return vehicleByLicenseNumberToReturn;
        }

        public static List<string> GetAllLicenseNumberByStatus(eVehicleStatus i_VehiclesStatus) // done
        {
            List<string> listOfVehicleMatchToStatus = new List<string>();
            foreach (KeyValuePair<Vehicle, GarageVehicleDetails> GarageVehicleDetails in s_Vehicles)
            {
                if (GarageVehicleDetails.Value.VehicleStatus == i_VehiclesStatus)
                {
                    listOfVehicleMatchToStatus.Add(GarageVehicleDetails.Key.LicenseNumber);
                }
            }
            return listOfVehicleMatchToStatus;
        }

        public static void SetNewStatusByLicenseNumber(string i_LicenseNumber, eVehicleStatus i_NewVehiclesStatus) // done.
        {
            s_Vehicles[GetVehicleByLicenseNumber(i_LicenseNumber)].VehicleStatus = i_NewVehiclesStatus;
        }

        public static void InflateByLicenseNumber(string i_LicenseNumber)
        {
            Vehicle vehicleToInflate = GetVehicleByLicenseNumber(i_LicenseNumber);
            vehicleToInflate.InflatingVehicleWheelsToMaximum();
        }

        public static bool FuelVehicle(Vehicle io_VehicleToFuel, eEnergyType i_FuelTypeToAdd, string i_FuelToAdd)// 5
        {
            if (!float.TryParse(i_FuelToAdd, out float fuelToAdd))
            {
                throw new FormatException("Please type a float value");
            }
            return io_VehicleToFuel.Engine.Fuel(fuelToAdd, i_FuelTypeToAdd);
        }

        public static bool ChargeVehicle(Vehicle io_VehicleToCharge, string i_TimeToCharge) // 6
        {
            return FuelVehicle(io_VehicleToCharge, eEnergyType.Electric, i_TimeToCharge);
        }

        public static string GetVehicleDetails(Vehicle i_VehicleToShow) // 7
        {
            string vehicleDetails = i_VehicleToShow.ToString();
            string garageVehicleDetails = s_Vehicles[i_VehicleToShow].ToString();
            return vehicleDetails + garageVehicleDetails;
        }

    }
}