using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private Wheel[] m_Wheels;
        private Engine m_Engine;

        public Vehicle(int i_NumOfWheels, float i_MaxAirPressure)
        {
            m_Wheels = new Wheel[i_NumOfWheels];
            for(int j = 0; j < i_NumOfWheels; j++)
            {
                m_Wheels[j] = new Wheel(i_MaxAirPressure);
            }
            m_Engine = new Engine();
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }

            set
            {
                m_Engine = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public float RemainingEnergy
        {
            get
            {
                return Engine.CurrentEnergyAmount;
            }

            set
            {
                Engine.CurrentEnergyAmount = value;
            }
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }

            set
            {
                m_Wheels = value;
            }
        }

        public void InflatingVehicleWheelsToMaximum()
        {
            foreach (Wheel currentWheel in m_Wheels)
            {
                float AirToInflate = currentWheel.MaxAirPressure - currentWheel.CurrentAirPressure;

                if (AirToInflate > 0)
                {
                    currentWheel.AirInflation(AirToInflate);
                }
            }
        }

        public virtual Dictionary<string, Func<string, bool>> GetQuestionsNeededToInitialize()
        {
            Dictionary<string, Func<string, bool>> questionsNeeded = new Dictionary<string, Func<string, bool>>();
            questionsNeeded.Add("1. Model Name: ", ValidModelName);
            questionsNeeded.Add("2. License Number: ", ValidLicenseNumber);
            questionsNeeded.Add(String.Format("3. Remaining Energy (a float value between 0 to {0}): ", this.Engine.MaxEnergyAmount), ValidRemainingEnergy);
            questionsNeeded.Add(String.Format("4. Current Wheel Air Pressure (a float value between 0 to {0}): ", this.Wheels[0].MaxAirPressure), ValidWheelAirPressure);
            questionsNeeded.Add("5. Wheels Manufacturer: ", ValidWheelModel);

            return questionsNeeded;
        }

        public virtual void UpdateDetails(int i_QuestionNumber, string i_ValueToUpdate)
        {
            switch (i_QuestionNumber)
            {
                case 1:
                    m_ModelName = i_ValueToUpdate;
                    break;
                case 2:
                    m_LicenseNumber = i_ValueToUpdate;
                    break;
                case 3:
                    this.Engine.CurrentEnergyAmount = float.Parse(i_ValueToUpdate);
                    break;
                case 4:
                    foreach(Wheel wheel in m_Wheels)
                    {
                        wheel.CurrentAirPressure = float.Parse(i_ValueToUpdate);
                    }
                    break;
                case 5:
                    foreach (Wheel wheel in m_Wheels)
                    {
                        wheel.ManufacturerName = i_ValueToUpdate;
                    }
                    break;
            }
        }

        public bool ValidModelName(string i_ModelNameToValidate)
        {
            return true;
        }

        public bool ValidLicenseNumber(string i_LicenseNumberToValidate)
        {
            return true;
        }

        public bool ValidWheelModel(string i_LicenseNumberToValidate)
        {
            return true;
        }

        public bool ValidRemainingEnergy(string i_RemainingEnergyToValidate)
        {
            float remainingEnergy = -1;
            if (!float.TryParse(i_RemainingEnergyToValidate, out remainingEnergy)){
                throw new FormatException("Invalid Input, please enter a float value");
            }else if(remainingEnergy < 0 || remainingEnergy > this.Engine.MaxEnergyAmount)
            {
                throw new ValueOutOfRangeException(0, this.Engine.MaxEnergyAmount);
            }
            return true;
        }

        public bool ValidWheelAirPressure(string i_WheelAirPressureToValidate)
        {
            float wheelAirPressurey = -1;
            if (!float.TryParse(i_WheelAirPressureToValidate, out wheelAirPressurey))
            {
                throw new FormatException("Invalid Input, please enter a float value");
            }
            else if (wheelAirPressurey < 0 || wheelAirPressurey > this.Wheels[0].MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, this.Wheels[0].MaxAirPressure);
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder VehicleDetails = new StringBuilder();
            VehicleDetails.Append(String.Format("Vehicle details are: {0}", Environment.NewLine));
            VehicleDetails.Append(String.Format("License number is: {0}{1} ", this.LicenseNumber, Environment.NewLine));
            VehicleDetails.Append(String.Format("Model name is: {0}{1} ", this.ModelName, Environment.NewLine));
            VehicleDetails.Append(String.Format("Tire manufacturer is: {0}{1} ", this.Wheels[0].ManufacturerName, Environment.NewLine));
            VehicleDetails.Append(String.Format("Tire air pressure is: {0}{1} ", this.Wheels[0].CurrentAirPressure, Environment.NewLine));
            VehicleDetails.Append(String.Format("Tire max air pressure is: {0}{1} ", this.Wheels[0].ManufacturerName, Environment.NewLine));
            VehicleDetails.Append(String.Format("Current Energy amount is: {0}{1} ", this.Engine.CurrentEnergyAmount, Environment.NewLine));
            VehicleDetails.Append(String.Format("Energy type is : {0}{1}", Engine.EnergyType, Environment.NewLine));
            return VehicleDetails.ToString();
        }
    }
}
