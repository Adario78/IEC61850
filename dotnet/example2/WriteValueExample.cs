using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using IEC61850.Client;
using IEC61850.Common;

namespace example2
{
    class WriteValueExample
    {
        public static void Main (string[] args)
        {
            IedConnection con = new IedConnection ();

            string hostname;

            if (args.Length > 0)
                hostname = args[0];
            else
                hostname = "192.168.1.101";

            Console.WriteLine("Connect to " + hostname);

            try
            {
                con.Connect(hostname, 102);
                int intg = 0;
                do
                {
                    //float setMagF = con.ReadFloatValue("AA1K1Q02A1LD0/VMMXU1.PhV.phsA.cVal.mag.f", FunctionalConstraint.MX);
                    bool setMagF = con.ReadBooleanValue("AA1K1Q02A1LD0/SPCGGIO1.SPCSO1.Oper.ctlVal", FunctionalConstraint.CO);
                    //float setMagF = con.ReadFloatValue("AA1K1Q02A1LD0/VMMXU1.PhV.phsA.cVal.mag.f", FunctionalConstraint.MX);
                    //float setMagF = con.ReadFloatValue("AA1K1Q02A1LD0/VMMXU1.PhV.phsA.cVal.mag.f", FunctionalConstraint.MX);

                    Console.WriteLine("AA1K1Q02A1LD0/SPCGGIO1.SPCSO1.Oper.ctlVal: " + setMagF);
                    if (setMagF == true)
                        setMagF = false;
                    else
                        setMagF = true;

                    con.WriteValue("AA1K1Q02A1LD0/SPCGGIO1.SPCSO1.Oper.ctlVal", FunctionalConstraint.CO, new MmsValue(setMagF));
                    setMagF = con.ReadBooleanValue("AA1K1Q02A1LD0/SPCGGIO1.SPCSO1.Oper.ctlVal", FunctionalConstraint.CO);
                    Console.WriteLine("AA1K1Q02A1LD0/SPCGGIO1.SPCSO1.Oper.ctlVal: " + setMagF);
                    Console.ReadLine();
                    intg += 1;
                } while (intg < 5);
                
                con.Abort();
            }
            catch (IedConnectionException e)
            {
                Console.WriteLine("IED connection excepion: " + e.Message);
            }

            // release all resources - do NOT use the object after this call!!
            con.Dispose ();
        }
    }
}
