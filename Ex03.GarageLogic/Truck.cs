using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle // done
    {
        private const int r_NumOfWheels = 16;
        private const float r_MaxAirPressure = 24f;
        private readonly float r_FuelTankLiterCapacity = 120f;
        private readonly eEnergyType r_TruckFuelType = eEnergyType.Soler;
        private bool m_DrivesRefrigeratedContents;
        private float m_TruckVolume;

        public Truck() : base(r_NumOfWheels, r_MaxAirPressure)
        {
            Engine.EnergyType = r_TruckFuelType;
            Engine.MaxEnergyAmount = r_FuelTankLiterCapacity;
        }

        public override Dictionary<string, Func<string, bool>> GetQuestionsNeededToInitialize()
        {
            Dictionary<string, Func<string, bool>> questionsNeeded = base.GetQuestionsNeededToInitialize();
            int numOfQuestions = questionsNeeded.Count;
            questionsNeeded.Add(string.Format("{0}. Is Truck Drives Refrigerated Contents (type exactly - True/False): ", ++numOfQuestions), ValidDriveRefrigeratedContents);
            questionsNeeded.Add(string.Format("{0}. Truck Volume (type positive integer value): ", ++numOfQuestions), ValidTruckVolume);
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
                        m_DrivesRefrigeratedContents = bool.Parse(i_ValueToUpdate);
                        break;
                    case 7:
                        m_TruckVolume = int.Parse(i_ValueToUpdate);
                        break;
                }
            }
        }

        public bool ValidDriveRefrigeratedContents(string i_DriveRefrigeratedContentsToValidate)
        {
            if (!bool.TryParse(i_DriveRefrigeratedContentsToValidate, out bool driveRefrigeratedContents))
            {
                throw new FormatException("Invalid Input, please enter a boolean value");
            }
            return true;
        }

        public bool ValidTruckVolume(string i_TruckVolumeToValidate)
        {
            float truckVolume = -1;
            if (!float.TryParse(i_TruckVolumeToValidate, out truckVolume))
            {
                throw new FormatException("Invalid Input, please enter a float value");
            }
            else if (truckVolume < 0)
            {
                throw new ValueOutOfRangeException(0, int.MaxValue);
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder truckDetails = new StringBuilder(base.ToString());
            truckDetails.Append(String.Format("Is truck can drives refrigerated contents: {0}", this.m_DrivesRefrigeratedContents));
            truckDetails.Append(String.Format("Truck volume is: {0}", this.m_TruckVolume));
            return truckDetails.ToString();
        }
    }
}
