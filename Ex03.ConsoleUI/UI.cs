using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        public static void OpenDialog()
        {
            Console.WriteLine("Hello!");
            Console.WriteLine("Wellcome to the best Garage in town!");
        }

        public static void CloseDialog()
        {
            Console.Clear();
            Console.WriteLine("Thanks, hope you enjoyed it");
            Console.WriteLine("Have a nice day!");
            Console.WriteLine("Press 'Enter' to exit");
            Console.ReadLine();
        }

        private static int getUserMenuSelection(string i_menuOptions, int i_MenuMinValue, int i_MenuMaxValue)
        {
            Console.WriteLine(i_menuOptions);

            string userOptionSelectionString = Console.ReadLine();
            int userOptionSelection = -1;

            if (!int.TryParse(userOptionSelectionString, out userOptionSelection))
            {
                throw new FormatException("Please enter a integer from the menu");
            }
            else if (userOptionSelection < i_MenuMinValue || userOptionSelection > i_MenuMaxValue)
            {
                throw new ValueOutOfRangeException(i_MenuMinValue, i_MenuMaxValue);
            }

            return userOptionSelection;
        }

        public static void StartGarage()
        {
            int userOptionSelection;
            string menuOptions = @"Choose one of the following options (numbers) from the menu:
1. Add new vehicle to garage
2. Show all vehicles license number by status
3. Change a certain vehicle’s status
4. Inflate a certain vehicle’s tires to maximum
5. Refuel a fuel-based vehicle
6. Charge an electric-based vehicle
7. Display vehicle information
8. Leave Garage";
            bool closeGarage = false;
            while (!closeGarage)
            {
                try
                {
                    userOptionSelection = getUserMenuSelection(menuOptions, 1, 8);
                    switch (userOptionSelection)
                    {
                        case 1:
                            addNewVehicle();
                            break;
                        case 2:
                            showVehiclesByStatus(); //done.
                            break;
                        case 3:
                            changeVehicleStatus(); // done.
                            break;
                        case 4:
                            inflateAirToMaximum(); //done
                            break;
                        case 5:
                            fuelVehicle();
                            break;
                        case 6:
                            chargeVehicle();
                            break;
                        case 7:
                            showAllVehicleDetails();
                            break;
                        case 8:
                            closeGarage = true;
                            break;
                    }
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void addNewVehicle()
        {
            Console.Clear();
            string vehicelOptionMenu = @"Choose one of the following vehicles (numbers) you want to add:
1. Fuel Car
2. Electric Car
3. Fuel Motorcycle
4. Electric Motorcycle
5. Truck";
            bool correctInput = false;
            eVehicleType chosenVehicelType = eVehicleType.Null;

            while (!correctInput)
            {
                try
                {
                    chosenVehicelType = (eVehicleType)getUserMenuSelection(vehicelOptionMenu, 1, 5);
                    correctInput = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Dictionary<string, Func<string, bool>> questionsNeededToInitialize = new Dictionary<string, Func<string, bool>>();
            Vehicle vehicle = Garage.InitializeNewVehicle(chosenVehicelType, ref questionsNeededToInitialize);

            string answer = "";
            Console.WriteLine("Please enter more details about the vehicle: ");
            foreach (KeyValuePair<string, Func<string, bool>> item in questionsNeededToInitialize)
            {
                bool answerMatch = false;
                while (!answerMatch)
                {
                    Console.WriteLine(item.Key);
                    answer = Console.ReadLine();
                    try
                    {
                        if (item.Value.Invoke(answer))
                        {
                            answerMatch = true;
                            Garage.UpdateDetails(vehicle, item.Key[0] - '0', answer);
                        }
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (ValueOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            Console.WriteLine("Now, to add the new vehicle to the garage please enter your details");
            Console.WriteLine("Enter your name:");
            string ownerName = Console.ReadLine();
            Console.WriteLine("Enter your phone number: ");
            string ownerPhoneNumber = Console.ReadLine();
            Garage.AddVehicleToGarage(vehicle, ownerName, ownerPhoneNumber);
            Console.WriteLine("A new vehicle successfully added to garage {0}", Environment.NewLine);
        }

        private static void showVehiclesByStatus() // done.
        {
            Console.Clear();
            string menuOptions = @"Please select the status you woul'd like to filter by (type the number):
1. InRepair 
2. Repair
3. Paid ";
            bool correctInput = false;

            while (!correctInput)
            {
                try
                {
                    eVehicleStatus userOptionSelectionAsEnum = (eVehicleStatus)getUserMenuSelection(menuOptions, 1, 3);
                    List<string> LicenseNumberByStatus = Garage.GetAllLicenseNumberByStatus(userOptionSelectionAsEnum);
                    foreach (string LicenseNumber in LicenseNumberByStatus)
                    {
                        Console.WriteLine(LicenseNumber);
                    }
                    correctInput = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void changeVehicleStatus() // done
        {
            Console.Clear();

            string menuOptions = @"To what status would you like to change (type the number)?
1. InRepair 
2. Repair
3. Paid ";
            bool correctInput = false;

            while (!correctInput)
            {
                try
                {
                    eVehicleStatus userOptionSelectionAsEnum = (eVehicleStatus)getUserMenuSelection(menuOptions, 1, 3);
                    correctInput = true;
                    Console.WriteLine("Please enter the vehicle license number: ");
                    string LicenseNumberString = Console.ReadLine();
                    Garage.SetNewStatusByLicenseNumber(LicenseNumberString, userOptionSelectionAsEnum);
                    Console.WriteLine("Status has been updated successfully !{0}", Environment.NewLine);
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void inflateAirToMaximum()// done
        {
            Console.Clear();

            Console.WriteLine("Enter the license number of vehicle to inflate: ");
            string LicenseNumberString = Console.ReadLine();

            try
            {
                Garage.InflateByLicenseNumber(LicenseNumberString);
                Console.WriteLine("vehicle has been inflated successfully !{0}", Environment.NewLine);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ValueOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void fuelVehicle()
        {
            Console.Clear();

            Console.WriteLine("Please enter the vehicle license number: ");
            string LicenseNumberString = Console.ReadLine();
            Vehicle vehicleToRefuel;
            try
            {
                vehicleToRefuel = Garage.GetVehicleByLicenseNumber(LicenseNumberString);
                if (vehicleToRefuel.Engine.EnergyType == eEnergyType.Electric)
                {
                    Console.WriteLine("It's not possible to fuel an a electric vehicle");
                    return;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            string menuOptions = @"Please enter the type of fuel you want to use (type the number):
1. Octan95 
2. Octan96
3. Octan98
4. Soler";
            bool correctInput = false;
            bool isRefuel = false;

            while (!correctInput)
            {
                try
                {
                    eEnergyType userOptionSelectionAsEnum = (eEnergyType)getUserMenuSelection(menuOptions, 1, 4);
                    Console.WriteLine("Please enter quantity to refuel: ");
                    string refuelAmountString = Console.ReadLine();
                    isRefuel = Garage.FuelVehicle(vehicleToRefuel, userOptionSelectionAsEnum, refuelAmountString);
                    correctInput = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            if (isRefuel)
            {
                Console.WriteLine("vehicle has been Refuel successfully !{0}", Environment.NewLine);
            }
        }

        private static void chargeVehicle()
        {
            Console.Clear();

            Console.WriteLine("Please enter the vehicle license number: ");
            string LicenseNumberString = Console.ReadLine();
            Vehicle vehicleToCharge;

            try
            {
                vehicleToCharge = Garage.GetVehicleByLicenseNumber(LicenseNumberString);
                if (vehicleToCharge.Engine.EnergyType != eEnergyType.Electric)
                {
                    Console.WriteLine("It's not possible to charge non electric vehicle");
                    return;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            bool correctInput = false;
            bool isCharged = false;

            while (!correctInput)
            {
                try
                {
                    Console.WriteLine("Please enter quantity to charge: ");
                    string chargeAmountString = Console.ReadLine();
                    isCharged = Garage.ChargeVehicle(vehicleToCharge, chargeAmountString);
                    correctInput = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            if (isCharged)
            {
                Console.WriteLine("vehicle has been Charged successfully !{0}", Environment.NewLine);
            }
        }

        private static void showAllVehicleDetails()
        {
            Console.Clear();

            Console.WriteLine("Please enter the vehicle license number: ");
            string LicenseNumberString = Console.ReadLine();
            Vehicle vehicleToCharge;

            try
            {
                vehicleToCharge = Garage.GetVehicleByLicenseNumber(LicenseNumberString);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine(Garage.GetVehicleDetails(vehicleToCharge));
        }
    }
}
