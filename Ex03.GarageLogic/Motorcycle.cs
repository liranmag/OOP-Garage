using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int r_NumOfWheels = 2;
        private const float r_MaxAirPressure = 31f;
        private readonly float r_FuelTankLiterCapacity = 6.2f;
        private readonly float r_BatteryHoursCapacity = 2.5f;
        private readonly eEnergyType r_MotorcycleFuelType = eEnergyType.Octan98;
        private readonly eEnergyType r_MotorcycleElectricType = eEnergyType.Electric;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(eEnergyType i_EnergyType) : base(r_NumOfWheels, r_MaxAirPressure)
        {
            if (i_EnergyType == eEnergyType.Electric)
            {
                Engine.EnergyType = r_MotorcycleElectricType;
                Engine.MaxEnergyAmount = r_BatteryHoursCapacity;
            }
            else
            {
                Engine.EnergyType = r_MotorcycleFuelType;
                Engine.MaxEnergyAmount = r_FuelTankLiterCapacity;
            }
        }

        public override Dictionary<string, Func<string, bool>> GetQuestionsNeededToInitialize()
        {
            Dictionary<string, Func<string, bool>> questionsNeeded = base.GetQuestionsNeededToInitialize();
            int numOfQuestions = questionsNeeded.Count;
            questionsNeeded.Add(string.Format("{0}. License Type (type exactly - A/A1/B1/BB): ", ++numOfQuestions), ValidLicenseType);
            questionsNeeded.Add(string.Format("{0}. Engine Volume (type positive integer value): ", ++numOfQuestions), ValidEngineVolume);
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
                        m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_ValueToUpdate);
                        break;
                    case 7:
                        m_EngineVolume = int.Parse(i_ValueToUpdate);
                        break;
                }
            }
        }

        public bool ValidLicenseType(string i_LicenseTypeToValidate)
        {
            switch (i_LicenseTypeToValidate)
            {
                case "A1":
                    break;
                case "A":
                    break;
                case "BB":
                    break;
                case "B1":
                    break;
                default:
                    throw new FormatException("Invalid Input, please enter the license type excatly");
            }
            return true;
        }

        public bool ValidEngineVolume(string i_EngineVolumeToValidate)
        {
            int engineVolume = -1;
            if (!int.TryParse(i_EngineVolumeToValidate, out engineVolume))
            {
                throw new FormatException("Invalid Input, please enter a integer value");
            }
            else if (engineVolume < 0)
            {
                throw new ValueOutOfRangeException(0, int.MaxValue);
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder motorcycleDetails = new StringBuilder(base.ToString());
            motorcycleDetails.Append(String.Format("License type is: {0}{1} ", nameof(m_LicenseType), Environment.NewLine));
            motorcycleDetails.Append(String.Format("Engine volume is: {0}{1} ", m_EngineVolume, Environment.NewLine));
            return motorcycleDetails.ToString();
        }
    }
}
