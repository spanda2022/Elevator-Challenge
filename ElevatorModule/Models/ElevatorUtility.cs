using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorModule.Models
{
    public static class ElevatorUtility
    {
        public static ElevatorType ShowElevatorType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return ElevatorType.Unknown;
            }
            ElevatorType output = ElevatorType.Unknown;
            switch (type)
            {
                case "Passenger":
                    output = ElevatorType.Passenger;
                    break;

                case "Freight":
                    output = ElevatorType.Freight;
                    break;

                case "HighSpeed":
                    output = ElevatorType.HighSpeed;
                    break;

                default:
                    output = ElevatorType.Unknown;
                    break;

            }
            return output;
        }
    }
}
