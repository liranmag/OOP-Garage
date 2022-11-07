using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageVehicleDetails //done
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;

        public GarageVehicleDetails(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleStatus = eVehicleStatus.InRepair;
        }
        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }

            set
            {
                m_OwnerPhoneNumber = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public override string ToString()
        {
            StringBuilder VehicleDetails = new StringBuilder();
            VehicleDetails.Append(String.Format("Owner name is: {0}{1}", OwnerName, Environment.NewLine));
            VehicleDetails.Append(String.Format("Owner phone number is: {0}{1}", OwnerPhoneNumber, Environment.NewLine));
            VehicleDetails.Append(String.Format("Vehicle status is: {0}{1}", nameof(VehicleStatus), Environment.NewLine));
            return VehicleDetails.ToString();
        }
    }
}
