using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int r_NumOfWheels = 4;
        private const float r_MaxAirPressure = 29f;
        private readonly float r_FuelTankLiterCapacity = 38f;
        private readonly float r_BatteryHoursCapacity = 3.3f;
        private readonly eEnergyType r_CarFuelType = eEnergyType.Octan95;
        private readonly eEnergyType r_CarElectricType = eEnergyType.Electric;
        private eCarColor m_Color;
        private eNumberOfDoors m_NumberOfDoors;

        public Car(eEnergyType i_EnergyType) : base(r_NumOfWheels, r_MaxAirPressure)
        {
            if (i_EnergyType == eEnergyType.Electric)
            {
                Engine.EnergyType = r_CarElectricType;
                Engine.MaxEnergyAmount = r_BatteryHoursCapacity;
            }
            else
            {
                Engine.EnergyType = r_CarFuelType;
                Engine.MaxEnergyAmount = r_FuelTankLiterCapacity;
            }
        }
        public eCarColor CarColor
        {
            get
            {
                return m_Color;
            }

            set
            {
                m_Color = value;
            }
        }
        public eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                m_NumberOfDoors = value;
            }
        }

        public override Dictionary<string, Func<string, bool>> GetQuestionsNeededToInitialize()
        {
            Dictionary<string, Func<string, bool>> questionsNeeded = base.GetQuestionsNeededToInitialize();
            int numOfQuestions = questionsNeeded.Count;
            questionsNeeded.Add(string.Format("{0}. Car Color (type exactly - Red/White/Green/Blue): ", ++numOfQuestions), ValidCarColor);
            questionsNeeded.Add(string.Format("{0}. Number Of Doors (type exactly - 2/3/4/5): ", ++numOfQuestions), ValidNumberOfDoors);
            return questionsNeeded;
        }

        public override void UpdateDetails(int i_QuestionNumber, string i_ValueToUpdate)
        {
            if (i_QuestionNumber <= 5)
            {
                base.UpdateDetails(i_QuestionNumber, i_ValueToUpdate);
            }
            else
            {
                switch (i_QuestionNumber)
                {
                    case 6:
                        CarColor = (eCarColor)Enum.Parse(typeof(eCarColor), i_ValueToUpdate);
                        break;
                    case 7:
                        NumberOfDoors = (eNumberOfDoors) int.Parse(i_ValueToUpdate);
                        break;
                }
            }
        }


        public bool ValidCarColor(string i_CarColorToValidate)
        {
            switch (i_CarColorToValidate)
            {
                case "Red":
                    break;
                case "White":
                    break;
                case "Green":
                    break;
                case "Blue":
                    break;
                default:
                    throw new FormatException("Invalid Input, please enter the car color excatly");
            }
            return true;
        }

        public bool ValidNumberOfDoors(string i_NumberOfDoorsToValidate)
        {
            int numberOfDoors = -1;
            if (!int.TryParse(i_NumberOfDoorsToValidate, out numberOfDoors))
            {
                throw new FormatException("Invalid Input, please enter a integer value");
            }
            else if (numberOfDoors < 2 || numberOfDoors > 5)
            {
                throw new ValueOutOfRangeException(2, 5);
            }
            return true;
        }

        public override string ToString()
        {
            string s = base.ToString();
            StringBuilder carDetails = new StringBuilder(s);
            carDetails.Append(String.Format("Number Of Doors is: {0}{1}", (int) NumberOfDoors, Environment.NewLine));
            carDetails.Append(String.Format("Car color is: {0}{1}", nameof(CarColor), Environment.NewLine));
            return carDetails.ToString();
        }
    }
}
