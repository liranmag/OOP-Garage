using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Engine
    {
        private float m_CurrentEnergyAmount;
        private float m_MaxEnergyAmount;
        private eEnergyType m_EnergyType;

        public Engine()
        {
            m_CurrentEnergyAmount = 0;
            m_MaxEnergyAmount = 0;
        }

        public float CurrentEnergyAmount
        {
            get
            {
                return m_CurrentEnergyAmount;
            }

            set
            {
                m_CurrentEnergyAmount = value;
            }
        }

        public float MaxEnergyAmount
        {
            get
            {
                return m_MaxEnergyAmount;
            }

            set
            {
                m_MaxEnergyAmount = value;
            }
        }

        public eEnergyType EnergyType
        {
            get
            {
                return m_EnergyType;
            }

            set
            {
                m_EnergyType = value;
            }
        }

        public bool Fuel(float i_GasolineToFuel, eEnergyType i_FuelTypeAdding)
        {
            bool isFueled = false;

            if (this.m_EnergyType != i_FuelTypeAdding)
            {
                throw new ArgumentException("Energey Type does not match!");
            }
            else if (this.CurrentEnergyAmount + i_GasolineToFuel > this.MaxEnergyAmount)
            {
                throw new ValueOutOfRangeException(0, this.MaxEnergyAmount - this.CurrentEnergyAmount);
            }
            else
            {
                this.CurrentEnergyAmount += i_GasolineToFuel;
                isFueled = true;
            }

            return isFueled;
        }

        public bool Charge(float i_HoursToCharge)
        {
            return Fuel(i_HoursToCharge, eEnergyType.Electric);
        }
    }
}
