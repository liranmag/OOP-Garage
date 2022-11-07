using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel //done
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            m_ManufacturerName = "";
            m_CurrentAirPressure = 0;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }


        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }

            set
            {
                m_MaxAirPressure = value;
            }
        }

        public bool AirInflation(float i_AirToInflate)
        {
            bool isInflated;
            if (this.m_CurrentAirPressure + i_AirToInflate > this.m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, this.m_MaxAirPressure - this.m_CurrentAirPressure);
            }
            else
            {
                this.m_CurrentAirPressure += i_AirToInflate;
                isInflated = true;
            }

            return isInflated;
        }
    }
}
